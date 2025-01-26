using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// レシピの問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class RecipeQueryService(IConfiguration configuration) : IRecipeQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// レシピを問い合わせします。
	/// </summary>
	/// <param name="recipeId">レシピID</param>
	/// <returns>問い合わせしたレシピデータを返します。</returns>
	public RecipeData QuerySingle(string recipeId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		RecipeRecord record;
		ItemSummaryData item;
		{
			const string sql = @"SELECT
	  rec.recipe_id
	, ite.item_id
	, inm.item_name
FROM
	recipes rec
	INNER JOIN items ite
		ON rec.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	recipe_id = :recipe_id";

			var param = new
			{
				recipe_id = recipeId,
				language_code = _languageCode,
			};

			record = connection.QuerySingle<RecipeRecord>(sql, param);

			item = new ItemSummaryData(record.ItemId, record.ItemName);
		}

		List<RecipeIngredientData> ingredients = [];
		{
			const string sql = @"SELECT
	  rig.item_id
	, inm.item_name
	, rig.quantity
FROM
	recipe_ingredients rig
	INNER JOIN items ite
		ON rig.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	rig.recipe_id = :recipe_id
ORDER BY
	rig.item_id";

			var param = new
			{
				recipe_id = recipeId,
				language_code = _languageCode,
			};

			IEnumerable<IngredientRecord> sources = connection.Query<IngredientRecord>(sql, param);

			foreach (IngredientRecord source in sources)
			{
				RecipeIngredientData ingredient;
				{
					ItemSummaryData ingredientItem = new(source.ItemId, source.ItemName);
					int quantity = (int)source.Quantity;

					ingredient = new RecipeIngredientData(ingredientItem, quantity);
				}

				ingredients.Add(ingredient);
			}
		}

		RecipeData result = new(record.RecipeId, item, ingredients);

		return result;
	}

	/// <summary>
	/// レシピを問い合わせします。
	/// </summary>
	/// <param name="recipeId">レシピID</param>
	/// <returns>問い合わせしたレシピデータを返します。</returns>
	public async Task<RecipeData> QuerySingleAsync(string recipeId)
	{
		return await Task.Run(() => QuerySingle(recipeId));
	}

	#endregion

	#region Nested types

	/// <summary>
	/// レシピのレコード
	/// </summary>
	/// <param name="RecipeId">レシピID</param>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	private record class RecipeRecord(string RecipeId, string ItemId, string ItemName);

	/// <summary>
	/// 材料のレコード
	/// </summary>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	/// <param name="Quantity">数量</param>
	private record class IngredientRecord(string ItemId, string ItemName, long Quantity);

	#endregion
}

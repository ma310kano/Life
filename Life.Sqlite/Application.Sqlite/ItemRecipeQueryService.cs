using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// アイテムのレシピの問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class ItemRecipeQueryService(IConfiguration configuration) : IItemRecipeQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// アイテムのレシピを問い合わせします。
	/// </summary>
	/// <param name="itemRecipeId">アイテムのレシピID</param>
	/// <returns>問い合わせしたアイテムのレシピデータを返します。</returns>
	public ItemRecipeData QuerySingle(string itemRecipeId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		RecipeRecord record;
		ItemSummaryData item;
		{
			const string sql = @"SELECT
	  irc.item_recipe_id
	, ite.item_id
	, inm.item_name
FROM
	item_recipes irc
	INNER JOIN items ite
		ON irc.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	irc.item_recipe_id = :item_recipe_id";

			var param = new
			{
				item_recipe_id = itemRecipeId,
				language_code = _languageCode,
			};

			record = connection.QuerySingle<RecipeRecord>(sql, param);

			item = new ItemSummaryData(record.ItemId, record.ItemName);
		}

		List<ItemRecipeIngredientData> ingredients = [];
		{
			const string sql = @"SELECT
	  iri.item_id
	, inm.item_name
	, iri.quantity
FROM
	item_recipe_ingredients iri
	INNER JOIN items ite
		ON iri.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	iri.item_recipe_id = :item_recipe_id
ORDER BY
	iri.item_id";

			var param = new
			{
				item_recipe_id = itemRecipeId,
				language_code = _languageCode,
			};

			IEnumerable<IngredientRecord> sources = connection.Query<IngredientRecord>(sql, param);

			foreach (IngredientRecord source in sources)
			{
				ItemRecipeIngredientData ingredient;
				{
					ItemSummaryData ingredientItem = new(source.ItemId, source.ItemName);
					int quantity = (int)source.Quantity;

					ingredient = new ItemRecipeIngredientData(ingredientItem, quantity);
				}

				ingredients.Add(ingredient);
			}
		}

		ItemRecipeData result = new(record.ItemRecipeId, item, ingredients);

		return result;
	}

	/// <summary>
	/// レシピを問い合わせします。
	/// </summary>
	/// <param name="recipeId">レシピID</param>
	/// <returns>問い合わせしたレシピデータを返します。</returns>
	public async Task<ItemRecipeData> QuerySingleAsync(string recipeId)
	{
		return await Task.Run(() => QuerySingle(recipeId));
	}

	#endregion

	#region Nested types

	/// <summary>
	/// レシピのレコード
	/// </summary>
	/// <param name="ItemRecipeId">レシピID</param>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	private record class RecipeRecord(string ItemRecipeId, string ItemId, string ItemName);

	/// <summary>
	/// 材料のレコード
	/// </summary>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	/// <param name="Quantity">数量</param>
	private record class IngredientRecord(string ItemId, string ItemName, long Quantity);

	#endregion
}

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

		string rItemRecipeId;
		ItemSummaryData item;
		int quantity;
		BuildingSummaryData? building;
		{
			const string sql = @"SELECT
	  irc.item_recipe_id
	, ite.item_id
	, inm.item_name
	, irc.quantity
	, irc.building_id
	, bnm.building_name
FROM
	item_recipes irc
	INNER JOIN items ite
		ON irc.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
	LEFT JOIN building_names bnm
		ON  irc.building_id = bnm.building_id
		AND bnm.language_code = :language_code
WHERE
	irc.item_recipe_id = :item_recipe_id";

			var param = new
			{
				item_recipe_id = itemRecipeId,
				language_code = _languageCode,
			};

			RecipeRecord record = connection.QuerySingle<RecipeRecord>(sql, param);

			rItemRecipeId = record.ItemRecipeId;
			item = new ItemSummaryData(record.ItemId, record.ItemName);
			quantity = (int)record.Quantity;
			building = record.BuildingId is not null ? new BuildingSummaryData(record.BuildingId, record.BuildingName) : null;
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
					int ingredientQuantity = (int)source.Quantity;

					ingredient = new ItemRecipeIngredientData(ingredientItem, ingredientQuantity);
				}

				ingredients.Add(ingredient);
			}
		}

		ItemRecipeData result = new(rItemRecipeId, item, quantity, building, ingredients);

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
	/// <param name="Quantity">数量</param>
	/// <param name="BuildingId">建造物ID</param>
	/// <param name="BuildingName">建造物名</param>
	private record class RecipeRecord(string ItemRecipeId, string ItemId, string ItemName, long Quantity, string? BuildingId, string? BuildingName);

	/// <summary>
	/// 材料のレコード
	/// </summary>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	/// <param name="Quantity">数量</param>
	private record class IngredientRecord(string ItemId, string ItemName, long Quantity);

	#endregion
}

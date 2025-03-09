using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// アイテムレシピのリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class ItemRecipeRepository(IDbConnection connection, IDbTransaction transaction) : IItemRecipeRepository
{
	#region Methods

	/// <summary>
	/// アイテムレシピを検索します。
	/// </summary>
	/// <param name="itemRecipeId">アイテムレシピID</param>
	/// <returns>検索したアイテムレシピを返します。</returns>
	public ItemRecipe Find(ItemRecipeId itemRecipeId)
	{
		ItemRecipeRecord recipe;
		{
			const string sql = @"SELECT
	  item_recipe_id
	, item_id
	, quantity
	, building_id
FROM
	item_recipes
WHERE
	item_recipe_id = :item_recipe_id";

			var param = new
			{
				item_recipe_id = itemRecipeId.Value,
			};

			recipe = connection.QuerySingle<ItemRecipeRecord>(sql, param, transaction);
		}

		IEnumerable<ItemRecipeIngredientRecord> recipeIngredients;
		{
			const string sql = @"SELECT
	  item_id
	, quantity
FROM
	item_recipe_ingredients
WHERE
	item_recipe_id = :item_recipe_id
ORDER BY
	item_id";

			var param = new
			{
				item_recipe_id = itemRecipeId.Value,
			};

			recipeIngredients = connection.Query<ItemRecipeIngredientRecord>(sql, param, transaction);
		}

		ItemRecipe result;
		{
			ItemRecipeId rItemRecipeId = new(recipe.ItemRecipeId);
			ItemId rItemId = new(recipe.ItemId);
			Quantity rQuantity = new((int)recipe.Quantity);
			BuildingId? rBuildingId = recipe.BuildingId is not null ? new(recipe.BuildingId) : null;

			List<RecipeIngredient> rIngredients = [];
			foreach (ItemRecipeIngredientRecord recipeIngredient in recipeIngredients)
			{
				RecipeIngredient rIngredient;
				{
					ItemId rIngredientItemId = new(recipeIngredient.ItemId);
					Quantity rIngredientQuantity = new((int)recipeIngredient.Quantity);

					rIngredient = new RecipeIngredient(rIngredientItemId, rIngredientQuantity);
				}

				rIngredients.Add(rIngredient);
			}

			result = new ItemRecipe(rItemRecipeId, rItemId, rQuantity, rBuildingId, rIngredients);
		}

		return result;
	}

	#endregion

	#region Nested types

	/// <summary>
	/// アイテムレシピのレコード
	/// </summary>
	/// <param name="ItemRecipeId">アイテムレシピID</param>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="Quantity">数量</param>
	/// <param name="BuildingId">建造物ID</param>
	private record class ItemRecipeRecord(string ItemRecipeId, string ItemId, long Quantity, string? BuildingId);

	/// <summary>
	/// アイテムレシピの材料レコード
	/// </summary>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="Quantity">数量</param>
	private record class ItemRecipeIngredientRecord(string ItemId, long Quantity);

	#endregion
}

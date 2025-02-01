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

			List<ItemRecipeIngredient> rIngredients = [];
			foreach (ItemRecipeIngredientRecord recipeIngredient in recipeIngredients)
			{
				ItemRecipeIngredient rIngredient;
				{
					ItemId rIngredientItemId = new(recipeIngredient.ItemId);
					Quantity rQuantity = new((int)recipeIngredient.Quantity);

					rIngredient = new ItemRecipeIngredient(rIngredientItemId, rQuantity);
				}

				rIngredients.Add(rIngredient);
			}

			result = new ItemRecipe(rItemRecipeId, rItemId, rIngredients);
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
	private record class ItemRecipeRecord(string ItemRecipeId, string ItemId);

	/// <summary>
	/// アイテムレシピの材料レコード
	/// </summary>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="Quantity">数量</param>
	private record class ItemRecipeIngredientRecord(string ItemId, long Quantity);

	#endregion
}

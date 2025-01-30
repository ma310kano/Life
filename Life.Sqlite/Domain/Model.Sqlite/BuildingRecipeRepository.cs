using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 建造物レシピのリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class BuildingRecipeRepository(IDbConnection connection, IDbTransaction transaction) : IBuildingRecipeRepository
{
	#region Methods

	/// <summary>
	/// 建造物レシピを検索します。
	/// </summary>
	/// <param name="buildingRecipeId">建造物レシピID</param>
	/// <returns>検索した建造物レシピを返します。</returns>
	public BuildingRecipe Find(BuildingRecipeId buildingRecipeId)
	{
		BuildingRecipeRecord recipe;
		{
			const string sql = @"SELECT
	  building_recipe_id
	, building_id
FROM
	building_recipes
WHERE
	building_recipe_id = :building_recipe_id";

			var param = new
			{
				building_recipe_id = buildingRecipeId.Value,
			};

			recipe = connection.QuerySingle<BuildingRecipeRecord>(sql, param, transaction);
		}

		IEnumerable<BuildingRecipeIngredientRecord> recipeIngredients;
		{
			const string sql = @"SELECT
	  item_id
	, quantity
FROM
	building_recipe_ingredients
WHERE
	building_recipe_id = :building_recipe_id
ORDER BY
	item_id";

			var param = new
			{
				building_recipe_id = buildingRecipeId.Value,
			};

			recipeIngredients = connection.Query<BuildingRecipeIngredientRecord>(sql, param, transaction);
		}

		BuildingRecipe result;
		{
			BuildingRecipeId rBuildingRecipeId = new(recipe.BuildingRecipeId);
			BuildingId rBuildingId = new(recipe.BuildingId);

			List<BuildingRecipeIngredient> rIngredients = [];
			foreach (BuildingRecipeIngredientRecord recipeIngredient in recipeIngredients)
			{
				BuildingRecipeIngredient rIngredient;
				{
					ItemId rItemId = new(recipeIngredient.ItemId);
					Quantity rQuantity = new((int)recipeIngredient.Quantity);

					rIngredient = new BuildingRecipeIngredient(rItemId, rQuantity);
				}

				rIngredients.Add(rIngredient);
			}

			result = new BuildingRecipe(rBuildingRecipeId, rBuildingId, rIngredients);
		}

		return result;
	}

	#endregion

	#region Nested types

	/// <summary>
	/// 建造物レシピのレコード
	/// </summary>
	/// <param name="BuildingRecipeId">建造物レシピID</param>
	/// <param name="BuildingId">建造物ID</param>
	private record class BuildingRecipeRecord(string BuildingRecipeId, string BuildingId);

	/// <summary>
	/// 建造物レシピの材料レコード
	/// </summary>
	/// <param name="ItemId"></param>
	/// <param name="Quantity"></param>
	private record class BuildingRecipeIngredientRecord(string ItemId, long Quantity);

	#endregion
}

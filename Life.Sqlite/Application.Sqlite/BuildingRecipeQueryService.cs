using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 建造物レシピの問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class BuildingRecipeQueryService(IConfiguration configuration) : IBuildingRecipeQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// 建造物レシピを問い合わせします。
	/// </summary>
	/// <param name="buildingRecipeId">建造物レシピID</param>
	/// <returns>問い合わせした建造物レシピデータを返します。</returns>
	public BuildingRecipeData QuerySingle(string buildingRecipeId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		string rBuildingRecipeId;
		BuildingSummaryData rBuilding;
		{
			const string sql = @"SELECT
	  brc.building_recipe_id
	, brc.building_id
	, bnm.building_name
FROM
	building_recipes brc
	INNER JOIN buildings bui
		ON brc.building_id = bui.building_id
	INNER JOIN building_names bnm
		ON  bui.building_id = bnm.building_id
		AND bnm.language_code = :language_code
WHERE
	brc.building_recipe_id = :building_recipe_id";

			var param = new
			{
				building_recipe_id = buildingRecipeId,
				language_code = _languageCode,
			};

			BuildingRecipeRecord source = connection.QuerySingle<BuildingRecipeRecord>(sql, param);

			rBuildingRecipeId = source.BuildingRecipeId;
			rBuilding = new BuildingSummaryData(source.BuildingId, source.BuildingName);
		}

		List<BuildingRecipeIngredientData> rIngredients = [];
		{
			const string sql = @"SELECT
	  bri.item_id
	, inm.item_name
	, bri.quantity
FROM
	building_recipe_ingredients bri
	INNER JOIN items ite
		ON bri.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	bri.building_recipe_id = :building_recipe_id";

			var param = new
			{
				building_recipe_id = buildingRecipeId,
				language_code = _languageCode,
			};

			IEnumerable<BuildingRecipeIngredientRecord> sources = connection.Query<BuildingRecipeIngredientRecord>(sql, param);

			foreach (BuildingRecipeIngredientRecord source in sources)
			{
				BuildingRecipeIngredientData rIngredient;
				{
					ItemSummaryData rItem = new(source.ItemId, source.ItemName);
					int rQuantity = (int)source.Quantity;

					rIngredient = new BuildingRecipeIngredientData(rItem, rQuantity);
				}

				rIngredients.Add(rIngredient);
			}
		}

		BuildingRecipeData result = new(rBuildingRecipeId, rBuilding, rIngredients);

		return result;
	}

	/// <summary>
	/// 建造物レシピを問い合わせします。
	/// </summary>
	/// <param name="buildingRecipeId">建造物レシピID</param>
	/// <returns>問い合わせした建造物レシピデータを返します。</returns>
	public async Task<BuildingRecipeData> QuerySingleAsync(string buildingRecipeId)
	{
		return await Task.Run(()=> QuerySingle(buildingRecipeId));
	}

	#endregion

	#region Nested types

	/// <summary>
	/// 建造物レシピのレコード
	/// </summary>
	/// <param name="BuildingRecipeId">建造物レシピID</param>
	/// <param name="BuildingId">建造物ID</param>
	/// <param name="BuildingName">建造物名</param>
	private record class BuildingRecipeRecord(string BuildingRecipeId, string BuildingId, string BuildingName);

	/// <summary>
	/// 建造物レシピ材料のレコード
	/// </summary>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	/// <param name="Quantity">数量</param>
	private record class BuildingRecipeIngredientRecord(string ItemId, string ItemName, long Quantity);

	#endregion
}

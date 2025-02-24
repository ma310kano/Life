using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 建造物の問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class BuildingQueryService(IConfiguration configuration) : IBuildingQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// 建造物を問い合わせします。
	/// </summary>
	/// <param name="buildingId">建造物ID</param>
	/// <returns>問い合わせした建造物データを返します。</returns>
	public BuildingData QuerySingle(string buildingId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		BuildingRecord source;
		{
			const string sql = @"SELECT
	  bui.building_id
	, bnm.building_name
FROM
	buildings bui
	INNER JOIN building_names bnm
		ON  bui.building_id = bnm.building_id
		AND bnm.language_code = :language_code
WHERE
	bui.building_id = :building_id";

			var param = new
			{
				building_id = buildingId,
				language_code = _languageCode,
			};

			source = connection.QuerySingle<BuildingRecord>(sql, param);
		}

		ItemRecipeSummaryData[] itemRecipes;
		{
			const string sql = @"SELECT
	  irc.item_recipe_id
	, inm.item_name
FROM
	item_recipes irc
	INNER JOIN items ite
		ON irc.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	irc.building_id = :building_id
ORDER BY
	irc.item_recipe_id";

			var param = new
			{
				building_id = buildingId,
				language_code = _languageCode,
			};

			itemRecipes = connection.Query<ItemRecipeSummaryData>(sql, param).ToArray();
		}

		BuildingData result = new(source.BuildingId, source.BuildingName, itemRecipes);

		return result;
	}

	/// <summary>
	/// 建造物を問い合わせします。
	/// </summary>
	/// <param name="buildingId">建造物ID</param>
	/// <returns>問い合わせした建造物データを返します。</returns>
	public Task<BuildingData> QuerySingleAsync(string buildingId)
	{
		return Task.Run(() => QuerySingle(buildingId));
	}

	#endregion

	#region Nested types

	/// <summary>
	/// 建造物のレコード
	/// </summary>
	/// <param name="BuildingId">建造物ID</param>
	/// <param name="BuildingName">建造物名</param>
	private record class BuildingRecord(string BuildingId, string BuildingName);

	#endregion
}

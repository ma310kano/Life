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

		BuildingData result = connection.QuerySingle<BuildingData>(sql, param);

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
}

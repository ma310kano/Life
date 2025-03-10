using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間のエリアを検索する機能
/// </summary>
/// <param name="humanId">人間ID</param>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class HumanAreaFinder(HumanId humanId, IDbConnection connection, IDbTransaction transaction) : IHumanAreaFinder
{
	#region Methods

	/// <summary>
	/// 存在しているか判定します。
	/// </summary>
	/// <param name="buildingId">建造物ID</param>
	/// <returns>存在している場合は、<c>true</c>。それ以外の場合は、<c>false</c>。</returns>
	public bool Exists(BuildingId buildingId)
	{
		const string sql = @"SELECT
	'1'
FROM
	humans hum
	INNER JOIN areas are
		ON hum.area_id = are.area_id
	INNER JOIN area_buildings abl
		ON are.area_id = abl.area_id
WHERE
		hum.human_id = :human_id
	AND abl.building_id = :building_id";

		var param = new
		{
			human_id = humanId.Value,
			building_id = buildingId.Value,
		};

		char c = connection.ExecuteScalar<char>(sql, param, transaction);

		bool result = c == '1';

		return result;
	}

	#endregion
}

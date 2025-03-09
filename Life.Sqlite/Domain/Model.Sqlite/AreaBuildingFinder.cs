using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// エリアの建造物を検索する機能
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class AreaBuildingFinder(IDbConnection connection, IDbTransaction transaction) : IAreaBuildingFinder
{
	#region Methods

	/// <summary>
	/// 存在しているか判定します。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <param name="buildingId">建造物ID</param>
	/// <returns>存在している場合は、<c>true</c>。それ以外の場合は、<c>false</c>。</returns>
	public bool Exists(AreaId areaId, BuildingId buildingId)
	{
		const string sql = @"SELECT
	'1'
FROM
	area_buildings
WHERE
		area_id = :area_id
	AND building_id = :building_id";

		var param = new
		{
			area_id = areaId.Value,
			building_id = buildingId.Value,
		};

		char c = connection.ExecuteScalar<char>(sql, param, transaction);

		bool result = c == '1';

		return result;
	}

	#endregion
}

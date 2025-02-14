using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// エリアの建造物のリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class AreaBuildingRepository(IDbConnection connection, IDbTransaction transaction) : IAreaBuildingRepository
{
	#region Methods

	/// <summary>
	/// 建造物を追加します。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <param name="buildingId">建造物ID</param>
	public void Add(AreaId areaId, BuildingId buildingId)
	{
		const string sql = @"INSERT INTO
	area_buildings
	(
		  area_id
		, building_id
	)
	VALUES
	(
		  :area_id
		, :building_id
	)";

		var param = new
		{
			area_id = areaId.Value,
			building_id = buildingId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

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

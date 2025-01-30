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

	#endregion
}

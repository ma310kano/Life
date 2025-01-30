namespace Life.Domain.Model;

/// <summary>
/// エリアの建造物のリポジトリー
/// </summary>
public interface IAreaBuildingRepository
{
	#region Methods

	/// <summary>
	/// 建造物を追加します。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <param name="buildingId">建造物ID</param>
	void Add(AreaId areaId, BuildingId buildingId);

	#endregion
}

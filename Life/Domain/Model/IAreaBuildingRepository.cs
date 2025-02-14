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

	/// <summary>
	/// 存在しているか判定します。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <param name="buildingId">建造物ID</param>
	/// <returns>存在している場合は、<c>true</c>。それ以外の場合は、<c>false</c>。</returns>
	bool Exists(AreaId areaId, BuildingId buildingId);

	#endregion
}

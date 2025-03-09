namespace Life.Domain.Model;

/// <summary>
/// エリアの建造物を検索する機能
/// </summary>
public interface IAreaBuildingFinder
{
	#region Methods

	/// <summary>
	/// 存在しているか判定します。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <param name="buildingId">建造物ID</param>
	/// <returns>存在している場合は、<c>true</c>。それ以外の場合は、<c>false</c>。</returns>
	bool Exists(AreaId areaId, BuildingId buildingId);

	#endregion
}

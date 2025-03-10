namespace Life.Domain.Model;

/// <summary>
/// 人間のエリアを検索する機能
/// </summary>
public interface IHumanAreaFinder
{
	#region Methods

	/// <summary>
	/// 存在しているか判定します。
	/// </summary>
	/// <param name="buildingId">建造物ID</param>
	/// <returns>存在している場合は、<c>true</c>。それ以外の場合は、<c>false</c>。</returns>
	bool Exists(BuildingId buildingId);

	#endregion
}

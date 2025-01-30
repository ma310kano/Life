using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// 建造物の問い合わせサービス
/// </summary>
public interface IBuildingQueryService
{
	#region Methods

	/// <summary>
	/// 建造物を問い合わせします。
	/// </summary>
	/// <param name="buildingId">建造物ID</param>
	/// <returns>問い合わせした建造物データを返します。</returns>
	BuildingData QuerySingle(string buildingId);

	/// <summary>
	/// 建造物を問い合わせします。
	/// </summary>
	/// <param name="buildingId">建造物ID</param>
	/// <returns>問い合わせした建造物データを返します。</returns>
	Task<BuildingData> QuerySingleAsync(string buildingId);

	#endregion
}

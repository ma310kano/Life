using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// エリアの問い合わせサービス
/// </summary>
public interface IAreaQueryService
{
	#region Methods

	/// <summary>
	/// エリアを問い合わせします。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <returns>問い合わせしたエリアデータを返します。</returns>
	AreaData QuerySingle(string areaId);

	/// <summary>
	/// エリアを問い合わせします。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <returns>問い合わせしたエリアデータを返します。</returns>
	Task<AreaData> QuerySingleAsync(string areaId);

	#endregion
}

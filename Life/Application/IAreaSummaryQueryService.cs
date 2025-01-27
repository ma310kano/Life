using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// エリア概要の問い合わせサービス
/// </summary>
public interface IAreaSummaryQueryService
{
	#region Methods

	/// <summary>
	/// エリア概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたエリア概要データのコレクションを返します。</returns>
	IEnumerable<AreaSummaryData> Query();

	/// <summary>
	/// エリア概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたエリア概要データのコレクションを返します。</returns>
	Task<IEnumerable<AreaSummaryData>> QueryAsync();

	#endregion
}

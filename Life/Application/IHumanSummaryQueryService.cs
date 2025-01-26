using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// 人間概要の問い合わせサービス
/// </summary>
public interface IHumanSummaryQueryService
{
	#region Methods

	/// <summary>
	/// 人間概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした人間概要データのコレクションを返します。</returns>
	IEnumerable<HumanSummaryData> Query();

	/// <summary>
	/// 人間概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした人間概要データのコレクションを返します。</returns>
	Task<IEnumerable<HumanSummaryData>> QueryAsync();

	#endregion
}

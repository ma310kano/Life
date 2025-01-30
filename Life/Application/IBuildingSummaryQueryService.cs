using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// 建造物概要の問い合わせサービス
/// </summary>
public interface IBuildingSummaryQueryService
{
	#region Methods

	/// <summary>
	/// 建造物概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした建造物概要データのコレクションを返します。</returns>
	IEnumerable<BuildingSummaryData> Query();

	/// <summary>
	/// 建造物概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした建造物概要データのコレクションを返します。</returns>
	Task<IEnumerable<BuildingSummaryData>> QueryAsync();

	#endregion
}

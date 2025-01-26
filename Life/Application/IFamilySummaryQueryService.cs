using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// 家族概要の問い合わせサービス
/// </summary>
public interface IFamilySummaryQueryService
{
	#region Methods

	/// <summary>
	/// 家族概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした家族概要データのコレクションを返します。</returns>
	IEnumerable<FamilySummaryData> Query();

	/// <summary>
	/// 家族概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした家族概要データのコレクションを返します。</returns>
	Task<IEnumerable<FamilySummaryData>> QueryAsync();

	#endregion
}

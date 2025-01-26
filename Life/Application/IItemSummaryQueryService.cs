using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// アイテム概要の問い合わせサービス
/// </summary>
public interface IItemSummaryQueryService
{
	#region Methods

	/// <summary>
	/// アイテム概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたアイテム概要データのコレクションを返します。</returns>
	IEnumerable<ItemSummaryData> Query();

	/// <summary>
	/// アイテム概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたアイテム概要データのコレクションを返します。</returns>
	Task<IEnumerable<ItemSummaryData>> QueryAsync();

	#endregion
}

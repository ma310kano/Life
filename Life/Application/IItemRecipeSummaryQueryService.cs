using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// アイテムのレシピ概要の問い合わせサービス
/// </summary>
public interface IItemRecipeSummaryQueryService
{
	#region Methods

	/// <summary>
	/// アイテムのレシピ概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたアイテムのレシピ概要データのコレクションを返します。</returns>
	IEnumerable<ItemRecipeSummaryData> Query();

	/// <summary>
	/// アイテムのレシピ概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたアイテムのレシピ概要データのコレクションを返します。</returns>
	Task<IEnumerable<ItemRecipeSummaryData>> QueryAsync();

	#endregion
}

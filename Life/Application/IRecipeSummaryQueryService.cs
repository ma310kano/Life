using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// レシピ概要の問い合わせサービス
/// </summary>
public interface IRecipeSummaryQueryService
{
	#region Methods

	/// <summary>
	/// レシピ概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたレシピ概要データのコレクションを返します。</returns>
	IEnumerable<RecipeSummaryData> Query();

	/// <summary>
	/// レシピ概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたレシピ概要データのコレクションを返します。</returns>
	Task<IEnumerable<RecipeSummaryData>> QueryAsync();

	#endregion
}

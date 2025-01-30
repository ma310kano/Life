using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// アイテムのレシピの問い合わせサービス
/// </summary>
public interface IItemRecipeQueryService
{
	#region Methods

	/// <summary>
	/// アイテムのレシピを問い合わせします。
	/// </summary>
	/// <param name="itemRecipeId">アイテムのレシピID</param>
	/// <returns>問い合わせしたアイテムのレシピデータを返します。</returns>
	ItemRecipeData QuerySingle(string itemRecipeId);

	/// <summary>
	/// アイテムのレシピを問い合わせします。
	/// </summary>
	/// <param name="itemRecipeId">アイテムのレシピID</param>
	/// <returns>問い合わせしたアイテムのレシピデータを返します。</returns>
	Task<ItemRecipeData> QuerySingleAsync(string itemRecipeId);

	#endregion
}

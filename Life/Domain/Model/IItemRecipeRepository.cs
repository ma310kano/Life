namespace Life.Domain.Model;

/// <summary>
/// アイテムレシピのリポジトリー
/// </summary>
public interface IItemRecipeRepository
{
	#region Methods

	/// <summary>
	/// アイテムレシピを検索します。
	/// </summary>
	/// <param name="itemRecipeId">アイテムレシピID</param>
	/// <returns>検索したアイテムレシピを返します。</returns>
	ItemRecipe Find(ItemRecipeId itemRecipeId);

	#endregion
}

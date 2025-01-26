using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// レシピの問い合わせサービス
/// </summary>
public interface IRecipeQueryService
{
	#region Methods

	/// <summary>
	/// レシピを問い合わせします。
	/// </summary>
	/// <param name="recipeId">レシピID</param>
	/// <returns>問い合わせしたレシピデータを返します。</returns>
	RecipeData QuerySingle(string recipeId);

	/// <summary>
	/// レシピを問い合わせします。
	/// </summary>
	/// <param name="recipeId">レシピID</param>
	/// <returns>問い合わせしたレシピデータを返します。</returns>
	Task<RecipeData> QuerySingleAsync(string recipeId);

	#endregion
}

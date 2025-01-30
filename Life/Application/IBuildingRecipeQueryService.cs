using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// 建造物レシピの問い合わせサービス
/// </summary>
public interface IBuildingRecipeQueryService
{
	#region Methods

	/// <summary>
	/// 建造物レシピを問い合わせします。
	/// </summary>
	/// <param name="buildingRecipeId">建造物レシピID</param>
	/// <returns>問い合わせした建造物レシピデータを返します。</returns>
	BuildingRecipeData QuerySingle(string buildingRecipeId);

	/// <summary>
	/// 建造物レシピを問い合わせします。
	/// </summary>
	/// <param name="buildingRecipeId">建造物レシピID</param>
	/// <returns>問い合わせした建造物レシピデータを返します。</returns>
	Task<BuildingRecipeData> QuerySingleAsync(string buildingRecipeId);

	#endregion
}

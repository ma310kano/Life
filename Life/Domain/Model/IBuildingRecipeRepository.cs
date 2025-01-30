namespace Life.Domain.Model;

/// <summary>
/// 建造物レシピのリポジトリー
/// </summary>
public interface IBuildingRecipeRepository
{
	#region Methods

	/// <summary>
	/// 建造物レシピを検索します。
	/// </summary>
	/// <param name="buildingRecipeId">建造物レシピID</param>
	/// <returns>検索した建造物レシピを返します。</returns>
	BuildingRecipe Find(BuildingRecipeId buildingRecipeId);

	#endregion
}

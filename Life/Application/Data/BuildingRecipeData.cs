namespace Life.Application.Data;

/// <summary>
/// 建造物レシピデータ
/// </summary>
/// <param name="BuildingRecipeId">建造物レシピID</param>
/// <param name="Building">建造物</param>
/// <param name="Ingredients">材料のコレクション</param>
public record class BuildingRecipeData(string BuildingRecipeId, BuildingSummaryData Building, IReadOnlyCollection<BuildingRecipeIngredientData> Ingredients);

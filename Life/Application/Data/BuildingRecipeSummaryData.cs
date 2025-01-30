namespace Life.Application.Data;

/// <summary>
/// 建造物レシピ概要データ
/// </summary>
/// <param name="BuildingRecipeId">建造物レシピID</param>
/// <param name="BuildingName">建造物名</param>
public record class BuildingRecipeSummaryData(string BuildingRecipeId, string BuildingName);

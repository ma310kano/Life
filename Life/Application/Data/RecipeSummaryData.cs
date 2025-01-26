namespace Life.Application.Data;

/// <summary>
/// レシピ概要データ
/// </summary>
/// <param name="RecipeId">レシピID</param>
/// <param name="ItemName">アイテム名</param>
public record class RecipeSummaryData(string RecipeId, string ItemName);

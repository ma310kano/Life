namespace Life.Application.Data;

/// <summary>
/// アイテムのレシピ概要データ
/// </summary>
/// <param name="ItemRecipeId">レシピID</param>
/// <param name="ItemName">アイテム名</param>
public record class ItemRecipeSummaryData(string ItemRecipeId, string ItemName);

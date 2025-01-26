namespace Life.Application.Data;

/// <summary>
/// レシピ材料データ
/// </summary>
/// <param name="Item">アイテム</param>
/// <param name="Quantity">数量</param>
public record class RecipeIngredientData(ItemSummaryData Item, int Quantity);

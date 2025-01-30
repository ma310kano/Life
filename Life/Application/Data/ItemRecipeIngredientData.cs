namespace Life.Application.Data;

/// <summary>
/// アイテムのレシピ材料データ
/// </summary>
/// <param name="Item">アイテム</param>
/// <param name="Quantity">数量</param>
public record class ItemRecipeIngredientData(ItemSummaryData Item, int Quantity);

namespace Life.Application.Data;

/// <summary>
/// 建造物レシピ材料データ
/// </summary>
/// <param name="Item">アイテム</param>
/// <param name="Quantity">数量</param>
public record class BuildingRecipeIngredientData(ItemSummaryData Item, int Quantity);

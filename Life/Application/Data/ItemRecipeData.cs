namespace Life.Application.Data;

/// <summary>
/// アイテムのレシピデータ
/// </summary>
/// <param name="ItemRecipeId">レシピID</param>
/// <param name="Item">アイテム</param>
/// <param name="Quantity">数量</param>
/// <param name="Building">建造物</param>
/// <param name="Ingredients">材料のコレクション</param>
public record class ItemRecipeData(string ItemRecipeId, ItemSummaryData Item, int Quantity, BuildingSummaryData? Building, IReadOnlyCollection<ItemRecipeIngredientData> Ingredients);

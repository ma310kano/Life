namespace Life.Application.Data;

/// <summary>
/// アイテムのレシピデータ
/// </summary>
/// <param name="ItemRecipeId">レシピID</param>
/// <param name="Item">アイテム</param>
/// <param name="Ingredients">材料のコレクション</param>
public record class ItemRecipeData(string ItemRecipeId, ItemSummaryData Item, IReadOnlyCollection<ItemRecipeIngredientData> Ingredients);

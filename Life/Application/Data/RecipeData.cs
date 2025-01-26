namespace Life.Application.Data;

/// <summary>
/// レシピデータ
/// </summary>
/// <param name="RecipeId">レシピID</param>
/// <param name="Item">アイテム</param>
/// <param name="Ingredients">材料のコレクション</param>
public record class RecipeData(string RecipeId, ItemSummaryData Item, IReadOnlyCollection<RecipeIngredientData> Ingredients);

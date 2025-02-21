namespace Life.Application.Data;

/// <summary>
/// 人間データ
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="FirstName">名</param>
/// <param name="Family">家族</param>
/// <param name="Area">エリア</param>
/// <param name="BuildingRecipes">建造物レシピのコレクション</param>
/// <param name="ItemRecipes">アイテムレシピのコレクション</param>
/// <param name="EquipmentItems">装備アイテムのコレクション</param>
/// <param name="InventorySlots">インベントリースロットのコレクション</param>
public record class HumanData(string HumanId, string FirstName, FamilySummaryData Family, AreaSummaryData Area, IReadOnlyCollection<BuildingRecipeSummaryData> BuildingRecipes, IReadOnlyCollection<ItemRecipeSummaryData> ItemRecipes, IReadOnlyCollection<IItemMatterData> EquipmentItems, IReadOnlyCollection<IItemMatterData> InventorySlots);

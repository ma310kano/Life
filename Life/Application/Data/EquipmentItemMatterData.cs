namespace Life.Application.Data;

/// <summary>
/// 装備アイテム物質データ
/// </summary>
/// <param name="ItemMatterId">アイテム物質ID</param>
/// <param name="Item">アイテム</param>
public record class EquipmentItemMatterData(string ItemMatterId, ItemSummaryData Item);

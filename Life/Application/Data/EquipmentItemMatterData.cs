namespace Life.Application.Data;

/// <summary>
/// 装備アイテム物質データ
/// </summary>
/// <param name="ItemMatterId">アイテム物質ID</param>
/// <param name="Item">アイテム</param>
/// <param name="Contents">内容のコレクション</param>
public record class EquipmentItemMatterData(string ItemMatterId, ItemSummaryData Item, IReadOnlyCollection<ContentItemMatterData> Contents);

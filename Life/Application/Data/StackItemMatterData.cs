namespace Life.Application.Data;

/// <summary>
/// スタックアイテム物質データ
/// </summary>
/// <param name="ItemMatterId">アイテム物質ID</param>
/// <param name="ItemId">アイテムID</param>
/// <param name="ItemName">アイテム名</param>
/// <param name="CanEquip">装備可能かどうか</param>
/// <param name="Quantity">数量</param>
public record class StackItemMatterData(string ItemMatterId, string ItemId, string ItemName, bool CanEquip, int Quantity) : IItemMatterData;

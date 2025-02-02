namespace Life.Application.Data;

/// <summary>
/// インベントリースロットデータ
/// </summary>
/// <param name="Item">アイテム</param>
/// <param name="Quantity">数量</param>
public record class InventorySlotData(ItemData Item, int Quantity);

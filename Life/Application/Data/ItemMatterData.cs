namespace Life.Application.Data;

/// <summary>
/// アイテム物質データ
/// </summary>
/// <param name="ItemMatterId">アイテム物質ID</param>
/// <param name="Item">アイテム</param>
/// <param name="Quantity">数量</param>
public record class ItemMatterData(string ItemMatterId, ItemData Item, int Quantity);

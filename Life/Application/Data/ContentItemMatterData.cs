namespace Life.Application.Data;

/// <summary>
/// 内容アイテム物質データ
/// </summary>
/// <param name="ItemMatterId">アイテム物質ID</param>
/// <param name="ItemId">アイテムID</param>
/// <param name="ItemName">アイテム名</param>
/// <param name="Quantity">数量</param>
public record class ContentItemMatterData(string ItemMatterId, string ItemId, string ItemName, int Quantity);

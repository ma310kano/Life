namespace Life.Application.Data;

/// <summary>
/// アイテムデータ
/// </summary>
/// <param name="ItemId">アイテムID</param>
/// <param name="ItemName">アイテム名</param>
public record class ItemData(string ItemId, string ItemName);

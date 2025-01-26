namespace Life.Application.Data;

/// <summary>
/// アイテム概要データ
/// </summary>
/// <param name="ItemId">アイテムID</param>
/// <param name="ItemName">アイテム名</param>
public record class ItemSummaryData(string ItemId, string ItemName);

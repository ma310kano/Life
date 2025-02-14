namespace Life.Application.Command;

/// <summary>
/// 人間の採集アイテムコマンド
/// </summary>
/// <param name="ItemId">アイテムID</param>
/// <param name="Quantity">数量</param>
public record class HumanGatheringItemCommand(string ItemId, int Quantity);

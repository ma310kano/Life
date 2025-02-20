namespace Life.Application.Command;

/// <summary>
/// 人間の採集流体アイテムのコマンド
/// </summary>
/// <param name="ItemId">アイテムID</param>
/// <param name="StorageItemMatterId">収納アイテム物質ID</param>
public record class HumanGatheringFluidItemCommand(string ItemId, string StorageItemMatterId);

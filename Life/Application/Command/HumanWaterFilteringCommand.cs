namespace Life.Application.Command;

/// <summary>
/// 人間の水濾過コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="StorageItemMatterId">収納アイテム物質ID</param>
public record class HumanWaterFilteringCommand(string HumanId, string StorageItemMatterId);

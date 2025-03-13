namespace Life.Application.Command;

/// <summary>
/// 人間の水濾過コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="SourceContainerItemMatterId">濾過元のコンテナーアイテム物質ID</param>
/// <param name="DestinationContainerItemMatterId">濾過先のコンテナーアイテム物質ID</param>
public record class HumanWaterFilteringCommand(string HumanId, string SourceContainerItemMatterId, string DestinationContainerItemMatterId);

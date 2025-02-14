namespace Life.Application.Command;

/// <summary>
/// 人間の採集コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="Items">アイテムのコレクション</param>
public record class HumanGatheringCommand(string HumanId, IReadOnlyCollection<HumanGatheringItemCommand> Items);

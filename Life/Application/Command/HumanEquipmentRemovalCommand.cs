namespace Life.Application.Command;

/// <summary>
/// 人間の装備解除コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="ItemId">アイテムID</param>
public record class HumanEquipmentRemovalCommand(string HumanId, string ItemId);

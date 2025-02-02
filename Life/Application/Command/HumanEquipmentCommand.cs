namespace Life.Application.Command;

/// <summary>
/// 人間の装備コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="ItemId">アイテムID</param>
public record class HumanEquipmentCommand(string HumanId, string ItemId);

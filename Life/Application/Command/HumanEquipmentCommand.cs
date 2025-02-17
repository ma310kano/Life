namespace Life.Application.Command;

/// <summary>
/// 人間の装備コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="ItemMatterId">アイテム物質ID</param>
public record class HumanEquipmentCommand(string HumanId, string ItemMatterId);

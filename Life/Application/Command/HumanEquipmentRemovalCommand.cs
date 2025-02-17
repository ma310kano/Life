namespace Life.Application.Command;

/// <summary>
/// 人間の装備解除コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="ItemMatterId">アイテム物質ID</param>
public record class HumanEquipmentRemovalCommand(string HumanId, string ItemMatterId);

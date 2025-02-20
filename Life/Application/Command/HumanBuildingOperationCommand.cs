namespace Life.Application.Command;

/// <summary>
/// 人間の建造物操作コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="BuildingId">建造物ID</param>
/// <param name="StorageItemMatterId">収納アイテム物質ID</param>
public record class HumanBuildingOperationCommand(string HumanId, string BuildingId, string StorageItemMatterId);

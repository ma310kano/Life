namespace Life.Application.Command;

/// <summary>
/// 人間のエリア移動コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="AreaId">エリアID</param>
public record class HumanAreaMovementCommand(string HumanId, string AreaId);

namespace Life.Application.Command;

/// <summary>
/// 人間のエリア建造物コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="BuildingRecipeId">建造物レシピID</param>
public record class HumanAreaBuildingCommand(string HumanId, string BuildingRecipeId);

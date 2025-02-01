namespace Life.Application.Command;

/// <summary>
/// 人間アイテム作成コマンド
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="ItemRecipeId">アイテムレシピID</param>
public record class HumanItemMakingCommand(string HumanId, string ItemRecipeId);

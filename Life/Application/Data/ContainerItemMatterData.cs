namespace Life.Application.Data;

/// <summary>
/// コンテナーアイテム物質データ
/// </summary>
/// <param name="ItemMatterId">アイテム物質ID</param>
/// <param name="ItemId">アイテムID</param>
/// <param name="ItemName">アイテム名</param>
/// <param name="CanEquip">装備可能かどうか</param>
/// <param name="Contents">内容のコレクション</param>
public record class ContainerItemMatterData(string ItemMatterId, string ItemId, string ItemName, bool CanEquip, IEnumerable<IItemMatterData> Contents) : IItemMatterData;

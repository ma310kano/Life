namespace Life.Application.Data;

/// <summary>
/// エリアアイテムデータ
/// </summary>
/// <param name="ItemId">アイテムID</param>
/// <param name="ItemName">アイテム名</param>
/// <param name="IsFluid">流体かどうか</param>
/// <param name="EquipmentItemIds">装備アイテムIDのコレクション</param>
public record class AreaItemData(string ItemId, string ItemName, bool IsFluid, IReadOnlyCollection<string> EquipmentItemIds);

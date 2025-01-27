namespace Life.Application.Data;

/// <summary>
/// エリアデータ
/// </summary>
/// <param name="AreaId">エリアID</param>
/// <param name="AreaName">エリア名</param>
/// <param name="Humans">人間のコレクション</param>
/// <param name="Items">アイテムのコレクション</param>
public record class AreaData(string AreaId, string AreaName, IReadOnlyCollection<HumanSummaryData> Humans, IReadOnlyCollection<ItemSummaryData> Items);

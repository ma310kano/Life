namespace Life.Application.Data;

/// <summary>
/// エリア概要データ
/// </summary>
/// <param name="AreaId">エリアID</param>
/// <param name="AreaName">エリア名</param>
public record class AreaSummaryData(string AreaId, string AreaName);

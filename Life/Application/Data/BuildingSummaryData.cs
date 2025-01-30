namespace Life.Application.Data;

/// <summary>
/// 建造物概要データ
/// </summary>
/// <param name="BuildingId">建造物ID</param>
/// <param name="BuildingName">建造物名</param>
public record class BuildingSummaryData(string BuildingId, string BuildingName);

namespace Life.Application.Data;

/// <summary>
/// 家族概要データ
/// </summary>
/// <param name="FamilyId">家族ID</param>
/// <param name="FamilyName">家族名</param>
public record class FamilySummaryData(string FamilyId, string FamilyName);

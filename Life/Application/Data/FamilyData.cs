namespace Life.Application.Data;

/// <summary>
/// 家族データ
/// </summary>
/// <param name="FamilyId">家族ID</param>
/// <param name="FamilyName">家族名</param>
public record class FamilyData(string FamilyId, string FamilyName);

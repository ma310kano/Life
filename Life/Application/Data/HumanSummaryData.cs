namespace Life.Application.Data;

/// <summary>
/// 人間概要データ
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="FirstName">名</param>
/// <param name="FamilyName">家族名</param>
public record class HumanSummaryData(string HumanId, string FirstName, string FamilyName);

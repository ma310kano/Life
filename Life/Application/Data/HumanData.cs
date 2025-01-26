namespace Life.Application.Data;

/// <summary>
/// 人間データ
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="FirstName">名</param>
/// <param name="Family">家族</param>
public record class HumanData(string HumanId, string FirstName, FamilySummaryData Family);

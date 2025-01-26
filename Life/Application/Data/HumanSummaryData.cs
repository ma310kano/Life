namespace Life.Application.Data;

/// <summary>
/// 人間概要データ
/// </summary>
/// <param name="HumanId">人間ID</param>
/// <param name="HumanName">人間名</param>
public record class HumanSummaryData(string HumanId, string HumanName);

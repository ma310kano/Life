using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 人間概要の問い合わせサービス
/// </summary>
public class HumanSummaryQueryService : IHumanSummaryQueryService
{
	#region Methods

	/// <summary>
	/// 人間概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした人間概要データのコレクションを返します。</returns>
	public IEnumerable<HumanSummaryData> Query()
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  hum.human_id
	, hum.first_name || ' ' || fam.family_name AS human_name
FROM
	humans hum
	INNER JOIN families fam
		ON hum.family_id = fam.family_id
ORDER BY
	hum.human_id";

		List<HumanSummaryData> results = connection.Query<HumanSummaryData>(sql).ToList();

		return results;
	}

	/// <summary>
	/// 人間概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした人間概要データのコレクションを返します。</returns>
	public async Task<IEnumerable<HumanSummaryData>> QueryAsync()
	{
		return await Task.Run(Query);
	}

	#endregion
}

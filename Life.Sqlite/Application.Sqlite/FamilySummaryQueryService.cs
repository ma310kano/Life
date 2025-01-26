using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 家族概要の問い合わせサービス
/// </summary>
public class FamilySummaryQueryService : IFamilySummaryQueryService
{
	#region Methods

	/// <summary>
	/// 家族概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした家族概要データのコレクションを返します。</returns>
	public IEnumerable<FamilySummaryData> Query()
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  family_id
	, family_name
FROM
	families
ORDER BY
	family_name";

		List<FamilySummaryData> results = connection.Query<FamilySummaryData>(sql).ToList();

		return results;
	}

	/// <summary>
	/// 家族概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした家族概要データのコレクションを返します。</returns>
	public async Task<IEnumerable<FamilySummaryData>> QueryAsync()
	{
		return await Task.Run(Query);
	}

	#endregion
}

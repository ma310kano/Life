using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 人間の問い合わせサービス
/// </summary>
public class HumanQueryService : IHumanQueryService
{
	#region Methods

	/// <summary>
	/// 人間を問い合わせします。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <returns>問い合わせした人間データを返します。</returns>
	public HumanData QuerySingle(string humanId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  hum.human_id
	, hum.first_name
	, hum.family_id
	, fam.family_name
FROM
	humans hum
	INNER JOIN families fam
		ON hum.family_id = fam.family_id
WHERE
	hum.human_id = :human_id";

		var param = new
		{
			human_id = humanId,
		};

		HumanRecord record = connection.QuerySingle<HumanRecord>(sql, param);

		HumanData result;
		{
			FamilySummaryData family = new(record.FamilyId, record.FamilyName);

			result = new HumanData(record.HumanId, record.FirstName, family);
		}

		return result;
	}

	/// <summary>
	/// 人間を問い合わせします。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <returns>問い合わせした人間データを返します。</returns>
	public async Task<HumanData> QuerySingleAsync(string humanId)
	{
		return await Task.Run(() => QuerySingle(humanId));
	}

	#endregion

	#region Nested types

	/// <summary>
	/// 人間のレコード
	/// </summary>
	/// <param name="HumanId">人間ID</param>
	/// <param name="FirstName">名</param>
	/// <param name="FamilyId">家族ID</param>
	/// <param name="FamilyName">家族名</param>
	private record class HumanRecord(string HumanId, string FirstName, string FamilyId, string FamilyName);

	#endregion
}

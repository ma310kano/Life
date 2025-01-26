using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 家族の問い合わせサービス
/// </summary>
public class FamilyQueryService : IFamilyQueryService
{
	#region Methods

	/// <summary>
	/// 家族を問い合わせします。
	/// </summary>
	/// <param name="familyId">家族ID</param>
	/// <returns>問い合わせした家族データを返します。</returns>
	public FamilyData QuerySingle(string familyId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  family_id
	, family_name
FROM
	families
WHERE
	family_id = :family_id";

		var param = new
		{
			family_id = familyId,
		};

		FamilyData result = connection.QuerySingle<FamilyData>(sql, param);

		return result;
	}

	/// <summary>
	/// 家族を問い合わせします。
	/// </summary>
	/// <param name="familyId">家族ID</param>
	/// <returns>問い合わせした家族データを返します。</returns>
	public Task<FamilyData> QuerySingleAsync(string familyId)
	{
		return Task.Run(() => QuerySingle(familyId));
	}

	#endregion
}

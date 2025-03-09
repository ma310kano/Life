using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間のリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class HumanRepository(HumanId humanId, IDbConnection connection, IDbTransaction transaction) : IHumanRepository
{
	#region Methods

	/// <summary>
	/// 人間を検索します。
	/// </summary>
	/// <returns>検索した人間を返します。</returns>
	public Human Find()
	{
		const string sql = @"SELECT
	  human_id
	, first_name
	, family_id
	, area_id
FROM
	humans
WHERE
	human_id = :human_id";

		var param = new
		{
			human_id = humanId.Value,
		};

		HumanRecord source = connection.QuerySingle<HumanRecord>(sql, param, transaction);

		Human result;
		{
			HumanId rHumanId = new(source.HumanId);
			FirstName rFirstName = new(source.FirstName);
			FamilyId rFamilyId = new(source.FamilyId);
			AreaId rAreaId = new(source.AreaId);

			result = new Human(rHumanId, rFirstName, rFamilyId, rAreaId);
		}

		return result;
	}

	/// <summary>
	/// 人間を保存します。
	/// </summary>
	/// <param name="human">人間</param>
	public void Save(Human human)
	{
		const string sql = @"INSERT INTO
	humans
	(
		  human_id
		, first_name
		, family_id
		, area_id
	)
	VALUES
	(
		  :human_id
		, :first_name
		, :family_id
		, :area_id
	)
	ON CONFLICT
	(
		human_id
	)
	DO UPDATE SET
		  first_name = :first_name
		, family_id = :family_id
		, area_id = :area_id";

		var param = new
		{
			human_id = human.HumanId.Value,
			first_name = human.FirstName.Value,
			family_id = human.FamilyId.Value,
			area_id = human.AreaId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	#endregion

	#region Nested types

	/// <summary>
	/// 人間のレコード
	/// </summary>
	/// <param name="HumanId">人間ID</param>
	/// <param name="FirstName">名</param>
	/// <param name="FamilyId">家族ID</param>
	/// <param name="AreaId">エリアID</param>
	private record class HumanRecord(string HumanId, string FirstName, string FamilyId, string AreaId);

	#endregion
}

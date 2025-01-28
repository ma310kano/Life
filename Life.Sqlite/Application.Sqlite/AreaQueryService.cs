using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// エリアの問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class AreaQueryService(IConfiguration configuration) : IAreaQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// エリアを問い合わせします。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <returns>問い合わせしたエリアデータを返します。</returns>
	public AreaData QuerySingle(string areaId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		AreaRecord record;
		{
			const string sql = @"SELECT
	  are.area_id
	, anm.area_name
FROM
	areas are
	INNER JOIN area_names anm
		ON  are.area_id = anm.area_id
		AND anm.language_code = :language_code
WHERE
	are.area_id = :area_id";

			var param = new
			{
				area_id = areaId,
				language_code = _languageCode,
			};

			record = connection.QuerySingle<AreaRecord>(sql, param);
		}

		List<HumanSummaryData> humans;
		{
			const string sql = @"SELECT
	  hum.human_id
	, hum.first_name
	, fam.family_name
FROM
	humans hum
	INNER JOIN families fam
		ON hum.family_id = fam.family_id
WHERE
	hum.area_id = :area_id
ORDER BY
	hum.human_id";

			var param = new
			{
				area_id = areaId,
			};

			humans = connection.Query<HumanSummaryData>(sql, param).ToList();
		}

		List<ItemSummaryData> items;
		{
			const string sql = @"SELECT
	  ait.item_id
	, inm.item_name
FROM
	area_items ait
	INNER JOIN items ite
		ON ait.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	ait.area_id = :area_id";

			var param = new
			{
				area_id = areaId,
				language_code = _languageCode,
			};

			items = connection.Query<ItemSummaryData>(sql, param).ToList();
		}

		AreaData result = new(record.AreaId, record.AreaName, humans, items);

		return result;
	}

	/// <summary>
	/// エリアを問い合わせします。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <returns>問い合わせしたエリアデータを返します。</returns>
	public async Task<AreaData> QuerySingleAsync(string areaId)
	{
		return await Task.Run(() => QuerySingle(areaId));
	}

	#endregion

	#region Nested types

	/// <summary>
	/// エリアのレコード
	/// </summary>
	/// <param name="AreaId">エリアID</param>
	/// <param name="AreaName">エリア名</param>
	private record class AreaRecord(string AreaId, string AreaName);

	#endregion
}

using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 建造物概要の問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class BuildingSummaryQueryService(IConfiguration configuration) : IBuildingSummaryQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// 建造物概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした建造物概要データのコレクションを返します。</returns>
	public IEnumerable<BuildingSummaryData> Query()
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  bui.building_id
	, bnm.building_name
FROM
	buildings bui
	INNER JOIN building_names bnm
		ON  bui.building_id = bnm.building_id
		AND bnm.language_code = :language_code
ORDER BY
	bui.building_id";

		var param = new
		{
			language_code = _languageCode,
		};

		List<BuildingSummaryData> results = connection.Query<BuildingSummaryData>(sql, param).ToList();

		return results;
	}

	/// <summary>
	/// 建造物概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせした建造物概要データのコレクションを返します。</returns>
	public Task<IEnumerable<BuildingSummaryData>> QueryAsync()
	{
		return Task.Run(Query);
	}

	#endregion
}

using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// エリア概要の問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class AreaSummaryQueryService(IConfiguration configuration) : IAreaSummaryQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// エリア概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたエリア概要データのコレクションを返します。</returns>
	public IEnumerable<AreaSummaryData> Query()
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  are.area_id
	, anm.area_name
FROM
	areas are
	INNER JOIN area_names anm
		ON  are.area_id = anm.area_id
		AND anm.language_code = :language_code
ORDER BY
	are.area_id";

		var param = new
		{
			language_code = _languageCode,
		};

		List<AreaSummaryData> results = connection.Query<AreaSummaryData>(sql, param).ToList();

		return results;
	}

	/// <summary>
	/// エリア概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたエリア概要データのコレクションを返します。</returns>
	public async Task<IEnumerable<AreaSummaryData>> QueryAsync()
	{
		return await Task.Run(Query);
	}

	#endregion
}

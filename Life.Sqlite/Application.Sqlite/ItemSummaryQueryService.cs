using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// アイテム概要の問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class ItemSummaryQueryService(IConfiguration configuration) : IItemSummaryQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// アイテム概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたアイテム概要データのコレクションを返します。</returns>
	public IEnumerable<ItemSummaryData> Query()
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  ite.item_id
	, inm.item_name
FROM
	items ite
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
ORDER BY
	ite.item_id";

		var param = new
		{
			language_code = _languageCode,
		};

		List<ItemSummaryData> results = connection.Query<ItemSummaryData>(sql, param).ToList();

		return results;
	}

	/// <summary>
	/// アイテム概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたアイテム概要データのコレクションを返します。</returns>
	public async Task<IEnumerable<ItemSummaryData>> QueryAsync()
	{
		return await Task.Run(Query);
	}

	#endregion
}

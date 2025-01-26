using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// レシピ概要の問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class RecipeSummaryQueryService(IConfiguration configuration) : IRecipeSummaryQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// レシピ概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたレシピ概要データのコレクションを返します。</returns>
	public IEnumerable<RecipeSummaryData> Query()
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  rec.recipe_id
	, inm.item_name
FROM
	recipes rec
	INNER JOIN items ite
		ON rec.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
ORDER BY
	rec.recipe_id";

		var param = new
		{
			language_code = _languageCode,
		};

		List<RecipeSummaryData> results = connection.Query<RecipeSummaryData>(sql, param).ToList();

		return results;
	}

	/// <summary>
	/// レシピ概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたレシピ概要データのコレクションを返します。</returns>
	public async Task<IEnumerable<RecipeSummaryData>> QueryAsync()
	{
		return await Task.Run(Query);
	}

	#endregion
}

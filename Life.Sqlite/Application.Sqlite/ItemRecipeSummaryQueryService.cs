using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// アイテムのレシピ概要の問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class ItemRecipeSummaryQueryService(IConfiguration configuration) : IItemRecipeSummaryQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// アイテムのレシピ概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたアイテムのレシピ概要データのコレクションを返します。</returns>
	public IEnumerable<ItemRecipeSummaryData> Query()
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  irc.item_recipe_id
	, inm.item_name
FROM
	item_recipes irc
	INNER JOIN items ite
		ON irc.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
ORDER BY
	irc.item_recipe_id";

		var param = new
		{
			language_code = _languageCode,
		};

		List<ItemRecipeSummaryData> results = connection.Query<ItemRecipeSummaryData>(sql, param).ToList();

		return results;
	}

	/// <summary>
	/// アイテムのレシピ概要を問い合わせします。
	/// </summary>
	/// <returns>問い合わせしたアイテムのレシピ概要データのコレクションを返します。</returns>
	public async Task<IEnumerable<ItemRecipeSummaryData>> QueryAsync()
	{
		return await Task.Run(Query);
	}

	#endregion
}

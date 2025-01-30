using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 建造物レシピ概要の問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class BuildingRecipeSummaryQueryService(IConfiguration configuration) : IBuildingRecipeSummaryQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// 建造物レシピ概要を問い合わせします。
	/// </summary>
	/// <returns>建造物レシピ概要データのコレクションを返します。</returns>
	public IEnumerable<BuildingRecipeSummaryData> Query()
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  brc.building_recipe_id
	, bnm.building_name
FROM
	building_recipes brc
	INNER JOIN buildings bui
		ON brc.building_id = bui.building_id
	INNER JOIN building_names bnm
		ON  bui.building_id = bnm.building_id
		AND bnm.language_code = :language_code
ORDER BY
	brc.building_recipe_id";

		var param = new
		{
			language_code = _languageCode,
		};

		List<BuildingRecipeSummaryData> results = connection.Query<BuildingRecipeSummaryData>(sql, param).ToList();

		return results;
	}

	/// <summary>
	/// 建造物レシピ概要を問い合わせします。
	/// </summary>
	/// <returns>建造物レシピ概要データのコレクションを返します。</returns>
	public async Task<IEnumerable<BuildingRecipeSummaryData>> QueryAsync()
	{
		return await Task.Run(Query);
	}

	#endregion
}

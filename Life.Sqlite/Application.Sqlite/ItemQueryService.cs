﻿using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// アイテムの問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class ItemQueryService(IConfiguration configuration) : IItemQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// アイテムを問い合わせします。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <returns>問い合わせしたアイテムデータを返します。</returns>
	public ItemData QuerySingle(string itemId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		const string sql = @"SELECT
	  ite.item_id
	, inm.item_name
	, ite.can_equip
FROM
	items ite
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND language_code = :language_code
WHERE
	ite.item_id = :item_id";

		var param = new
		{
			item_id = itemId,
			language_code = _languageCode,
		};

		ItemRecord source = connection.QuerySingle<ItemRecord>(sql, param);

		ItemData result;
		{
			bool canEquip = source.CanEquip == "1";

			result = new ItemData(source.ItemId, source.ItemName, canEquip);
		}

		return result;
	}

	/// <summary>
	/// アイテムを問い合わせします。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <returns>問い合わせしたアイテムデータを返します。</returns>
	public async Task<ItemData> QuerySingleAsync(string itemId)
	{
		return await Task.Run(() => QuerySingle(itemId));
	}

	#endregion

	#region Nested types

	/// <summary>
	/// アイテムのレコード
	/// </summary>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	/// <param name="CanEquip">装備できるか</param>
	private record class ItemRecord(string ItemId, string ItemName, string CanEquip);

	#endregion
}

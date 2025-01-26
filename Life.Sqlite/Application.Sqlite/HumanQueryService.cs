﻿using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 人間の問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class HumanQueryService(IConfiguration configuration) : IHumanQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// 人間を問い合わせします。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <returns>問い合わせした人間データを返します。</returns>
	public HumanData QuerySingle(string humanId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		HumanRecord record;
		FamilySummaryData family;
		{
			const string sql = @"SELECT
	  hum.human_id
	, hum.first_name
	, hum.family_id
	, fam.family_name
FROM
	humans hum
	INNER JOIN families fam
		ON hum.family_id = fam.family_id
WHERE
	hum.human_id = :human_id";

			var param = new
			{
				human_id = humanId,
			};

			record = connection.QuerySingle<HumanRecord>(sql, param);

			family = new FamilySummaryData(record.FamilyId, record.FamilyName);
		}

		List<InventorySlotData> inventorySlots = [];
		{
			const string sql = @"SELECT
	  his.item_id
	, inm.item_name
	, his.quantity
FROM
	human_inventory_slots his
	INNER JOIN items ite
		ON his.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	his.human_id = :human_id
ORDER BY
	  his.human_id
	, his.item_id";

			var param = new
			{
				human_id = humanId,
				language_code = _languageCode,
			};

			IEnumerable<InventorySlotRecord> sources = connection.Query<InventorySlotRecord>(sql, param);

			foreach (InventorySlotRecord source in sources)
			{
				InventorySlotData inventorySlot;
				{
					ItemSummaryData item = new(source.ItemId, source.ItemName);
					int quantity = (int)source.Quantity;

					inventorySlot = new InventorySlotData(item, quantity);
				}

				inventorySlots.Add(inventorySlot);
			}
		}

		HumanData result = new(record.HumanId, record.FirstName, family, inventorySlots);

		return result;
	}

	/// <summary>
	/// 人間を問い合わせします。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <returns>問い合わせした人間データを返します。</returns>
	public async Task<HumanData> QuerySingleAsync(string humanId)
	{
		return await Task.Run(() => QuerySingle(humanId));
	}

	#endregion

	#region Nested types

	/// <summary>
	/// 人間のレコード
	/// </summary>
	/// <param name="HumanId">人間ID</param>
	/// <param name="FirstName">名</param>
	/// <param name="FamilyId">家族ID</param>
	/// <param name="FamilyName">家族名</param>
	private record class HumanRecord(string HumanId, string FirstName, string FamilyId, string FamilyName);

	/// <summary>
	/// インベントリースロットのレコード
	/// </summary>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	/// <param name="Quantity">数量</param>
	private record class InventorySlotRecord(string ItemId, string ItemName, long Quantity);

	#endregion
}

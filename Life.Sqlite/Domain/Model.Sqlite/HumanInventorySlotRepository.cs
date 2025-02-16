using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間のインベントリースロットのリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class HumanInventorySlotRepository(IDbConnection connection, IDbTransaction transaction) : IHumanInventorySlotRepository
{
	#region Methods

	/// <summary>
	/// 人間のインベントリースロットを削除します。
	/// </summary>
	/// <param name="humanInventorySlot">人間のインベントリースロット</param>
	public void Delete(HumanInventorySlot humanInventorySlot)
	{
		Delete(humanInventorySlot.HumanId, humanInventorySlot.ItemId);
	}

	/// <summary>
	/// 人間のインベントリースロットを削除します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	public void Delete(HumanId humanId, ItemId itemId)
	{
		string[] itemMatterIds;
		{
			const string sql = @"SELECT
	him.item_matter_id
FROM
	human_item_matters him
	INNER JOIN item_matters imt
		ON him.item_matter_id = imt.item_matter_id
WHERE
		him.human_id = :human_id
	AND imt.item_id = :item_id";

			var param = new
			{
				human_id = humanId.Value,
				item_id = itemId.Value,
			};

			itemMatterIds = connection.Query<string>(sql, param, transaction).ToArray();
		}

		{
			const string sql = @"DELETE FROM
	human_item_matters
WHERE
		human_id = :human_id
	AND item_matter_id IN :item_matter_ids";

			var param = new
			{
				human_id = humanId.Value,
				item_matter_ids = itemMatterIds,
			};

			connection.Execute(sql, param, transaction);
		}
	}

	/// <summary>
	/// 人間のインベントリースロットを検索します。
	/// </summary>
	/// <param name="humainId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索した人間のインベントリースロットを返します。</returns>
	public HumanInventorySlot? FindOrDefault(HumanId humainId, ItemId itemId)
	{
		const string sql = @"SELECT
	  him.human_id
	, him.item_matter_id
	, imt.item_id
	, imt.quantity
FROM
	human_item_matters him
	INNER JOIN item_matters imt
		ON him.item_matter_id = imt.item_matter_id
WHERE
		him.human_id = :human_id
	AND imt.item_id = :item_id";

		var param = new
		{
			human_id = humainId.Value,
			item_id = itemId.Value,
		};

		SlotRecord? source = connection.QuerySingleOrDefault<SlotRecord>(sql, param, transaction);
		if (source is null) return null;

		HumanInventorySlot result;
		{
			HumanId rHumanId = new(source.HumanId);
			ItemMatterId rItemMatterId = new(source.ItemMatterId);
			ItemId rItemId = new(source.ItemId);
			Quantity rQuantity = new((int)source.Quantity);

			result = new HumanInventorySlot(rHumanId, rItemMatterId, rItemId, rQuantity);
		}

		return result;
	}

	/// <summary>
	/// 人間のインベントリースロットを保存します。
	/// </summary>
	/// <param name="humanInventorySlot">人間のインベントリースロット</param>
	public void Save(HumanInventorySlot humanInventorySlot)
	{
		{
			const string sql = @"INSERT INTO
	human_item_matters
	(
		  human_id
		, item_matter_id
	)
	VALUES
	(
		  :human_id
		, :item_matter_id
	)
	ON CONFLICT
	(
		  human_id
		, item_matter_id
	)
	DO NOTHING";

			var param = new
			{
				human_id = humanInventorySlot.HumanId.Value,
				item_matter_id = humanInventorySlot.ItemMatterId.Value,
			};

			connection.Execute(sql, param, transaction);
		}

		{
			const string sql = @"INSERT INTO
	item_matters
	(
		  item_matter_id
		, item_id
		, quantity
	)
	VALUES
	(
		  :item_matter_id
		, :item_id
		, :quantity
	)
	ON CONFLICT
	(
		item_matter_id
	)
	DO UPDATE SET
		quantity = :quantity";

			var param = new
			{
				item_matter_id = humanInventorySlot.ItemMatterId.Value,
				item_id = humanInventorySlot.ItemId.Value,
				quantity = humanInventorySlot.Quantity.Value,
			};

			connection.Execute(sql, param, transaction);
		}
	}

	#endregion

	#region Nested types

	/// <summary>
	/// スロットのレコード
	/// </summary>
	/// <param name="HumanId">人間ID</param>
	/// <param name="ItemMatterId">アイテム物質ID</param>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="Quantity">数量</param>
	private record class SlotRecord(string HumanId, string ItemMatterId, string ItemId, long Quantity);

	#endregion
}

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
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	public void Delete(HumanId humanId, ItemId itemId)
	{
		const string sql = @"DELETE FROM
	human_inventory_slots
WHERE
		human_id = :human_id
	AND item_id = :item_id";

		var param = new
		{
			human_id = humanId.Value,
			item_id = itemId.Value,
		};

		connection.Execute(sql, param, transaction);
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
	  human_id
	, item_id
	, quantity
FROM
	human_inventory_slots
WHERE
		human_id = :human_id
	AND item_id = :item_id";

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
			ItemId rItemId = new(source.ItemId);
			Quantity rQuantity = new((int)source.Quantity);

			result = new HumanInventorySlot(rHumanId, rItemId, rQuantity);
		}

		return result;
	}

	/// <summary>
	/// 人間のインベントリースロットを保存します。
	/// </summary>
	/// <param name="humanInventorySlot">人間のインベントリースロット</param>
	public void Save(HumanInventorySlot humanInventorySlot)
	{
		const string sql = @"INSERT INTO
	human_inventory_slots
	(
		  human_id
		, item_id
		, quantity
	)
	VALUES
	(
		  :human_id
		, :item_id
		, :quantity
	)
	ON CONFLICT
	(
		  human_id
		, item_id
	)
	DO UPDATE SET
		quantity = :quantity";

		var param = new
		{
			human_id = humanInventorySlot.HumanId.Value,
			item_id = humanInventorySlot.ItemId.Value,
			quantity = humanInventorySlot.Quantity.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	#endregion

	#region Nested types

	/// <summary>
	/// スロットのレコード
	/// </summary>
	/// <param name="HumanId">人間ID</param>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="Quantity">数量</param>
	private record class SlotRecord(string HumanId, string ItemId, long Quantity);

	#endregion
}

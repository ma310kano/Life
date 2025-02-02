using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 装備アイテムのリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class EquipmentItemRepository(IDbConnection connection, IDbTransaction transaction) : IEquipmentItemRepository
{
	#region Methods

	/// <summary>
	/// 装備アイテムを削除します。
	/// </summary>
	/// <param name="item">アイテム</param>
	public void Delete(EquipmentItem item)
	{
		const string sql = @"DELETE FROM
	human_equipment_items
WHERE
		human_id = :human_id
	AND item_id = :item_id";

		var param = new
		{
			human_id = item.HumanId.Value,
			item_id = item.ItemId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	/// <summary>
	/// 装備アイテムを検索します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索した装備アイテムを返します。</returns>
	public EquipmentItem Find(HumanId humanId, ItemId itemId)
	{
		const string sql = @"SELECT
	  human_id
	, item_id
FROM
	human_equipment_items
WHERE
		human_id = :human_id
	AND item_id = :item_id";

		var param = new
		{
			human_id = humanId.Value,
			item_id = itemId.Value,
		};

		EquipmentItemRecord source = connection.QuerySingle<EquipmentItemRecord>(sql, param, transaction);

		EquipmentItem result;
		{
			HumanId rHumanId = new(source.HumanId);
			ItemId rItemId = new(source.ItemId);

			result = new EquipmentItem(rHumanId, rItemId);
		}

		return result;
	}

	/// <summary>
	/// 装備アイテムを保存します。
	/// </summary>
	/// <param name="item">アイテム</param>
	public void Save(EquipmentItem item)
	{
		const string sql = @"INSERT INTO
	human_equipment_items
	(
		  human_id
		, item_id
	)
	VALUES
	(
		  :human_id
		, :item_id
	)";

		var param = new
		{
			human_id = item.HumanId.Value,
			item_id = item.ItemId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	#endregion

	#region Nested types

	/// <summary>
	/// 装備アイテムのレコード
	/// </summary>
	/// <param name="HumanId">人間ID</param>
	/// <param name="ItemId">アイテムID</param>
	private record class EquipmentItemRecord(string HumanId, string ItemId);

	#endregion
}

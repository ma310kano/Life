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
}

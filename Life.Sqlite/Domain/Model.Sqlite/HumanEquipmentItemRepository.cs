using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間の装備アイテムのリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class HumanEquipmentItemRepository(HumanId humanId, IDbConnection connection, IDbTransaction transaction) : IHumanEquipmentItemRepository
{
	#region Methods

	/// <summary>
	/// 装備アイテムを保存します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	public void Add(ItemMatterId itemMatterId)
	{
		const string sql = @"INSERT INTO
	human_equipment_items
	(
		  human_id
		, item_matter_id
	)
	VALUES
	(
		  :human_id
		, :item_matter_id
	)";

		var param = new
		{
			human_id = humanId.Value,
			item_matter_id = itemMatterId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	/// <summary>
	/// 装備アイテムを除去します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	public void Remove(ItemMatterId itemMatterId)
	{
		const string sql = @"DELETE FROM
	human_equipment_items
WHERE
		human_id = :human_id
	AND item_matter_id = :item_matter_id";

		var param = new
		{
			human_id = humanId.Value,
			item_matter_id = itemMatterId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	#endregion
}

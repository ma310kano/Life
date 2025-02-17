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
	/// インベントリースロットを追加します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	public void Add(HumanId humanId, ItemMatterId itemMatterId)
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
			human_id = humanId.Value,
			item_matter_id = itemMatterId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	/// <summary>
	/// 人間のインベントリースロットを除去します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	public void Remove(HumanId humanId, ItemMatterId itemMatterId)
	{
		const string sql = @"DELETE FROM
	human_item_matters
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

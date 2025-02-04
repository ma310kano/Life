using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間の採集アイテムのリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class HumanGatheringItemRepository(IDbConnection connection, IDbTransaction transaction) : IHumanGatheringItemRepository
{
	#region Methods

	/// <summary>
	/// 追加します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	public void Add(HumanId humanId, ItemId itemId)
	{
		const string sql = @"INSERT INTO
	human_gathering_items
	(
		  human_id
		, item_id
	)
	SELECT
		  :human_id
		, igi.gathering_item_id AS item_id
	FROM
		item_gathering_items igi
	WHERE
		igi.item_id = :item_id";

		var param = new
		{
			human_id = humanId.Value,
			item_id = itemId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	/// <summary>
	/// 除去します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	public void Remove(HumanId humanId, ItemId itemId)
	{
		const string sql = @"DELETE FROM
	human_gathering_items
WHERE
		human_id = :human_id
	AND item_id = 
	(
		SELECT
			gathering_item_id
		FROM
			item_gathering_items
		WHERE
			item_id = :item_id
	)";

		var param = new
		{
			human_id = humanId.Value,
			item_id = itemId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	#endregion
}

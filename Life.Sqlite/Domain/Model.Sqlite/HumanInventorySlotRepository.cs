using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間のインベントリースロットのリポジトリー
/// </summary>
/// <param name="humanId">人間ID</param>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class HumanInventorySlotRepository(HumanId humanId, IDbConnection connection, IDbTransaction transaction) : IHumanInventorySlotRepository
{
	#region Methods

	/// <summary>
	/// インベントリースロットを追加します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	public void Add(ItemMatterId itemMatterId)
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
	/// <param name="itemMatterId">アイテム物質ID</param>
	public void Remove(ItemMatterId itemMatterId)
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

	/// <summary>
	/// 検索します。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索したアイテム物質のコレクションを返します。</returns>
	public IEnumerable<ItemMatter> Find(ItemId itemId)
	{
		const string sql = @"SELECT
	  him.item_matter_id
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
			human_id = humanId.Value,
			item_id = itemId.Value,
		};

		IEnumerable<ItemMatterRecord> sources = connection.Query<ItemMatterRecord>(sql, param, transaction);

		List<ItemMatter> results = [];
		foreach (ItemMatterRecord source in sources)
		{
			ItemMatter result;
			{
				ItemMatterId rItemMatterId = new(source.ItemMatterId);
				ItemId rItemId = new(source.ItemId);
				Quantity rQuantity = new((int)source.Quantity);

				result = new ItemMatter(rItemMatterId, rItemId, rQuantity);
			}

			results.Add(result);
		}

		return results;
	}

	#endregion

	#region Nested types

	/// <summary>
	/// アイテム物質のレコード
	/// </summary>
	/// <param name="ItemMatterId">アイテム物質ID</param>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="Quantity">数量</param>
	private record class ItemMatterRecord(string ItemMatterId, string ItemId, long Quantity);

	#endregion
}

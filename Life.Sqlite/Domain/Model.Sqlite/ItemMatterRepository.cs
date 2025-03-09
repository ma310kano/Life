using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// アイテム物質のリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class ItemMatterRepository(IDbConnection connection, IDbTransaction transaction) : IItemMatterRepository
{
	#region Methods

	/// <summary>
	/// アイテムの中にアイテムを追加します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	public void AddInItem(ItemMatterId storageItemMatterId, ItemMatterId itemMatterId)
	{
		const string sql = @"INSERT INTO
	human_item_matters_item_matters
	(
		  item_matter_id
		, content_item_matter_id
	)
	VALUES
	(
		  :item_matter_id
		, :content_item_matter_id
	)";

		var param = new
		{
			item_matter_id = storageItemMatterId.Value,
			content_item_matter_id = itemMatterId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	/// <summary>
	/// アイテム物質を削除します。
	/// </summary>
	/// <param name="itemMatter">アイテム物質</param>
	public void Delete(ItemMatter itemMatter)
	{
		const string sql = @"DELETE FROM
	item_matters
WHERE
	item_matter_id = :item_matter_id";

		var param = new
		{
			item_matter_id = itemMatter.ItemMatterId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	/// <summary>
	/// アイテムの中を検索します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索したアイテム物質を返します。</returns>
	public ItemMatter FindInItem(ItemMatterId storageItemMatterId, ItemId itemId)
	{
		const string sql = @"SELECT
	  hii.content_item_matter_id AS item_matter_id
	, imt.item_id
	, imt.quantity
FROM
	human_item_matters_item_matters hii
	INNER JOIN item_matters imt
		ON hii.content_item_matter_id = imt.item_matter_id
WHERE
		hii.item_matter_id = :item_matter_id
	AND imt.item_id = :item_id";

		var param = new
		{
			item_matter_id = storageItemMatterId.Value,
			item_id = itemId.Value,
		};

		ItemMatterRecord source = connection.QuerySingle<ItemMatterRecord>(sql, param, transaction);

		ItemMatter result;
		{
			ItemMatterId rItemMatterId = new(source.ItemMatterId);
			ItemId rItemId = new(source.ItemId);
			Quantity rQuantity = new((int)source.Quantity);

			result = new ItemMatter(rItemMatterId, rItemId, rQuantity);
		}

		return result;
	}

	/// <summary>
	/// アイテム物質を検索します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	/// <returns>検索したアイテム物質を返します。</returns>
	public ItemMatter? FindOrDefault(ItemMatterId itemMatterId)
	{
		const string sql = @"SELECT
	  item_matter_id
	, item_id
	, quantity
FROM
	item_matters
WHERE
	item_matter_id = :item_matter_id";

		var param = new
		{
			item_matter_id = itemMatterId.Value,
		};

		ItemMatterRecord? source = connection.QuerySingleOrDefault<ItemMatterRecord>(sql, param, transaction);
		if (source is null) return null;

		ItemMatter result;
		{
			ItemMatterId rItemMatterId = new(source.ItemMatterId);
			ItemId rItemId = new(source.ItemId);
			Quantity rQuantity = new((int)source.Quantity);

			result = new ItemMatter(rItemMatterId, rItemId, rQuantity);
		}

		return result;
	}

	/// <summary>
	/// アイテムの中からアイテムを除去します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	public void RemoveInItem(ItemMatterId storageItemMatterId, ItemMatterId itemMatterId)
	{
		const string sql = @"DELETE FROM
	human_item_matters_item_matters
WHERE
		item_matter_id = :item_matter_id
	AND content_item_matter_id = :content_item_matter_id";

		var param = new
		{
			item_matter_id = storageItemMatterId.Value,
			content_item_matter_id = itemMatterId.Value,
		};

		connection.Execute(sql, param, transaction);
	}

	/// <summary>
	/// アイテム物質を保存します。
	/// </summary>
	/// <param name="itemMatter">アイテム物質</param>
	public void Save(ItemMatter itemMatter)
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
			item_matter_id = itemMatter.ItemMatterId.Value,
			item_id = itemMatter.ItemId.Value,
			quantity = itemMatter.Quantity.Value,
		};

		connection.Execute(sql, param, transaction);
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

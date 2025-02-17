﻿using Dapper;
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
	/// 人間のインベントリー内を検索します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索したアイテム物質のコレクションを返します。</returns>
	public IEnumerable<ItemMatter> FindInHumanInventory(HumanId humanId, ItemId itemId)
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

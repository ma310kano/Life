using Dapper;
using Life.Application.Data;
using System.Data;

namespace Life.Application.Sqlite.Functions;

/// <summary>
/// アイテム物質の問い合わせ機能
/// </summary>
/// <param name="languageCode">言語コード</param>
/// <param name="humanId">人間ID</param>
/// <param name="connection">コネクション</param>
internal class ItemMatterQuerier(string languageCode, string humanId, IDbConnection connection)
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = languageCode;

	/// <summary>
	/// 人間ID
	/// </summary>
	private readonly string _humanId = humanId;

	/// <summary>
	/// コネクション
	/// </summary>
	private readonly IDbConnection _connection = connection;

	#endregion

	#region Methods

	/// <summary>
	/// アイテム物質を問い合わせします。
	/// </summary>
	/// <param name="sql">SQL</param>
	/// <param name="contentSql">内容のSQL</param>
	/// <returns>問い合わせしたアイテム物質データのコレクションを返します。</returns>
	public IReadOnlyCollection<IItemMatterData> Query(string tableName)
	{
		List<IItemMatterData> results = [];
		{
			IEnumerable<ItemMatterRecord> sources;
			{
				const string sqlTemplate = @"SELECT
	  him.item_matter_id
	, imt.item_id
	, inm.item_name
	, ite.is_container
	, ite.can_stack
	, ite.can_equip
	, imt.quantity
FROM
	{0} him
	INNER JOIN item_matters imt
		ON him.item_matter_id = imt.item_matter_id
	INNER JOIN items ite
		ON imt.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	him.human_id = :human_id
ORDER BY
	  him.human_id
	, imt.item_id
	, him.item_matter_id";

				string sql = string.Format(sqlTemplate, tableName);

				var param = new
				{
					human_id = _humanId,
					language_code = _languageCode,
				};

				sources = _connection.Query<ItemMatterRecord>(sql, param);
			}

			IEnumerable<ContentItemMatterRecord>? allContentSources;
			if (sources.Any(x => x.IsContainer))
			{
				const string sqlTemplate = @"SELECT
	  hii.item_matter_id AS container_item_matter_id
	, hii.content_item_matter_id AS item_matter_id
	, imt.item_id
	, inm.item_name
	, ite.can_stack
	, ite.can_equip
	, imt.quantity
FROM
	{0} him
	INNER JOIN human_item_matters_item_matters hii
		ON him.item_matter_id = hii.item_matter_id
	INNER JOIN item_matters imt
		ON hii.content_item_matter_id = imt.item_matter_id
	INNER JOIN items ite
		ON imt.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND language_code = :language_code
WHERE
	him.human_id = :human_id
ORDER BY
	  hii.item_matter_id
	, ite.item_id
	, hii.content_item_matter_id";

				string sql = string.Format(sqlTemplate, tableName);

				var param = new
				{
					human_id = _humanId,
					language_code = _languageCode,
				};

				allContentSources = _connection.Query<ContentItemMatterRecord>(sql, param);
			}
			else
			{
				allContentSources = null;
			}

			foreach (ItemMatterRecord source in sources)
			{
				IItemMatterData result;
				{
					if (source.IsContainer)
					{
						List<IItemMatterData> contents = [];
						IEnumerable<ContentItemMatterRecord> contentSources = allContentSources!.Where(x => x.ContainerItemMatterId == source.ItemMatterId);
						foreach (ContentItemMatterRecord contentSource in contentSources)
						{
							IItemMatterData content;
							if (contentSource.CanStack)
							{
								int contentQuantity = (int)contentSource.Quantity;

								content = new StackItemMatterData(contentSource.ContainerItemMatterId, contentSource.ItemId, contentSource.ItemName, contentSource.CanEquip, contentQuantity);
							}
							else
							{
								content = new ItemMatterData(contentSource.ContainerItemMatterId, contentSource.ItemId, contentSource.ItemName, contentSource.CanEquip);
							}

							contents.Add(content);
						}

						result = new ContainerItemMatterData(source.ItemMatterId, source.ItemId, source.ItemName, source.CanEquip, contents);
					}
					else if (source.CanStack)
					{
						int quantity = (int)source.Quantity;

						result = new StackItemMatterData(source.ItemMatterId, source.ItemId, source.ItemName, source.CanEquip, quantity);
					}
					else
					{
						result = new ItemMatterData(source.ItemMatterId, source.ItemId, source.ItemName, source.CanEquip);
					}
				}

				results.Add(result);
			}

			return results;
		}
	}

	#endregion

	#region Nested types

	/// <summary>
	/// アイテム物質のレコード
	/// </summary>
	/// <param name="ItemMatterId">アイテム物質ID</param>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	/// <param name="IsContainer">コンテナーかどうか</param>
	/// <param name="CanStack">スタック可能かどうか</param>
	/// <param name="CanEquip">装備できるか</param>
	/// <param name="Quantity">数量</param>
	private record class ItemMatterRecord(string ItemMatterId, string ItemId, string ItemName, bool IsContainer, bool CanStack, bool CanEquip, long Quantity);

	/// <summary>
	/// 内容アイテム物質のレコード
	/// </summary>
	/// <param name="ContainerItemMatterId">コンテナーアイテム物質ID</param>
	/// <param name="ItemMatterId">アイテム物質ID</param>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	/// <param name="CanStack">スタック可能かどうか</param>
	/// <param name="CanEquip">装備可能かどうか</param>
	/// <param name="Quantity">数量</param>
	private record class ContentItemMatterRecord(string ContainerItemMatterId, string ItemMatterId, string ItemId, string ItemName, bool CanStack, bool CanEquip, long Quantity);

	#endregion
}

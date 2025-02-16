using Dapper;
using Life.Application.Data;
using Life.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Life.Application.Sqlite;

/// <summary>
/// 人間の問い合わせサービス
/// </summary>
/// <param name="configuration">設定</param>
public class HumanQueryService(IConfiguration configuration) : IHumanQueryService
{
	#region Fields

	/// <summary>
	/// 言語コード
	/// </summary>
	private readonly string _languageCode = configuration.GetValue<string>("LanguageCode") ?? throw new InvalidOperationException("言語コードが取得できません。");

	#endregion

	#region Methods

	/// <summary>
	/// 人間を問い合わせします。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <returns>問い合わせした人間データを返します。</returns>
	public HumanData QuerySingle(string humanId)
	{
		using IDbConnection connection = ConnectionFactory.Create();
		connection.Open();

		HumanRecord record;
		FamilySummaryData family;
		{
			const string sql = @"SELECT
	  hum.human_id
	, hum.first_name
	, hum.family_id
	, fam.family_name
FROM
	humans hum
	INNER JOIN families fam
		ON hum.family_id = fam.family_id
WHERE
	hum.human_id = :human_id";

			var param = new
			{
				human_id = humanId,
			};

			record = connection.QuerySingle<HumanRecord>(sql, param);

			family = new FamilySummaryData(record.FamilyId, record.FamilyName);
		}

		AreaSummaryData area;
		{
			const string sql = @"SELECT
	  hum.area_id
	, anm.area_name
FROM
	humans hum
	INNER JOIN areas are
		ON hum.area_id = are.area_id
	INNER JOIN area_names anm
		ON  are.area_id = anm.area_id
		AND anm.language_code = :language_code
WHERE
	hum.human_id = :human_id";

			var param = new
			{
				human_id = humanId,
				language_code = _languageCode,
			};

			area = connection.QuerySingle<AreaSummaryData>(sql, param);
		}

		List<ItemSummaryData> gatheringItems;
		{
			const string sql = @"SELECT
	  hgi.item_id
	, inm.item_name
FROM
	human_gathering_items hgi
	INNER JOIN items ite
		ON hgi.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND language_code = :language_code
WHERE
	hgi.human_id = :human_id
ORDER BY
	  hgi.human_id
	, hgi.item_id";

			var param = new
			{
				human_id = humanId,
				language_code = _languageCode,
			};

			gatheringItems = connection.Query<ItemSummaryData>(sql, param).ToList();
		}

		List<BuildingRecipeSummaryData> buildingRecipes;
		{
			const string sql = @"SELECT
	  brc.building_recipe_id
	, bnm.building_name
FROM
	human_building_recipes hbr
	INNER JOIN building_recipes brc
		ON hbr.building_recipe_id = brc.building_recipe_id
	INNER JOIN buildings bui
		ON brc.building_id = bui.building_id
	INNER JOIN building_names bnm
		ON  bui.building_id = bnm.building_id
		AND bnm.language_code = :language_code
WHERE
	hbr.human_id = :human_id
ORDER BY
	brc.building_recipe_id";

			var param = new
			{
				human_id = humanId,
				language_code = _languageCode,
			};

			buildingRecipes = connection.Query<BuildingRecipeSummaryData>(sql, param).ToList();
		}

		List<ItemRecipeSummaryData> itemRecipes;
		{
			const string sql = @"SELECT
	  hir.item_recipe_id
	, inm.item_name
FROM
	human_item_recipes hir
	INNER JOIN item_recipes irc
		ON hir.item_recipe_id = irc.item_recipe_id
	INNER JOIN items ite
		ON irc.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id = inm.item_id
		AND inm.language_code = :language_code
WHERE
	human_id = :human_id
ORDER BY
	hir.item_recipe_id";

			var param = new
			{
				human_id = humanId,
				language_code = _languageCode,
			};

			itemRecipes = connection.Query<ItemRecipeSummaryData>(sql, param).ToList();
		}

		List<ItemSummaryData> equipmentItems = [];
		{
			const string sql = @"SELECT
	  hei.item_id
	, inm.item_name
FROM
	human_equipment_items hei
	INNER JOIN items ite
		ON hei.item_id = ite.item_id
	INNER JOIN item_names inm
		ON  ite.item_id= inm.item_id
		AND inm.language_code = :language_code
WHERE
	hei.human_id = :human_id
ORDER BY
	  hei.human_id
	, hei.item_id";

			var param = new
			{
				human_id = humanId,
				language_code = _languageCode,
			};

			equipmentItems = connection.Query<ItemSummaryData>(sql, param).ToList();
		}

		List<InventorySlotData> inventorySlots = [];
		{
			const string sql = @"SELECT
	  imt.item_id
	, inm.item_name
	, ite.can_equip
	, imt.quantity
FROM
	human_item_matters him
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
	, imt.item_id";

			var param = new
			{
				human_id = humanId,
				language_code = _languageCode,
			};

			IEnumerable<InventorySlotRecord> sources = connection.Query<InventorySlotRecord>(sql, param);

			foreach (InventorySlotRecord source in sources)
			{
				InventorySlotData inventorySlot;
				{
					ItemData item;
					{
						bool canEquip = source.CanEquip == "1";

						item = new ItemData(source.ItemId, source.ItemName, canEquip);
					}

					int quantity = (int)source.Quantity;

					inventorySlot = new InventorySlotData(item, quantity);
				}

				inventorySlots.Add(inventorySlot);
			}
		}

		HumanData result = new(record.HumanId, record.FirstName, family, area, gatheringItems, buildingRecipes, itemRecipes, equipmentItems, inventorySlots);

		return result;
	}

	/// <summary>
	/// 人間を問い合わせします。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <returns>問い合わせした人間データを返します。</returns>
	public async Task<HumanData> QuerySingleAsync(string humanId)
	{
		return await Task.Run(() => QuerySingle(humanId));
	}

	#endregion

	#region Nested types

	/// <summary>
	/// 人間のレコード
	/// </summary>
	/// <param name="HumanId">人間ID</param>
	/// <param name="FirstName">名</param>
	/// <param name="FamilyId">家族ID</param>
	/// <param name="FamilyName">家族名</param>
	private record class HumanRecord(string HumanId, string FirstName, string FamilyId, string FamilyName);

	/// <summary>
	/// インベントリースロットのレコード
	/// </summary>
	/// <param name="ItemId">アイテムID</param>
	/// <param name="ItemName">アイテム名</param>
	/// <param name="CanEquip">装備できるか</param>
	/// <param name="Quantity">数量</param>
	private record class InventorySlotRecord(string ItemId, string ItemName, string CanEquip, long Quantity);

	#endregion
}

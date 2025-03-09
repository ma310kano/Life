namespace Life.Domain.Model;

/// <summary>
/// アイテム物質のリポジトリー
/// </summary>
public interface IItemMatterRepository
{
	#region Methods

	/// <summary>
	/// アイテムの中にアイテムを追加します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void AddInItem(ItemMatterId storageItemMatterId, ItemMatterId itemMatterId);

	/// <summary>
	/// アイテム物質を削除します。
	/// </summary>
	/// <param name="itemMatter">アイテム物質</param>
	void Delete(ItemMatter itemMatter);

	/// <summary>
	/// アイテムの中を検索します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索したアイテム物質を返します。</returns>
	ItemMatter FindInItem(ItemMatterId storageItemMatterId, ItemId itemId);

	/// <summary>
	/// アイテム物質を検索します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	/// <returns>検索したアイテム物質を返します。</returns>
	ItemMatter? FindOrDefault(ItemMatterId itemMatterId);

	/// <summary>
	/// アイテムの中からアイテムを除去します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void RemoveInItem(ItemMatterId storageItemMatterId, ItemMatterId itemMatterId);

	/// <summary>
	/// アイテム物質を保存します。
	/// </summary>
	/// <param name="itemMatter">アイテム物質</param>
	void Save(ItemMatter itemMatter);

	#endregion
}

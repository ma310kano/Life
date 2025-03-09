namespace Life.Domain.Model;

/// <summary>
/// 人間のインベントリースロットのリポジトリー
/// </summary>
public interface IHumanInventorySlotRepository
{
	#region Methods

	/// <summary>
	/// インベントリースロットを追加します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void Add(ItemMatterId itemMatterId);

	/// <summary>
	/// 人間のインベントリースロットを除去します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void Remove(ItemMatterId itemMatterId);

	/// <summary>
	/// アイテムの中にアイテムを追加します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void AddInItem(ItemMatterId storageItemMatterId, ItemMatterId itemMatterId);

	/// <summary>
	/// 検索します。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索したアイテム物質のコレクションを返します。</returns>
	IEnumerable<ItemMatter> Find(ItemId itemId);

	/// <summary>
	/// アイテムの中を検索します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索したアイテム物質を返します。</returns>
	ItemMatter FindInItem(ItemMatterId storageItemMatterId, ItemId itemId);

	/// <summary>
	/// アイテムの中からアイテムを除去します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void RemoveInItem(ItemMatterId storageItemMatterId, ItemMatterId itemMatterId);

	#endregion
}

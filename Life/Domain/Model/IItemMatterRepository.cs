namespace Life.Domain.Model;

/// <summary>
/// アイテム物質のリポジトリー
/// </summary>
public interface IItemMatterRepository
{
	#region Methods

	/// <summary>
	/// コンテナーにアイテムを追加します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void AddInContainer(ItemMatterId storageItemMatterId, ItemMatterId itemMatterId);

	/// <summary>
	/// アイテム物質を削除します。
	/// </summary>
	/// <param name="itemMatter">アイテム物質</param>
	void Delete(ItemMatter itemMatter);

	/// <summary>
	/// コンテナーから単一のアイテムを検索します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	/// <returns>検索したアイテム物質を返します。</returns>
	ItemMatter FindSingleInContainer(ItemMatterId storageItemMatterId);

	/// <summary>
	/// アイテム物質を検索します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	/// <returns>検索したアイテム物質を返します。</returns>
	ItemMatter? FindOrDefault(ItemMatterId itemMatterId);

	/// <summary>
	/// コンテナーからすべてのアイテムを除去します。
	/// </summary>
	/// <param name="storageItemMatterId">収納アイテム物質ID</param>
	void RemoveAllInContainer(ItemMatterId storageItemMatterId);

	/// <summary>
	/// アイテム物質を保存します。
	/// </summary>
	/// <param name="itemMatter">アイテム物質</param>
	void Save(ItemMatter itemMatter);

	#endregion
}

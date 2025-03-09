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
	/// 検索します。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索したアイテム物質のコレクションを返します。</returns>
	IEnumerable<ItemMatter> Find(ItemId itemId);

	#endregion
}

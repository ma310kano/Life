namespace Life.Domain.Model;

/// <summary>
/// 人間のインベントリースロットのリポジトリー
/// </summary>
public interface IHumanInventorySlotRepository
{
	#region Methods

	/// <summary>
	/// 人間のインベントリースロットを削除します。
	/// </summary>
	/// <param name="humanInventorySlot">人間のインベントリースロット</param>
	void Delete(HumanInventorySlot humanInventorySlot);

	/// <summary>
	/// 人間のインベントリースロットを削除します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	void Delete(HumanId humanId, ItemId itemId);

	/// <summary>
	/// 人間のインベントリースロットを検索します。
	/// </summary>
	/// <param name="humainId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索した人間のインベントリースロットを返します。</returns>
	HumanInventorySlot? FindOrDefault(HumanId humainId, ItemId itemId);

	/// <summary>
	/// 人間のインベントリースロットを保存します。
	/// </summary>
	/// <param name="humanInventorySlot">人間のインベントリースロット</param>
	void Save(HumanInventorySlot humanInventorySlot);

	#endregion
}

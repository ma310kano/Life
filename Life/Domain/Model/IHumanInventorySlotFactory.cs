namespace Life.Domain.Model;

/// <summary>
/// 人間のインベントリースロットのファクトリー
/// </summary>
public interface IHumanInventorySlotFactory
{
	#region Methods

	/// <summary>
	/// 人間のインベントリースロットを作成します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>作成した人間のインベントリースロットを返します。</returns>
	HumanInventorySlot Create(HumanId humanId, ItemId itemId);

	#endregion
}

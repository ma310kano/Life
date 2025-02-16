namespace Life.Domain.Model;

/// <summary>
/// 人間のインベントリースロットのファクトリー
/// </summary>
public class HumanInventorySlotFactory : IHumanInventorySlotFactory
{
	#region Methods

	/// <summary>
	/// 人間のインベントリースロットを作成します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>作成した人間のインベントリースロットを返します。</returns>
	public HumanInventorySlot Create(HumanId humanId, ItemId itemId)
	{
		HumanInventorySlot product;
		{
			ItemMatterId itemMatterId = ItemMatterId.Create();
			Quantity quantity = new(0);

			product = new(humanId, itemMatterId, itemId, quantity);
		}

		return product;
	}

	#endregion
}

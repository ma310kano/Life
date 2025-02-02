namespace Life.Domain.Model;

/// <summary>
/// 装備アイテムのファクトリー
/// </summary>
public class EquipmentItemFactory : IEquipmentItemFactory
{
	#region Methods

	/// <summary>
	/// 装備アイテムを作成します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>作成した装備アイテムを返します。</returns>
	public EquipmentItem Create(HumanId humanId, ItemId itemId)
	{
		EquipmentItem product = new(humanId, itemId);

		return product;
	}

	#endregion
}

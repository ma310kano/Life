namespace Life.Domain.Model;

/// <summary>
/// アイテム物質のリポジトリー
/// </summary>
public class ItemMatterFactory : IItemMatterFactory
{
	#region Methods

	/// <summary>
	/// アイテム物質を作成します。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <param name="quantity">数量</param>
	/// <returns>作成したアイテム物質のコレクションを返します。</returns>
	public ItemMatter Create(ItemId itemId, Quantity quantity)
	{
		ItemMatter product;
		{
			ItemMatterId itemMatterId = ItemMatterId.Create();

			product = new ItemMatter(itemMatterId, itemId, quantity);
		}

		return product;
	}

	#endregion
}

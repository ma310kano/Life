﻿using Life.Domain.Model;

namespace Life.Domain.Service;

/// <summary>
/// インベントリーの追加サービス
/// </summary>
/// <param name="context">コンテキスト</param>
/// <param name="humanId">人間ID</param>
public class HumanInventoryAdditionService(IHumanContext context, HumanId humanId)
{
	#region Methods

	/// <summary>
	/// インベントリーへ追加します。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <param name="quantity">数量</param>
	public void Add(ItemId itemId, Quantity quantity)
	{
		bool canStack = context.ItemRepository.FindCanStack(itemId);

		if (canStack)
		{
			ItemMatter? itemMatter;
			{
				IEnumerable<ItemMatter> itemMatters = context.ItemMatterRepository.FindInHumanInventory(humanId, itemId);

				itemMatter = itemMatters.SingleOrDefault();
			}

			if (itemMatter is not null)
			{
				itemMatter.AddQuantity(quantity);
			}
			else
			{
				itemMatter = context.ItemMatterFactory.Create(itemId, quantity);

				context.InventorySlotRepository.Add(humanId, itemMatter.ItemMatterId);
			}

			context.ItemMatterRepository.Save(itemMatter);
		}
		else
		{
			List<ItemMatter> products = [];

			int frequency = quantity.Value;
			Quantity one = new(1);
			for (int i = 0; i < frequency; i++)
			{
				ItemMatter product = context.ItemMatterFactory.Create(itemId, one);

				products.Add(product);
			}

			foreach (ItemMatter product in products)
			{
				context.ItemMatterRepository.Save(product);
			}

			foreach (ItemMatter product in products)
			{
				context.InventorySlotRepository.Add(humanId, product.ItemMatterId);
			}
		}
	}

	#endregion
}

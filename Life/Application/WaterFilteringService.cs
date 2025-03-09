using Life.Application.Command;
using Life.Domain.Model;

namespace Life.Application;

/// <summary>
/// 人間の建造物操作サービス
/// </summary>
/// <param name="contextFactory">コンテキストのファクトリー</param>
public class WaterFilteringService(IHumanContextFactory contextFactory) : IWaterFilteringService
{
	#region Methods

	/// <summary>
	/// 水を濾過します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public void Filter(HumanBuildingOperationCommand command)
	{
		HumanId humanId = new(command.HumanId);
		BuildingId buildingId = new(command.BuildingId);
		ItemMatterId storageItemMatterId = new(command.StorageItemMatterId);

		using IHumanContext context = contextFactory.Create(humanId);

		try
		{
			if (buildingId.Value == "filter")
			{
				// 材料
				{
					ItemMatter ingredient;
					{
						ItemId itemId = new("raw-water");
						ingredient = context.ItemMatterRepository.FindInItem(storageItemMatterId, itemId);
					}

					context.ItemMatterRepository.RemoveInItem(storageItemMatterId, ingredient.ItemMatterId);
					context.ItemMatterRepository.Delete(ingredient);
				}

				// 作成物
				{
					ItemMatter product;
					{
						ItemId itemId = new("filtrate-water");
						Quantity quantity = new(1);

						product = context.ItemMatterFactory.Create(itemId, quantity);
					}

					context.ItemMatterRepository.Save(product);
					context.ItemMatterRepository.AddInItem(storageItemMatterId, product.ItemMatterId);
				}
			}

			context.Commit();
		}
		catch
		{
			context.Rollback();
			throw;
		}
	}

	/// <summary>
	/// 水を濾過します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public async Task FilterAsync(HumanBuildingOperationCommand command)
	{
		await Task.Run(() => Filter(command));
	}

	#endregion
}

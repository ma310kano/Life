using Life.Application.Command;
using Life.Domain.Model;

namespace Life.Application;

/// <summary>
/// 水の濾過サービス
/// </summary>
/// <param name="contextFactory">コンテキストのファクトリー</param>
public class WaterFilteringService(IHumanContextFactory contextFactory) : IWaterFilteringService
{
	#region Methods

	/// <summary>
	/// 水を濾過します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public void Filter(HumanWaterFilteringCommand command)
	{
		HumanId humanId = new(command.HumanId);
		ItemMatterId sourceContainerItemMatterId = new(command.SourceContainerItemMatterId);
		ItemMatterId destinationContainerItemMatterId = new(command.DestinationContainerItemMatterId);

		using IHumanContext context = contextFactory.Create(humanId);

		try
		{
			// 材料
			{
				ItemMatter ingredient = context.ItemMatterRepository.FindSingleInContainer(sourceContainerItemMatterId);
				if (ingredient.ItemId.Value != "raw-water") throw new InvalidOperationException("濾過の材料が生水ではありません。");

				context.ItemMatterRepository.RemoveAllInContainer(sourceContainerItemMatterId);
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
				context.ItemMatterRepository.AddInContainer(destinationContainerItemMatterId, product.ItemMatterId);
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
	public async Task FilterAsync(HumanWaterFilteringCommand command)
	{
		await Task.Run(() => Filter(command));
	}

	#endregion
}

using Life.Application.Command;
using Life.Domain.Model;

namespace Life.Application;

/// <summary>
/// 人間の採集サービス
/// </summary>
/// <param name="contextFactory">コンテキストのファクトリー</param>
public class HumanGatheringService(IHumanContextFactory contextFactory) : IHumanGatheringService
{
	#region Methods

	/// <summary>
	/// 採集します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public void Gather(HumanGatheringCommand command)
	{
		HumanId humanId = new(command.HumanId);

		using IHumanContext context = contextFactory.Create();

		try
		{
			foreach (HumanGatheringItemCommand source in command.Items)
			{
				ItemId itemId = new(source.ItemId);

				HumanInventorySlot? inventorySlot = context.InventorySlotRepository.FindOrDefault(humanId, itemId);
				inventorySlot ??= context.InventorySlotFactory.Create(humanId, itemId);

				inventorySlot.AddQuantity(source.Quantity);

				context.InventorySlotRepository.Save(inventorySlot);
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
	/// 採集します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public async Task GatherAsync(HumanGatheringCommand command)
	{
		await Task.Run(() => Gather(command));
	}

	#endregion
}

using Life.Application.Command;
using Life.Domain.Model;
using Life.Domain.Service;

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
			HumanInventoryAdditionService inventoryAdditionService = new(context, humanId);
			foreach (HumanGatheringItemCommand source in command.Items)
			{
				ItemId itemId = new(source.ItemId);
				Quantity quantity = new(source.Quantity);

				inventoryAdditionService.Add(itemId, quantity);
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

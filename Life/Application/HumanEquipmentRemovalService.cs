using Life.Application.Command;
using Life.Domain.Model;

namespace Life.Application;

/// <summary>
/// 人間の装備解除サービス
/// </summary>
/// <param name="contextFactory">コンテキストファクトリー</param>
public class HumanEquipmentRemovalService(IHumanContextFactory contextFactory) : IHumanEquipmentRemovalService
{
	#region Methods

	/// <summary>
	/// 装備を解除します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public void Remove(HumanEquipmentRemovalCommand command)
	{
		HumanId humanId = new(command.HumanId);
		ItemMatterId itemMatterId = new(command.ItemMatterId);

		using IHumanContext context = contextFactory.Create();

		try
		{
			ItemMatter itemMatter = context.ItemMatterRepository.FindOrDefault(itemMatterId) ?? throw new InvalidOperationException("アイテムが見つかりません。");

			// Equipment item
			{
				context.EquipmentItemRepository.Remove(humanId, itemMatter.ItemMatterId);
			}

			// Gthering item
			{
				context.GatheringItemRepository.Remove(humanId, itemMatter.ItemId);
			}

			// Inventory slot
			{
				context.InventorySlotRepository.Add(humanId, itemMatter.ItemMatterId);
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
	/// 装備を解除します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public async Task RemoveAsync(HumanEquipmentRemovalCommand command)
	{
		await Task.Run(() => Remove(command));
	}

	#endregion
}

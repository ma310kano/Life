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
		ItemId itemId = new(command.ItemId);

		using IHumanContext context = contextFactory.Create();

		try
		{
			{
				EquipmentItem equipmentItem = context.EquipmentItemRepository.Find(humanId, itemId);

				context.EquipmentItemRepository.Delete(equipmentItem);
			}

			{
				HumanInventorySlot inventorySlot = context.InventorySlotRepository.FindOrDefault(humanId, itemId) ?? context.InventorySlotFactory.Create(humanId, itemId);

				inventorySlot.AddQuantity(1);

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
	/// 装備を解除します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public async Task RemoveAsync(HumanEquipmentRemovalCommand command)
	{
		await Task.Run(() => Remove(command));
	}

	#endregion
}

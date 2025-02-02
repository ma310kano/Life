using Life.Application.Command;
using Life.Domain.Model;

namespace Life.Application;

/// <summary>
/// 人間の装備サービス
/// </summary>
/// <param name="contextFactory">コンテキストファクトリー</param>
public class HumanEquipmentService(IHumanContextFactory contextFactory) : IHumanEquipmentService
{
	#region Methods

	/// <summary>
	/// 装備します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public void Equip(HumanEquipmentCommand command)
	{
		HumanId humanId = new(command.HumanId);
		ItemId itemId = new(command.ItemId);

		using IHumanContext context = contextFactory.Create();

		try
		{
			HumanInventorySlot inventorySlot = context.InventorySlotRepository.FindOrDefault(humanId, itemId) ?? throw new InvalidOperationException("アイテムが見つかりません。");

			inventorySlot.SubtractQuantity(1);

			if (inventorySlot.Quantity.Value > 0)
			{
				context.InventorySlotRepository.Save(inventorySlot);
			}
			else
			{
				context.InventorySlotRepository.Delete(inventorySlot);
			}

			EquipmentItem equipmentItem = context.EquipmentItemFactory.Create(humanId, itemId);

			context.EquipmentItemRepository.Save(equipmentItem);

			context.Commit();
		}
		catch
		{
			context.Rollback();
			throw;
		}
	}

	/// <summary>
	/// 装備します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public async Task EquipAsync(HumanEquipmentCommand command)
	{
		await Task.Run(() => Equip(command));
	}

	#endregion
}

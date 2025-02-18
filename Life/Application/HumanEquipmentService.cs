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
		ItemMatterId itemMatterId = new(command.ItemMatterId);

		using IHumanContext context = contextFactory.Create();

		try
		{
			ItemMatter itemMatter = context.ItemMatterRepository.FindOrDefault(itemMatterId) ?? throw new InvalidOperationException("アイテムが見つかりません。");

			// Inventory slot
			{
				context.InventorySlotRepository.Remove(humanId, itemMatter.ItemMatterId);
			}

			// Equipment item
			{
				context.EquipmentItemRepository.Add(humanId, itemMatter.ItemMatterId);
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
	/// 装備します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public async Task EquipAsync(HumanEquipmentCommand command)
	{
		await Task.Run(() => Equip(command));
	}

	#endregion
}

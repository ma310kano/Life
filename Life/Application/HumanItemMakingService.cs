using Life.Application.Command;
using Life.Domain.Model;
using Life.Domain.Service;

namespace Life.Application;

/// <summary>
/// 人間のアイテム作成サービス
/// </summary>
/// <param name="contextFactory">コンテキストファクトリー</param>
public class HumanItemMakingService(IHumanContextFactory contextFactory) : IHumanItemMakingService
{
	#region Methods

	/// <summary>
	/// アイテムを作成します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public void Make(HumanItemMakingCommand command)
	{
		HumanId humanId = new(command.HumanId);
		ItemRecipeId itemRecipeId = new(command.ItemRecipeId);
		Frequency frequency = new(command.Frequency);

		using IHumanContext context = contextFactory.Create(humanId);

		try
		{
			ItemRecipe itemRecipe = context.ItemRecipeRepository.Find(itemRecipeId);

			if (itemRecipe.BuildingId is not null)
			{
				bool existsBuilding = context.AreaFinder.Exists(itemRecipe.BuildingId);
				if (!existsBuilding) throw new InvalidOperationException($"エリアに建造物 {itemRecipe.BuildingId} がありません。");
			}

			// Ingredients
			HumanInventorySubstractionService inventorySubstractionService = new(context);
			inventorySubstractionService.Subtract(itemRecipe.Ingredients, frequency);

			// Product
			{
				Quantity quantity = itemRecipe.Quantity * frequency;

				HumanInventoryAdditionService inventoryAdditionService = new(context);
				inventoryAdditionService.Add(itemRecipe.ItemId, quantity);
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
	/// アイテムを作成します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public async Task MakeAsync(HumanItemMakingCommand command)
	{
		await Task.Run(() => Make(command));
	}

	#endregion
}

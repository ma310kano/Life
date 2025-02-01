using Life.Application.Command;
using Life.Domain.Model;

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

		using IHumanContext context = contextFactory.Create();

		try
		{
			ItemRecipe itemRecipe = context.ItemRecipeRepository.Find(itemRecipeId);

			foreach (ItemRecipeIngredient ingredient in itemRecipe.Ingredients)
			{
				HumanInventorySlot ingredientSlot = context.InventorySlotRepository.FindOrDefault(humanId, ingredient.ItemId) ?? throw new InvalidOperationException("アイテムが見つかりません。");

				ingredientSlot.SubtractQuantity(ingredient.Quantity.Value);

				if (ingredientSlot.Quantity.Value > 0)
				{
					context.InventorySlotRepository.Save(ingredientSlot);
				}
				else
				{
					context.InventorySlotRepository.Delete(ingredientSlot);
				}
			}

			HumanInventorySlot productSlot = context.InventorySlotRepository.FindOrDefault(humanId, itemRecipe.ItemId) ?? context.InventorySlotFactory.Create(humanId, itemRecipe.ItemId);

			productSlot.AddQuantity(itemRecipe.Quantity.Value);

			context.InventorySlotRepository.Save(productSlot);

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

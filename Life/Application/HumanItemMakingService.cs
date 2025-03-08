﻿using Life.Application.Command;
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

		using IHumanContext context = contextFactory.Create();

		try
		{
			ItemRecipe itemRecipe = context.ItemRecipeRepository.Find(itemRecipeId);

			if (itemRecipe.BuildingId is not null)
			{
				Human human = context.Repository.Find(humanId);
				bool existsBuilding = context.AreaBuildingRepository.Exists(human.AreaId, itemRecipe.BuildingId);
				if (!existsBuilding) throw new InvalidOperationException($"エリア {human.AreaId} に建造物 {itemRecipe.BuildingId} がありません。");
			}

			// Ingredients
			foreach (ItemRecipeIngredient ingredient in itemRecipe.Ingredients)
			{
				ItemMatter itemMatter = context.ItemMatterRepository.FindInHumanInventory(humanId, ingredient.ItemId).Single();

				Quantity quantity = new(ingredient.Quantity.Value * command.Frequencty);

				itemMatter.SubtractQuantity(quantity);

				if (itemMatter.Remains)
				{
					context.ItemMatterRepository.Save(itemMatter);
				}
				else
				{
					context.InventorySlotRepository.Remove(humanId, itemMatter.ItemMatterId);
					context.ItemMatterRepository.Delete(itemMatter);
				}
			}

			// Product
			{
				Quantity quantity = new(itemRecipe.Quantity.Value * command.Frequencty);

				HumanInventoryAdditionService inventoryAdditionService = new(context, humanId);
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

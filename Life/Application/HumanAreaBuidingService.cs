using Life.Application.Command;
using Life.Domain.Model;

namespace Life.Application;

/// <summary>
/// 人間のエリア建造サービス
/// </summary>
/// <param name="contextFactory">コンテキストファクトリー</param>
public class HumanAreaBuidingService(IHumanContextFactory contextFactory) : IHumanAreaBuidingService
{
	#region Methods

	/// <summary>
	/// 建造します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public void Build(HumanAreaBuildingCommand command)
	{
		HumanId humanId = new(command.HumanId);
		BuildingRecipeId buildingRecipeId = new(command.BuildingRecipeId);

		using IHumanContext context = contextFactory.Create();

		try
		{
			Human human = context.Repository.Find(humanId);

			BuildingRecipe buildingRecipe = context.BuildingRecipeRepository.Find(buildingRecipeId);

			foreach (BuildingRecipeIngredient ingredient in buildingRecipe.Ingredients)
			{
				HumanInventorySlot inventorySlot = context.InventorySlotRepository.FindOrDefault(humanId, ingredient.ItemId) ?? throw new InvalidOperationException("アイテムが見つかりません。");

				inventorySlot.SubtractQuantity(ingredient.Quantity.Value);

				if (inventorySlot.Quantity.Value > 0)
				{
					context.InventorySlotRepository.Save(inventorySlot);
				}
				else
				{
					context.InventorySlotRepository.Delete(inventorySlot);
				}
			}

			context.AreaBuildingRepository.Add(human.AreaId, buildingRecipe.BuildingId);

			context.Commit();
		}
		catch
		{
			context.Rollback();
			throw;
		}
	}

	/// <summary>
	/// 建造します。
	/// </summary>
	/// <param name="commnad">コマンド</param>
	public async Task BuildAsync(HumanAreaBuildingCommand command)
	{
		await Task.Run(() => Build(command));
	}

	#endregion
}

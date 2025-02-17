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
				ItemMatter itemMatter = context.ItemMatterRepository.FindInHumanInventory(humanId, ingredient.ItemId).Single();

				itemMatter.SubtractQuantity(ingredient.Quantity);

				if (itemMatter.Quantity.Value > 0)
				{
					context.ItemMatterRepository.Save(itemMatter);
				}
				else
				{
					context.InventorySlotRepository.Remove(humanId, itemMatter.ItemMatterId);
					context.ItemMatterRepository.Delete(itemMatter);
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

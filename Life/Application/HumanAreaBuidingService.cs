using Life.Application.Command;
using Life.Domain.Model;
using Life.Domain.Service;

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

			HumanInventorySubstractionService inventorySubstractionService = new(context, humanId);
			inventorySubstractionService.Subtract(buildingRecipe.Ingredients);

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

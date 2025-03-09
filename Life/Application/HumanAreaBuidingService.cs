using Life.Application.Command;
using Life.Domain.Model;
using Life.Domain.Service;

namespace Life.Application;

/// <summary>
/// 人間のエリア建造サービス
/// </summary>
/// <param name="humanContextFactory">人間のコンテキストファクトリー</param>
/// <param name="areaContextFactory">エリアのコンテキストファクトリー</param>
public class HumanAreaBuidingService(IHumanContextFactory humanContextFactory, IAreaContextFactory areaContextFactory) : IHumanAreaBuidingService
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

		AreaId areaId;
		BuildingId buildingId;
		using (IHumanContext context = humanContextFactory.Create(humanId))
		{
			try
			{
				BuildingRecipe buildingRecipe = context.BuildingRecipeRepository.Find(buildingRecipeId);

				HumanInventorySubstractionService inventorySubstractionService = new(context);
				inventorySubstractionService.Subtract(buildingRecipe.Ingredients);

				{
					Human human = context.Repository.Find();
					areaId = human.AreaId;
				}

				buildingId = buildingRecipe.BuildingId;

				context.Commit();
			}
			catch
			{
				context.Rollback();
				throw;
			}
		}

		using (IAreaContext context = areaContextFactory.Create(areaId))
		{
			try
			{
				context.BuildingRepository.Add(buildingId);

				context.Commit();
			}
			catch
			{
				context.Rollback();
				throw;
			}
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

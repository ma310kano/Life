using Life.Application.Command;
using Life.Domain.Model;

namespace Life.Application;

/// <summary>
/// 人間のエリア移動サービス
/// </summary>
/// <param name="contextFactory">コンテキストのファクトリー</param>
public class HumanAreaMovementService(IHumanContextFactory contextFactory) : IHumanAreaMovementService
{
	#region Methods

	/// <summary>
	/// エリアを移動します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public void MoveArea(HumanAreaMovementCommand command)
	{
		HumanId humanId = new(command.HumanId);
		AreaId areaId = new(command.AreaId);

		using IHumanContext context = contextFactory.Create(humanId);

		try
		{
			Human human = context.Repository.Find();

			human.ChangeArea(areaId);

			context.Repository.Save(human);

			context.Commit();
		}
		catch
		{
			context.Rollback();
			throw;
		}
	}

	/// <summary>
	/// エリアを移動します。
	/// </summary>
	/// <param name="command">コマンド</param>
	public async Task MoveAreaAsync(HumanAreaMovementCommand command)
	{
		await Task.Run(() => MoveArea(command));
	}

	#endregion
}

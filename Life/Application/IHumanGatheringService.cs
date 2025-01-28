using Life.Application.Command;

namespace Life.Application;

/// <summary>
/// 人間の採集サービス
/// </summary>
public interface IHumanGatheringService
{
	#region Methods

	/// <summary>
	/// 採集します。
	/// </summary>
	/// <param name="command">コマンド</param>
	void Gather(HumanGatheringCommand command);

	/// <summary>
	/// 採集します。
	/// </summary>
	/// <param name="command">コマンド</param>
	Task GatherAsync(HumanGatheringCommand command);

	#endregion
}

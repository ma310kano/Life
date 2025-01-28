using Life.Application.Command;

namespace Life.Application;

/// <summary>
/// 人間のエリア移動サービス
/// </summary>
public interface IHumanAreaMovementService
{
	#region Methods

	/// <summary>
	/// エリアを移動します。
	/// </summary>
	/// <param name="command">コマンド</param>
	void MoveArea(HumanAreaMovementCommand command);

	/// <summary>
	/// エリアを移動します。
	/// </summary>
	/// <param name="command">コマンド</param>
	Task MoveAreaAsync(HumanAreaMovementCommand command);

	#endregion
}

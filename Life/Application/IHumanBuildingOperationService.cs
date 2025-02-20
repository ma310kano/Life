using Life.Application.Command;

namespace Life.Application;

/// <summary>
/// 人間の建造物操作サービス
/// </summary>
public interface IHumanBuildingOperationService
{
	#region Methods

	/// <summary>
	/// 建造物を操作します。
	/// </summary>
	/// <param name="command">コマンド</param>
	void Operate(HumanBuildingOperationCommand command);

	/// <summary>
	/// 建造物を操作します。
	/// </summary>
	/// <param name="command">コマンド</param>
	Task OperateAsync(HumanBuildingOperationCommand command);

	#endregion
}

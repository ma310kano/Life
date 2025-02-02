using Life.Application.Command;

namespace Life.Application;

/// <summary>
/// 人間の装備解除サービス
/// </summary>
public interface IHumanEquipmentRemovalService
{
	#region Methods

	/// <summary>
	/// 装備を解除します。
	/// </summary>
	/// <param name="command">コマンド</param>
	void Remove(HumanEquipmentRemovalCommand command);

	/// <summary>
	/// 装備を解除します。
	/// </summary>
	/// <param name="command">コマンド</param>
	Task RemoveAsync(HumanEquipmentRemovalCommand command);

	#endregion
}

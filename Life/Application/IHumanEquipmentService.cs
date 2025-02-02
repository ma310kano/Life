using Life.Application.Command;

namespace Life.Application;

/// <summary>
/// 人間の装備サービス
/// </summary>
public interface IHumanEquipmentService
{
	#region Methods

	/// <summary>
	/// 装備します。
	/// </summary>
	/// <param name="command">コマンド</param>
	void Equip(HumanEquipmentCommand command);

	/// <summary>
	/// 装備します。
	/// </summary>
	/// <param name="command">コマンド</param>
	Task EquipAsync(HumanEquipmentCommand command);

	#endregion
}

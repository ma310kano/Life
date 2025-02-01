using Life.Application.Command;

namespace Life.Application;

/// <summary>
/// 人間のアイテム作成サービス
/// </summary>
public interface IHumanItemMakingService
{
	#region Methods

	/// <summary>
	/// アイテムを作成します。
	/// </summary>
	/// <param name="command">コマンド</param>
	void Make(HumanItemMakingCommand command);

	/// <summary>
	/// アイテムを作成します。
	/// </summary>
	/// <param name="command">コマンド</param>
	Task MakeAsync(HumanItemMakingCommand command);

	#endregion
}

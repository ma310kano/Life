using Life.Application.Command;

namespace Life.Application;

/// <summary>
/// 水の濾過サービス
/// </summary>
public interface IWaterFilteringService
{
	#region Methods

	/// <summary>
	/// 水を濾過します。
	/// </summary>
	/// <param name="command">コマンド</param>
	void Filter(HumanBuildingOperationCommand command);

	/// <summary>
	/// 水を濾過します。
	/// </summary>
	/// <param name="command">コマンド</param>
	Task FilterAsync(HumanBuildingOperationCommand command);

	#endregion
}

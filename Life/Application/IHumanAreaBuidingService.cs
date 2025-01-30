using Life.Application.Command;

namespace Life.Application
{
	/// <summary>
	/// 人間のエリア建造サービス
	/// </summary>
	public interface IHumanAreaBuidingService
	{
		#region Methods

		/// <summary>
		/// 建造します。
		/// </summary>
		/// <param name="commnad">コマンド</param>
		void Build(HumanAreaBuildingCommand commnad);

		/// <summary>
		/// 建造します。
		/// </summary>
		/// <param name="command">コマンド</param>
		Task BuildAsync(HumanAreaBuildingCommand command);

		#endregion
	}
}

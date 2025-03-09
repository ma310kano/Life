namespace Life.Domain.Model;

/// <summary>
/// エリアのコンテキスト
/// </summary>
public interface IAreaContext : IContext
{
	#region Properties

	/// <summary>
	/// 建造物のリポジトリーを取得します。
	/// </summary>
	IAreaBuildingRepository BuildingRepository { get; }

	#endregion
}

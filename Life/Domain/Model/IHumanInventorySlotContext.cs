namespace Life.Domain.Model;

/// <summary>
/// 人間のインベントリースロットのコンテキスト
/// </summary>
public interface IHumanInventorySlotContext : IDisposable
{
	#region Properties

	/// <summary>
	/// ファクトリーを取得します。
	/// </summary>
	IHumanInventorySlotFactory Factory { get; }

	/// <summary>
	/// リポジトリーを取得します。
	/// </summary>
	IHumanInventorySlotRepository Repository { get; }

	#endregion

	#region Methods

	/// <summary>
	/// コミットします。
	/// </summary>
	void Commit();

	/// <summary>
	/// ロールバックします。
	/// </summary>
	void Rollback();

	#endregion
}

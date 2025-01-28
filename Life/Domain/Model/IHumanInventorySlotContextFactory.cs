namespace Life.Domain.Model;

/// <summary>
/// 人間のインベントリースロットのコンテキストファクトリー
/// </summary>
public interface IHumanInventorySlotContextFactory
{
	#region Methods

	/// <summary>
	/// コンテキストを作成します。
	/// </summary>
	/// <returns>作成したコンテキストを返します。</returns>
	IHumanInventorySlotContext Create();

	#endregion
}

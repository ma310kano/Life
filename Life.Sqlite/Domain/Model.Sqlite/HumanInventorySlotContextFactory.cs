namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間のインベントリースロットのコンテキストファクトリー
/// </summary>
public class HumanInventorySlotContextFactory : IHumanInventorySlotContextFactory
{
	#region Methods

	/// <summary>
	/// コンテキストを作成します。
	/// </summary>
	/// <returns>作成したコンテキストを返します。</returns>
	public IHumanInventorySlotContext Create()
	{
		HumanInventorySlotContext context = new();

		return context;
	}

	#endregion
}

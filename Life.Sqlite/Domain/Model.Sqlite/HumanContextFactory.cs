namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間のコンテキストファクトリー
/// </summary>
public class HumanContextFactory : IHumanContextFactory
{
	#region Methods

	/// <summary>
	/// コンテキストを作成します。
	/// </summary>
	/// <returns>作成したコンテキストを返します。</returns>
	public IHumanContext Create()
	{
		HumanContext context = new();

		return context;
	}

	#endregion
}

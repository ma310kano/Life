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
	/// <param name="humanId">人間ID</param>
	/// <returns>作成したコンテキストを返します。</returns>
	public IHumanContext Create(HumanId humanId)
	{
		HumanContext context = new(humanId);

		return context;
	}

	#endregion
}

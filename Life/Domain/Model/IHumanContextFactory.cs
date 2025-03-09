namespace Life.Domain.Model;

/// <summary>
/// 人間のコンテキストファクトリー
/// </summary>
public interface IHumanContextFactory
{
	#region Methods

	/// <summary>
	/// コンテキストを作成します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <returns>作成したコンテキストを返します。</returns>
	IHumanContext Create(HumanId humanId);

	#endregion
}

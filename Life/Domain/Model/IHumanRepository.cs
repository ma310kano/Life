namespace Life.Domain.Model;

/// <summary>
/// 人間のリポジトリー
/// </summary>
public interface IHumanRepository
{
	#region Methods

	/// <summary>
	/// 人間を検索します。
	/// </summary>
	/// <returns>検索した人間を返します。</returns>
	Human Find();

	/// <summary>
	/// 人間を保存します。
	/// </summary>
	/// <param name="human">人間</param>
	void Save(Human human);

	#endregion
}

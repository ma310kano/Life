namespace Life.Domain.Model;

/// <summary>
/// コンテキスト
/// </summary>
public interface IContext : IDisposable
{
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

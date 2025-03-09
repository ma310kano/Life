namespace Life.Domain.Model.Sqlite;

/// <summary>
/// エリアのコンテキストファクトリー
/// </summary>
public class AreaContextFactory : IAreaContextFactory
{
	#region Methods

	/// <summary>
	/// コンテキストを作成します。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	/// <returns>作成したコンテキストを返します。</returns>
	public IAreaContext Create(AreaId areaId)
	{
		AreaContext context = new(areaId);

		return context;
	}

	#endregion
}

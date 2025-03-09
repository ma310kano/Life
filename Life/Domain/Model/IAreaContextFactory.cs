namespace Life.Domain.Model;

/// <summary>
/// エリアのコンテキストファクトリー
/// </summary>
public interface IAreaContextFactory
{
    #region Methods

    /// <summary>
    /// コンテキストを作成します。
    /// </summary>
    /// <param name="areaId">エリアID</param>
    /// <returns>作成したコンテキストを返します。</returns>
    IAreaContext Create(AreaId areaId);

    #endregion
}

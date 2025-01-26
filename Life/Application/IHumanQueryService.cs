using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// 人間の問い合わせサービス
/// </summary>
public interface IHumanQueryService
{
	#region Methods

	/// <summary>
	/// 人間を問い合わせします。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <returns>問い合わせした人間データを返します。</returns>
	HumanData QuerySingle(string humanId);

	/// <summary>
	/// 人間を問い合わせします。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <returns>問い合わせした人間データを返します。</returns>
	Task<HumanData> QuerySingleAsync(string humanId);

	#endregion
}

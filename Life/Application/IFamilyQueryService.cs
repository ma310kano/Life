using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// 家族の問い合わせサービス
/// </summary>
public interface IFamilyQueryService
{
	#region Methods

	/// <summary>
	/// 家族を問い合わせします。
	/// </summary>
	/// <param name="familyId">家族ID</param>
	/// <returns>問い合わせした家族データを返します。</returns>
	FamilyData QuerySingle(string familyId);

	/// <summary>
	/// 家族を問い合わせします。
	/// </summary>
	/// <param name="familyId">家族ID</param>
	/// <returns>問い合わせした家族データを返します。</returns>
	Task<FamilyData> QuerySingleAsync(string familyId);

	#endregion
}

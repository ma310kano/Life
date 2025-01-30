using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// 建造物レシピ概要の問い合わせサービス
/// </summary>
public interface IBuildingRecipeSummaryQueryService
{
	#region Methods

	/// <summary>
	/// 建造物レシピ概要を問い合わせします。
	/// </summary>
	/// <returns>建造物レシピ概要データのコレクションを返します。</returns>
	IEnumerable<BuildingRecipeSummaryData> Query();

	/// <summary>
	/// 建造物レシピ概要を問い合わせします。
	/// </summary>
	/// <returns>建造物レシピ概要データのコレクションを返します。</returns>
	Task<IEnumerable<BuildingRecipeSummaryData>> QueryAsync();

	#endregion
}

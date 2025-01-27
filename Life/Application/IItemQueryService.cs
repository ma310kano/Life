using Life.Application.Data;

namespace Life.Application;

/// <summary>
/// アイテムの問い合わせサービス
/// </summary>
public interface IItemQueryService
{
	#region Methods

	/// <summary>
	/// アイテムを問い合わせします。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <returns>問い合わせしたアイテムデータを返します。</returns>
	ItemData QuerySingle(string itemId);

	/// <summary>
	/// アイテムを問い合わせします。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <returns>問い合わせしたアイテムデータを返します。</returns>
	Task<ItemData> QuerySingleAsync(string itemId);

	#endregion
}

namespace Life.Domain.Model;

/// <summary>
/// アイテムのリポジトリー
/// </summary>
public interface IItemRepository
{
	#region Methods

	/// <summary>
	/// スタック可能かどうかを検索します。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <returns>スタック可能かどうかを返します。</returns>
	bool FindCanStack(ItemId itemId);

	#endregion
}

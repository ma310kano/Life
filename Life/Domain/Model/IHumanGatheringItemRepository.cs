namespace Life.Domain.Model;

/// <summary>
/// 人間の採集アイテムのリポジトリー
/// </summary>
public interface IHumanGatheringItemRepository
{
	#region Methods

	/// <summary>
	/// 追加します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	void Add(HumanId humanId, ItemId itemId);

	/// <summary>
	/// 除去します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	void Remove(HumanId humanId, ItemId itemId);

	#endregion
}

namespace Life.Domain.Model;

/// <summary>
/// 装備アイテムのファクトリー
/// </summary>
public interface IEquipmentItemRepository
{
	#region Methods

	/// <summary>
	/// 装備アイテムを削除します。
	/// </summary>
	/// <param name="item">アイテム</param>
	void Delete(EquipmentItem item);

	/// <summary>
	/// 装備アイテムを検索します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemId">アイテムID</param>
	/// <returns>検索した装備アイテムを返します。</returns>
	EquipmentItem Find(HumanId humanId, ItemId itemId);

	/// <summary>
	/// 装備アイテムを作成します。
	/// </summary>
	/// <param name="item">アイテム</param>
	void Save(EquipmentItem item);

	#endregion
}

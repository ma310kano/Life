namespace Life.Domain.Model;

/// <summary>
/// 装備アイテムのファクトリー
/// </summary>
public interface IEquipmentItemRepository
{
	#region Methods

	/// <summary>
	/// 装備アイテムを追加します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void Add(HumanId humanId, ItemMatterId itemMatterId);

	/// <summary>
	/// 装備アイテムを除去します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void Remove(HumanId humanId, ItemMatterId itemMatterId);

	#endregion
}

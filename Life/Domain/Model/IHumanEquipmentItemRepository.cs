namespace Life.Domain.Model;

/// <summary>
/// 人間の装備アイテムのファクトリー
/// </summary>
public interface IHumanEquipmentItemRepository
{
	#region Methods

	/// <summary>
	/// 装備アイテムを追加します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void Add(ItemMatterId itemMatterId);

	/// <summary>
	/// 装備アイテムを除去します。
	/// </summary>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void Remove(ItemMatterId itemMatterId);

	#endregion
}

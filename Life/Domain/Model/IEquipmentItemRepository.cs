namespace Life.Domain.Model;

/// <summary>
/// 装備アイテムのファクトリー
/// </summary>
public interface IEquipmentItemRepository
{
	#region Methods

	/// <summary>
	/// 装備アイテムを作成します。
	/// </summary>
	/// <param name="item">アイテム</param>
	void Save(EquipmentItem item);

	#endregion
}

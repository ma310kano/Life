namespace Life.Application.Data;

/// <summary>
/// アイテム物質データ
/// </summary>
public interface IItemMatterData
{
	#region Properties

	/// <summary>
	/// アイテム物質IDを取得します。
	/// </summary>
	public string ItemMatterId { get; }

	/// <summary>
	/// アイテムIDを取得します。
	/// </summary>
	public string ItemId { get; }

	/// <summary>
	/// アイテム名を取得します。
	/// </summary>
	public string ItemName { get; }

	/// <summary>
	/// 装備可能かどうかを取得します。
	/// </summary>
	public bool CanEquip { get; }

	#endregion
}

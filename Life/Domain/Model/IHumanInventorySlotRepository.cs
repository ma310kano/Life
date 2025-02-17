namespace Life.Domain.Model;

/// <summary>
/// 人間のインベントリースロットのリポジトリー
/// </summary>
public interface IHumanInventorySlotRepository
{
	#region Methods

	/// <summary>
	/// インベントリースロットを追加します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void Add(HumanId humanId, ItemMatterId itemMatterId);

	/// <summary>
	/// 人間のインベントリースロットを除去します。
	/// </summary>
	/// <param name="humanId">人間ID</param>
	/// <param name="itemMatterId">アイテム物質ID</param>
	void Remove(HumanId humanId, ItemMatterId itemMatterId);

	#endregion
}

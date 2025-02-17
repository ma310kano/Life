namespace Life.Domain.Model;

/// <summary>
/// 人間のコンテキスト
/// </summary>
public interface IHumanContext : IDisposable
{
	#region Properties

	/// <summary>
	/// リポジトリーを取得します。
	/// </summary>
	IHumanRepository Repository { get; }

	/// <summary>
	/// 採集アイテムのリポジトリーを取得します。
	/// </summary>
	IHumanGatheringItemRepository GatheringItemRepository { get; }

	/// <summary>
	/// 建造物レシピのリポジトリーを取得します。
	/// </summary>
	IBuildingRecipeRepository BuildingRecipeRepository { get; }

	/// <summary>
	/// アイテムレシピのリポジトリーを取得します。
	/// </summary>
	IItemRecipeRepository ItemRecipeRepository { get; }

	/// <summary>
	/// アイテムのリポジトリーを取得します。
	/// </summary>
	IItemRepository ItemRepository { get; }

	/// <summary>
	/// アイテム物質のファクトリーを取得します。
	/// </summary>
	IItemMatterFactory ItemMatterFactory { get; }

	/// <summary>
	/// アイテム物質のリポジトリーを取得します。
	/// </summary>
	IItemMatterRepository ItemMatterRepository { get; }

	/// <summary>
	/// 装備アイテムのリポジトリーを取得します。
	/// </summary>
	IEquipmentItemRepository EquipmentItemRepository { get; }

	/// <summary>
	/// インベントリースロットのリポジトリーを取得します。
	/// </summary>
	IHumanInventorySlotRepository InventorySlotRepository { get; }

	/// <summary>
	/// エリアの建造物のリポジトリーを取得します。
	/// </summary>
	IAreaBuildingRepository AreaBuildingRepository { get; }

	#endregion

	#region Methods

	/// <summary>
	/// コミットします。
	/// </summary>
	void Commit();

	/// <summary>
	/// ロールバックします。
	/// </summary>
	void Rollback();

	#endregion
}

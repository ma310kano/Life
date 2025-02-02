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
	/// 装備アイテムのファクトリーを取得します。
	/// </summary>
	IEquipmentItemFactory EquipmentItemFactory { get; }

	/// <summary>
	/// 装備アイテムのリポジトリーを取得します。
	/// </summary>
	IEquipmentItemRepository EquipmentItemRepository { get; }

	/// <summary>
	/// インベントリースロットのファクトリーを取得します。
	/// </summary>
	IHumanInventorySlotFactory InventorySlotFactory { get; }

	/// <summary>
	/// インベントリースロットのリポジトリーを取得します。
	/// </summary>
	IHumanInventorySlotRepository InventorySlotRepository { get; }

	/// <summary>
	/// 建造物レシピのリポジトリーを取得します。
	/// </summary>
	IBuildingRecipeRepository BuildingRecipeRepository { get; }

	/// <summary>
	/// アイテムレシピのリポジトリーを取得します。
	/// </summary>
	IItemRecipeRepository ItemRecipeRepository { get; }

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

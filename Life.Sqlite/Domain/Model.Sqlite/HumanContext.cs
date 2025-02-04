﻿using Life.Sqlite;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間のコンテキスト
/// </summary>
public class HumanContext : IHumanContext
{
	#region Fields

	/// <summary>
	/// コネクション
	/// </summary>
	private readonly IDbConnection _connection;

	/// <summary>
	/// トランザクション
	/// </summary>
	private readonly IDbTransaction _transaction;

	/// <summary>
	/// リポジトリー
	/// </summary>
	private IHumanRepository? _repository;

	/// <summary>
	/// 採集アイテムのリポジトリー
	/// </summary>
	private IHumanGatheringItemRepository? _gatheringItemRepository;

	/// <summary>
	/// 建造物レシピのリポジトリー
	/// </summary>
	private IBuildingRecipeRepository? _buildingRecipeRepository;

	/// <summary>
	/// アイテムレシピのリポジトリー
	/// </summary>
	private IItemRecipeRepository? _itemRecipeRepository;

	/// <summary>
	/// 装備アイテムのファクトリー
	/// </summary>
	private IEquipmentItemFactory? _equipmentItemFactory;

	/// <summary>
	/// 装備アイテムのリポジトリー
	/// </summary>
	private IEquipmentItemRepository? _equipmentItemRepository;

	/// <summary>
	/// インベントリースロットのファクトリー
	/// </summary>
	private IHumanInventorySlotFactory? _inventorySlotFactory;

	/// <summary>
	/// インベントリースロットのリポジトリー
	/// </summary>
	private IHumanInventorySlotRepository? _inventorySlotRepository;

	/// <summary>
	/// エリアの建造物のリポジトリー
	/// </summary>
	private IAreaBuildingRepository? _areaBuildingRepository;

	/// <summary>
	/// 破棄したかどうか
	/// </summary>
	private bool disposedValue;

	#endregion

	#region Constructors

	/// <summary>
	/// 人間のコンテキストを初期化します。
	/// </summary>
	public HumanContext()
	{
		_connection = ConnectionFactory.Create();
		_connection.Open();

		_transaction = _connection.BeginTransaction();
	}

	#endregion

	#region Properties

	/// <summary>
	/// リポジトリーを取得します。
	/// </summary>
	public IHumanRepository Repository => _repository ??= new HumanRepository(_connection, _transaction);

	/// <summary>
	/// 採集アイテムのリポジトリーを取得します。
	/// </summary>
	public IHumanGatheringItemRepository GatheringItemRepository => _gatheringItemRepository ??= new HumanGatheringItemRepository(_connection, _transaction);

	/// <summary>
	/// 建造物レシピのリポジトリーを取得します。
	/// </summary>
	public IBuildingRecipeRepository BuildingRecipeRepository => _buildingRecipeRepository ??= new BuildingRecipeRepository(_connection, _transaction);

	/// <summary>
	/// アイテムレシピのリポジトリーを取得します。
	/// </summary>
	public IItemRecipeRepository ItemRecipeRepository => _itemRecipeRepository ??= new ItemRecipeRepository(_connection, _transaction);

	/// <summary>
	/// 装備アイテムのファクトリーを取得します。
	/// </summary>
	public IEquipmentItemFactory EquipmentItemFactory => _equipmentItemFactory ??= new EquipmentItemFactory();

	/// <summary>
	/// 装備アイテムのリポジトリーを取得します。
	/// </summary>
	public IEquipmentItemRepository EquipmentItemRepository => _equipmentItemRepository ??= new EquipmentItemRepository(_connection, _transaction);

	/// <summary>
	/// インベントリースロットのファクトリーを取得します。
	/// </summary>
	public IHumanInventorySlotFactory InventorySlotFactory => _inventorySlotFactory ??= new HumanInventorySlotFactory();

	/// <summary>
	/// インベントリースロットのリポジトリーを取得します。
	/// </summary>
	public IHumanInventorySlotRepository InventorySlotRepository => _inventorySlotRepository ??= new HumanInventorySlotRepository(_connection, _transaction);

	/// <summary>
	/// エリアの建造物のリポジトリーを取得します。
	/// </summary>
	public IAreaBuildingRepository AreaBuildingRepository => _areaBuildingRepository ??= new AreaBuildingRepository(_connection, _transaction);

	#endregion

	#region Methods

	/// <summary>
	/// コミットします。
	/// </summary>
	public void Commit()
	{
		_transaction.Commit();
	}

	/// <summary>
	/// ロールバックします。
	/// </summary>
	public void Rollback()
	{
		_transaction.Rollback();
	}

	/// <summary>
	/// アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。
	/// </summary>
	/// <param name="disposing">破棄かどうか</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				_transaction.Dispose();
				_connection.Dispose();
			}

			disposedValue = true;
		}
	}

	/// <summary>
	/// アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。
	/// </summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	#endregion
}

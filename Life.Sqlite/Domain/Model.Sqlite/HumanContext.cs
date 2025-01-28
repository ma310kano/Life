using Life.Sqlite;
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
	/// インベントリースロットのファクトリー
	/// </summary>
	private IHumanInventorySlotFactory? _inventorySlotFactory;

	/// <summary>
	/// インベントリースロットのリポジトリー
	/// </summary>
	private IHumanInventorySlotRepository? _inventorySlotRepository;

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
	/// インベントリースロットのファクトリーを取得します。
	/// </summary>
	public IHumanInventorySlotFactory InventorySlotFactory => _inventorySlotFactory ??= new HumanInventorySlotFactory();

	/// <summary>
	/// インベントリースロットのリポジトリーを取得します。
	/// </summary>
	public IHumanInventorySlotRepository InventorySlotRepository => _inventorySlotRepository ??= new HumanInventorySlotRepository(_connection, _transaction);

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

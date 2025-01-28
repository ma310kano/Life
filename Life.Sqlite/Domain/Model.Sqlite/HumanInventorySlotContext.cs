using Life.Sqlite;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// 人間のインベントリースロットのコンテキスト
/// </summary>
public class HumanInventorySlotContext : IHumanInventorySlotContext
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
	/// ファクトリー
	/// </summary>
	private IHumanInventorySlotFactory? _factory;

	/// <summary>
	/// リポジトリー
	/// </summary>
	private IHumanInventorySlotRepository? _repository;

	/// <summary>
	/// 破棄したかどうか
	/// </summary>
	private bool disposedValue;

	#endregion

	#region Constructors

	/// <summary>
	/// 人間のインベントリースロットのコンテキストを初期化します。
	/// </summary>
	public HumanInventorySlotContext()
	{
		_connection = ConnectionFactory.Create();
		_connection.Open();

		_transaction = _connection.BeginTransaction();
	}

	#endregion

	#region Properties

	/// <summary>
	/// ファクトリーを取得します。
	/// </summary>
	public IHumanInventorySlotFactory Factory => _factory ??= new HumanInventorySlotFactory();

	/// <summary>
	/// リポジトリーを取得します。
	/// </summary>
	public IHumanInventorySlotRepository Repository => _repository ??= new HumanInventorySlotRepository(_connection, _transaction);

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

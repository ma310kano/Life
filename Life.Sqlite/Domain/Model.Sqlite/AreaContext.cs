using Life.Sqlite;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// エリアのコンテキスト
/// </summary>
public class AreaContext : IAreaContext
{
	#region Fields

	/// <summary>
	/// エリアID
	/// </summary>
	private readonly AreaId _areaId;

	/// <summary>
	/// コネクション
	/// </summary>
	private readonly IDbConnection _connection;

	/// <summary>
	/// トランザクション
	/// </summary>
	private readonly IDbTransaction _transaction;

	/// <summary>
	/// 建造物のリポジトリー
	/// </summary>
	private IAreaBuildingRepository? _buildingRepository;

	/// <summary>
	/// 破棄したかどうか
	/// </summary>
	private bool disposedValue;

	#endregion

	#region Constructors

	/// <summary>
	/// エリアのコンテキストを初期化します。
	/// </summary>
	/// <param name="areaId">エリアID</param>
	public AreaContext(AreaId areaId)
	{
		_areaId = areaId;

		_connection = ConnectionFactory.Create();
		_connection.Open();

		_transaction = _connection.BeginTransaction();
	}

	#endregion

	#region Properties

	/// <summary>
	/// 建造物のリポジトリーを取得します。
	/// </summary>
	public IAreaBuildingRepository BuildingRepository => _buildingRepository ??= new AreaBuildingRepository(_areaId, _connection, _transaction);

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

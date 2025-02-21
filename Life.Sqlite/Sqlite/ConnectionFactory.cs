using Dapper;
using Life.Sqlite.TypeHandlers;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;

namespace Life.Sqlite;

/// <summary>
/// コネクションファクトリー
/// </summary>
public static class ConnectionFactory
{
	#region Fields

	/// <summary>
	/// 接続文字列
	/// </summary>
	private static readonly string _connectionString;

	#endregion

	#region Constructors

	/// <summary>
	/// コネクションファクトリーを初期化します。
	/// </summary>
	static ConnectionFactory()
	{
		SqlMapper.AddTypeHandler(new BoolHandler());

		DefaultTypeMap.MatchNamesWithUnderscores = true;

		{
			string databaseFilePath;
			{
				string parentDirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("実行中のアセンブリーのディレクトリーが取得できません。");
				const string databaseFileName = "Life.db";

				databaseFilePath = Path.Combine(parentDirPath, databaseFileName);
			}

			_connectionString = "Data Source=" + databaseFilePath;
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// コネクションを作成します。
	/// </summary>
	/// <returns>作成したコネクションを返します。</returns>
	public static IDbConnection Create()
	{
		SqliteConnection connection = new(_connectionString);

		return connection;
	}

	#endregion
}

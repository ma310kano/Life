using Dapper;
using System.Data;

namespace Life.Domain.Model.Sqlite;

/// <summary>
/// アイテムのリポジトリー
/// </summary>
/// <param name="connection">コネクション</param>
/// <param name="transaction">トランザクション</param>
public class ItemRepository(IDbConnection connection, IDbTransaction transaction) : IItemRepository
{
	#region Fields
	
	/// <summary>
	/// コネクション
	/// </summary>
	private readonly IDbConnection _connection = connection;

	/// <summary>
	/// トランザクション
	/// </summary>
	private readonly IDbTransaction _transaction = transaction;

	#endregion

	#region Methods

	/// <summary>
	/// スタック可能かどうかを検索します。
	/// </summary>
	/// <param name="itemId">アイテムID</param>
	/// <returns>スタック可能かどうかを返します。</returns>
	public bool FindCanStack(ItemId itemId)
	{
		const string sql = @"SELECT
	  can_stack
FROM
	items
WHERE
	item_id = :item_id";

		var param = new
		{
			item_id = itemId.Value,
		};

		char c = _connection.QuerySingle<char>(sql, param, _transaction);

		bool canStack = c == '1';

		return canStack;
	}

	#endregion
}

using Dapper;
using System.Data;

namespace Life.Sqlite.TypeHandlers;

/// <summary>
/// bool ハンドラー
/// </summary>
public class BoolHandler : SqlMapper.TypeHandler<bool>
{
	#region Fields

	/// <summary>
	/// 真の文字列
	/// </summary>
	private const string _trueString = "1";

	/// <summary>
	/// 偽の文字列
	/// </summary>
	private const string _falseString = "0";

	#endregion

	#region Methods

	/// <summary>
	/// 値を解析します。
	/// </summary>
	/// <param name="value">値</param>
	/// <returns>解析した結果を返します。</returns>
	/// <exception cref="InvalidOperationException"></exception>
	public override bool Parse(object value)
	{
		string str = (string)value;

		bool result = str switch
		{
			_trueString => true,
			_falseString => false,
			_ => throw new InvalidOperationException("bool 型へ変換できません。"),
		};

		return result;
	}

	/// <summary>
	/// 値を設定します。
	/// </summary>
	/// <param name="parameter">パラメーター</param>
	/// <param name="value">値</param>
	public override void SetValue(IDbDataParameter parameter, bool value)
	{
		parameter.DbType = DbType.String;
		parameter.Value = value ? _trueString : _falseString;
	}
	
	#endregion
}

using System.Reflection.Metadata.Ecma335;

namespace Life.Domain.Model;

/// <summary>
/// 数量
/// </summary>
public record class Quantity
{
    #region Constructors

    /// <summary>
    /// 数量を初期化します。
    /// </summary>
    /// <param name="value">値</param>
    public Quantity(int value)
    {
        bool succeeded = Validate(value, out string message);
        if (!succeeded) throw new ArgumentException(message, nameof(value));

        Value = value;
    }

	#endregion

	#region Operators

	/// <summary>
	/// オペランドの合計を計算します。
	/// </summary>
	/// <param name="lhs">左側のオペランド</param>
	/// <param name="rhs">右側のオペランド</param>
	/// <returns>加算した数量を返します。</returns>
	public static Quantity operator +(Quantity lhs,  int rhs)
    {
        Quantity result = new(lhs.Value + rhs);

        return result;
    }

	/// <summary>
	/// 左側のオペランドから右側のオペランドを減算します。
	/// </summary>
	/// <param name="lhs">左側のオペランド</param>
	/// <param name="rhs">右側のオペランド</param>
	/// <returns>減算した数量を返します。</returns>
	public static Quantity operator -(Quantity lhs, int rhs)
    {
        Quantity result = new(lhs.Value - rhs);

        return result;
    }

	#endregion

	#region Properties

	/// <summary>
	/// 値を取得します。
	/// </summary>
	public int Value { get; }

    #endregion

    #region Methods

    /// <summary>
    /// 値を検証します。
    /// </summary>
    /// <param name="value">値</param>
    /// <param name="message">メッセージ</param>
    public static bool Validate(int value, out string message)
    {
        const int minimumValue = 0;
        const int maximumValue = 999_999_999;
        bool result = minimumValue <= value && value <= maximumValue;

        if (result)
        {
            message = string.Empty;
        }
        else
        {
            message = $"数量は、{minimumValue:#,0} ～ {maximumValue:#,0} の間で入力してください。";
        }

        return result;
    }

    #endregion
}

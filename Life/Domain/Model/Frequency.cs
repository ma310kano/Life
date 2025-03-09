namespace Life.Domain.Model;

/// <summary>
/// 回数
/// </summary>
public record class Frequency
{
    #region Constructors

    /// <summary>
    /// 回数を初期化します。
    /// </summary>
    /// <param name="value">値</param>
    public Frequency(int value)
    {
        bool succeeded = Validate(value, out string message);
        if (!succeeded) throw new ArgumentException(message, nameof(value));

        Value = value;
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
        const int minimumValue = 1;
        const int maximumValue = 99;
        bool result = minimumValue <= value && value <= maximumValue;

        if (result)
        {
            message = string.Empty;
        }
        else
        {
            message = $"回数は、{minimumValue:#,0} ～ {maximumValue:#,0} の間で入力してください。";
        }

        return result;
    }

    #endregion
}

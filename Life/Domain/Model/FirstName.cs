namespace Life.Domain.Model;

/// <summary>
/// 名
/// </summary>
public record class FirstName
{
    #region Constructors

    /// <summary>
    /// 名を初期化します。
    /// </summary>
    /// <param name="value">値</param>
    public FirstName(string value)
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
    public string Value { get; }

    #endregion

    #region Methods

    /// <summary>
    /// 値を検証します。
    /// </summary>
    /// <param name="value">値</param>
    /// <param name="message">メッセージ</param>
    public static bool Validate(string value, out string message)
    {
        const int maximumLength = 10;
        bool result = value.Length <= maximumLength;

        if (result)
        {
            message = string.Empty;
        }
        else
        {
            message = $"名は、{maximumLength}桁以内で入力してください。";
        }

        return result;
    }

    #endregion
}

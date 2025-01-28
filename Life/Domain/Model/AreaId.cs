using System.Text.RegularExpressions;

namespace Life.Domain.Model;

/// <summary>
/// エリアID
/// </summary>
public record class AreaId
{
    #region Constructors

    /// <summary>
    /// エリアIDを初期化します。
    /// </summary>
    /// <param name="value">値</param>
    public AreaId(string value)
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
        bool result;
        {
            const string pattern = "[-0-9a-z]+";

            result = Regex.IsMatch(value, pattern);
        }

        if (result)
        {
            message = string.Empty;
        }
        else
        {
            message = "エリアIDは、半角英数字とハイフンで入力してください。";
        }

        return result;
    }

    #endregion
}

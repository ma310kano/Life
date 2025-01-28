using System.Text.RegularExpressions;

namespace Life.Domain.Model;

/// <summary>
/// 人間ID
/// </summary>
public record class HumanId
{
    #region Constructors

    /// <summary>
    /// 人間IDを初期化します。
    /// </summary>
    /// <param name="value">値</param>
    public HumanId(string value)
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
    /// 人間IDを作成します。
    /// </summary>
    /// <returns>作成した人間IDを返します。</returns>
    public static HumanId Create()
    {
        HumanId product;
        {
            string value = Guid.NewGuid().ToString().ToLower();

            product = new HumanId(value);
        }

        return product;
    }

    /// <summary>
    /// 値を検証します。
    /// </summary>
    /// <param name="value">値</param>
    /// <param name="message">メッセージ</param>
    public static bool Validate(string value, out string message)
    {
        bool result;
        {
            const string pattern = "[0-9a-f]{8}\\-[0-9a-f]{4}\\-[0-9a-f]{4}\\-[0-9a-f]{4}\\-[0-9a-f]{12}";

            result = Regex.IsMatch(value, pattern);
        }

        if (result)
        {
            message = string.Empty;
        }
        else
        {
            message = "人間IDは、UUID の形式で入力してください。";
        }

        return result;
    }

    #endregion
}

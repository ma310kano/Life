using System.Text.RegularExpressions;

namespace Life.Domain.Model;

/// <summary>
/// 建造物ID
/// </summary>
public record class BuildingId
{
    #region Constructors

    /// <summary>
    /// 建造物IDを初期化します。
    /// </summary>
    /// <param name="value">値</param>
    public BuildingId(string value)
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
    /// 建造物IDを作成します。
    /// </summary>
    /// <returns>作成した建造物IDを返します。</returns>
    public static BuildingId Create()
    {
        BuildingId product;
        {
            string value = Guid.NewGuid().ToString().ToLower();

            product = new BuildingId(value);
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
            const string pattern = "^[-0-9a-z]+$";

            result = Regex.IsMatch(value, pattern);
        }

        if (result)
        {
            message = string.Empty;
        }
        else
        {
            message = $"建造物IDは、半角英数字とハイフンで入力してください。";
        }

        return result;
    }

    #endregion
}

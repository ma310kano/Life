namespace Life.Domain.Model;

/// <summary>
/// 人間のインベントリースロット
/// </summary>
/// <param name="humanId">人間ID</param>
/// <param name="itemMatterId">アイテム物質ID</param>
/// <param name="itemId">アイテムID</param>
/// <param name="quantity">数量</param>
public class HumanInventorySlot(HumanId humanId, ItemMatterId itemMatterId, ItemId itemId, Quantity quantity) : IEquatable<HumanInventorySlot>
{
    #region Properties

    /// <summary>
    /// 人間IDを取得します。
    /// </summary>
    public HumanId HumanId { get; } = humanId;

    /// <summary>
    /// アイテム物質IDを取得します。
    /// </summary>
    public ItemMatterId ItemMatterId { get; } = itemMatterId;

    /// <summary>
    /// アイテムIDを取得します。
    /// </summary>
    public ItemId ItemId { get; } = itemId;

    /// <summary>
    /// 数量を取得します。
    /// </summary>
    public Quantity Quantity { get; private set; } = quantity;

    #endregion

    #region Operators

    /// <summary>
    /// オペランドが等しい場合には <c>true</c> を返し、それ以外の場合は <c>false</c> を返します。
    /// </summary>
    /// <param name="lhs">左辺</param>
    /// <param name="rhs">右辺</param>
    /// <returns>オペランドが等しい場合は、 <c>true</c>。それ以外の場合は、 <c>false</c>。</returns>
    public static bool operator ==(HumanInventorySlot lhs, HumanInventorySlot rhs)
    {
        if (lhs is null) return rhs is null;

        bool result = lhs.Equals(rhs);

        return result;
    }

    /// <summary>
    /// オペランドが等しくない場合には <c>true</c> を返し、それ以外の場合は <c>false</c> を返します。
    /// </summary>
    /// <param name="lhs">左辺</param>
    /// <param name="rhs">右辺</param>
    /// <returns>オペランドが等しくない場合は、 <c>true</c>。それ以外の場合は、 <c>false</c>。</returns>
    public static bool operator !=(HumanInventorySlot lhs, HumanInventorySlot rhs)
    {
        bool result = !(lhs == rhs);

        return result;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 数量を加算します。
    /// </summary>
    /// <param name="quantity">数量</param>
    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }

    /// <summary>
    /// 数量を減算します。
    /// </summary>
    /// <param name="quantity">数量</param>
    public void SubtractQuantity(int quantity)
    {
        Quantity -= quantity;
    }

    /// <summary>
    /// 指定されたオブジェクトが現在のオブジェクトと等しいかどうかを判断します。
    /// </summary>
    /// <param name="obj">現在のオブジェクトと比較するオブジェクト。</param>
    /// <returns>指定したオブジェクトが現在のオブジェクトと等しい場合は <c>true</c>。それ以外の場合は <c>false</c>。</returns>
    public override bool Equals(object? obj)
    {
        bool result = obj switch
        {
            HumanInventorySlot other => Equals(other),
            _ => base.Equals(obj),
        };

        return result;
    }
    /// <summary>
    /// 現在のオブジェクトが、同じ型の別のオブジェクトと等しいかどうかを示します。
    /// </summary>
    /// <param name="other">このオブジェクトと比較するオブジェクト。</param>
    /// <returns>現在のオブジェクトが <c>other</c> パラメーターと等しい場合は <c>true</c>、それ以外の場合は <c>false</c> です。</returns>
    public bool Equals(HumanInventorySlot? other)
    {
        if (other is null) return false;

        bool result = HumanId == other.HumanId && ItemMatterId == other.ItemMatterId;

        return result;
    }

    /// <summary>
    /// 既定のハッシュ関数として機能します。
    /// </summary>
    /// <returns>現在のオブジェクトのハッシュ コード。</returns>
    public override int GetHashCode()
    {
        int result = HashCode.Combine(HumanId, ItemMatterId);

        return result;
    }

    /// <summary>
    /// 現在のオブジェクトを表す文字列を返します。
    /// </summary>
    /// <returns>現在のオブジェクトを表す文字列。</returns>
    public override string ToString()
    {
        string str = $"{nameof(HumanInventorySlot)} {{ {nameof(HumanId)} = {HumanId}, {nameof(ItemMatterId)} = {ItemMatterId}, {nameof(ItemId)} = {ItemId}, {nameof(Quantity)} = {Quantity} }}";

        return str;
    }

    #endregion
}

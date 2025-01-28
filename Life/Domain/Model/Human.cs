namespace Life.Domain.Model;

/// <summary>
/// 人間
/// </summary>
/// <param name="humanId">人間ID</param>
/// <param name="firstName">名</param>
/// <param name="familyId">家族ID</param>
/// <param name="areaId">エリアID</param>
public class Human(HumanId humanId, FirstName firstName, FamilyId familyId, AreaId areaId) : IEquatable<Human>
{
    #region Properties

    /// <summary>
    /// 人間IDを取得します。
    /// </summary>
    public HumanId HumanId { get; } = humanId;

    /// <summary>
    /// 名を取得します。
    /// </summary>
    public FirstName FirstName { get; } = firstName;

    /// <summary>
    /// 家族IDを取得します。
    /// </summary>
    public FamilyId FamilyId { get; } = familyId;

    /// <summary>
    /// エリアIDを取得します。
    /// </summary>
    public AreaId AreaId { get; private set; } = areaId;

    #endregion

    #region Operators

    /// <summary>
    /// オペランドが等しい場合には <c>true</c> を返し、それ以外の場合は <c>false</c> を返します。
    /// </summary>
    /// <param name="lhs">左辺</param>
    /// <param name="rhs">右辺</param>
    /// <returns>オペランドが等しい場合は、 <c>true</c>。それ以外の場合は、 <c>false</c>。</returns>
    public static bool operator ==(Human lhs, Human rhs)
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
    public static bool operator !=(Human lhs, Human rhs)
    {
        bool result = !(lhs == rhs);

        return result;
    }

    #endregion

    #region Methods

    /// <summary>
    /// エリアを変更します。
    /// </summary>
    /// <param name="areaId">エリアID</param>
    public void ChangeArea(AreaId areaId)
    {
        AreaId = areaId;
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
            Human other => Equals(other),
            _ => base.Equals(obj),
        };

        return result;
    }
    /// <summary>
    /// 現在のオブジェクトが、同じ型の別のオブジェクトと等しいかどうかを示します。
    /// </summary>
    /// <param name="other">このオブジェクトと比較するオブジェクト。</param>
    /// <returns>現在のオブジェクトが <c>other</c> パラメーターと等しい場合は <c>true</c>、それ以外の場合は <c>false</c> です。</returns>
    public bool Equals(Human? other)
    {
        if (other is null) return false;

        bool result = HumanId == other.HumanId;

        return result;
    }

    /// <summary>
    /// 既定のハッシュ関数として機能します。
    /// </summary>
    /// <returns>現在のオブジェクトのハッシュ コード。</returns>
    public override int GetHashCode()
    {
        int result = HashCode.Combine(HumanId);

        return result;
    }

    /// <summary>
    /// 現在のオブジェクトを表す文字列を返します。
    /// </summary>
    /// <returns>現在のオブジェクトを表す文字列。</returns>
    public override string ToString()
    {
        string str = $"{nameof(Human)} {{ {nameof(HumanId)} = {HumanId}, {nameof(FirstName)} = {FirstName}, {nameof(FamilyId)} = {FamilyId}, {nameof(AreaId)} = {AreaId} }}";

        return str;
    }

    #endregion
}

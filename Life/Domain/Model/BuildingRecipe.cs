namespace Life.Domain.Model;

/// <summary>
/// 建造物レシピ
/// </summary>
/// <param name="buildingRecipeId">建造物レシピID</param>
/// <param name="buildingId">建造物ID</param>
/// <param name="ingredients">材料のコレクション</param>
public class BuildingRecipe(BuildingRecipeId buildingRecipeId, BuildingId buildingId, IReadOnlyCollection<BuildingRecipeIngredient> ingredients) : IEquatable<BuildingRecipe>
{
    #region Properties

    /// <summary>
    /// 建造物レシピIDを取得します。
    /// </summary>
    public BuildingRecipeId BuildingRecipeId { get; } = buildingRecipeId;

    /// <summary>
    /// 建造物IDを取得します。
    /// </summary>
    public BuildingId BuildingId { get; } = buildingId;

    /// <summary>
    /// 材料のコレクションを取得します。
    /// </summary>
    public IReadOnlyCollection<BuildingRecipeIngredient> Ingredients { get; } = ingredients;

    #endregion

    #region Operators

    /// <summary>
    /// オペランドが等しい場合には <c>true</c> を返し、それ以外の場合は <c>false</c> を返します。
    /// </summary>
    /// <param name="lhs">左辺</param>
    /// <param name="rhs">右辺</param>
    /// <returns>オペランドが等しい場合は、 <c>true</c>。それ以外の場合は、 <c>false</c>。</returns>
    public static bool operator ==(BuildingRecipe lhs, BuildingRecipe rhs)
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
    public static bool operator !=(BuildingRecipe lhs, BuildingRecipe rhs)
    {
        bool result = !(lhs == rhs);

        return result;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 指定されたオブジェクトが現在のオブジェクトと等しいかどうかを判断します。
    /// </summary>
    /// <param name="obj">現在のオブジェクトと比較するオブジェクト。</param>
    /// <returns>指定したオブジェクトが現在のオブジェクトと等しい場合は <c>true</c>。それ以外の場合は <c>false</c>。</returns>
    public override bool Equals(object? obj)
    {
        bool result = obj switch
        {
            BuildingRecipe other => Equals(other),
            _ => base.Equals(obj),
        };

        return result;
    }
    /// <summary>
    /// 現在のオブジェクトが、同じ型の別のオブジェクトと等しいかどうかを示します。
    /// </summary>
    /// <param name="other">このオブジェクトと比較するオブジェクト。</param>
    /// <returns>現在のオブジェクトが <c>other</c> パラメーターと等しい場合は <c>true</c>、それ以外の場合は <c>false</c> です。</returns>
    public bool Equals(BuildingRecipe? other)
    {
        if (other is null) return false;

        bool result = BuildingRecipeId == other.BuildingRecipeId;

        return result;
    }

    /// <summary>
    /// 既定のハッシュ関数として機能します。
    /// </summary>
    /// <returns>現在のオブジェクトのハッシュ コード。</returns>
    public override int GetHashCode()
    {
        int result = HashCode.Combine(BuildingRecipeId);

        return result;
    }

    /// <summary>
    /// 現在のオブジェクトを表す文字列を返します。
    /// </summary>
    /// <returns>現在のオブジェクトを表す文字列。</returns>
    public override string ToString()
    {
        string str = $"{nameof(BuildingRecipe)} {{ {nameof(BuildingRecipeId)} = {BuildingRecipeId}, {nameof(BuildingId)} = {BuildingId}, {nameof(Ingredients)} = {Ingredients} }}";

        return str;
    }

    #endregion
}

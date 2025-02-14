namespace Life.Domain.Model;

/// <summary>
/// アイテムレシピ
/// </summary>
/// <param name="itemRecipeId">アイテムレシピID</param>
/// <param name="itemId">アイテムID</param>
/// <param name="quantity">数量</param>
/// <param name="buildingId">建造物ID</param>
/// <param name="ingredients">材料のコレクション</param>
public class ItemRecipe(ItemRecipeId itemRecipeId, ItemId itemId, Quantity quantity, BuildingId? buildingId, IReadOnlyCollection<ItemRecipeIngredient> ingredients) : IEquatable<ItemRecipe>
{
    #region Properties

    /// <summary>
    /// アイテムレシピIDを取得します。
    /// </summary>
    public ItemRecipeId ItemRecipeId { get; } = itemRecipeId;

    /// <summary>
    /// アイテムIDを取得します。
    /// </summary>
    public ItemId ItemId { get; } = itemId;

    /// <summary>
    /// 数量を取得します。
    /// </summary>
    public Quantity Quantity { get; } = quantity;

    /// <summary>
    /// 建造物IDを取得します。
    /// </summary>
    public BuildingId? BuildingId { get; } = buildingId;

    /// <summary>
    /// 材料のコレクションを取得します。
    /// </summary>
    public IReadOnlyCollection<ItemRecipeIngredient> Ingredients { get; } = ingredients;

    #endregion

    #region Operators

    /// <summary>
    /// オペランドが等しい場合には <c>true</c> を返し、それ以外の場合は <c>false</c> を返します。
    /// </summary>
    /// <param name="lhs">左辺</param>
    /// <param name="rhs">右辺</param>
    /// <returns>オペランドが等しい場合は、 <c>true</c>。それ以外の場合は、 <c>false</c>。</returns>
    public static bool operator ==(ItemRecipe lhs, ItemRecipe rhs)
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
    public static bool operator !=(ItemRecipe lhs, ItemRecipe rhs)
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
            ItemRecipe other => Equals(other),
            _ => base.Equals(obj),
        };

        return result;
    }
    /// <summary>
    /// 現在のオブジェクトが、同じ型の別のオブジェクトと等しいかどうかを示します。
    /// </summary>
    /// <param name="other">このオブジェクトと比較するオブジェクト。</param>
    /// <returns>現在のオブジェクトが <c>other</c> パラメーターと等しい場合は <c>true</c>、それ以外の場合は <c>false</c> です。</returns>
    public bool Equals(ItemRecipe? other)
    {
        if (other is null) return false;

        bool result = ItemRecipeId == other.ItemRecipeId;

        return result;
    }

    /// <summary>
    /// 既定のハッシュ関数として機能します。
    /// </summary>
    /// <returns>現在のオブジェクトのハッシュ コード。</returns>
    public override int GetHashCode()
    {
        int result = HashCode.Combine(ItemRecipeId);

        return result;
    }

    /// <summary>
    /// 現在のオブジェクトを表す文字列を返します。
    /// </summary>
    /// <returns>現在のオブジェクトを表す文字列。</returns>
    public override string ToString()
    {
        string str = $"{nameof(ItemRecipe)} {{ {nameof(ItemRecipeId)} = {ItemRecipeId}, {nameof(ItemId)} = {ItemId}, {nameof(Quantity)} = {Quantity}, {nameof(BuildingId)} = {BuildingId}, {nameof(Ingredients)} = {Ingredients} }}";

        return str;
    }

    #endregion
}

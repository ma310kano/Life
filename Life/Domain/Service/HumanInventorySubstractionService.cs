using Life.Domain.Model;

namespace Life.Domain.Service;

/// <summary>
/// 人間のインベントリーの減算サービス
/// </summary>
/// <param name="context">コンテキスト</param>
public class HumanInventorySubstractionService(IHumanContext context)
{
	#region Methods

	/// <summary>
	/// 減算します。
	/// </summary>
	/// <param name="ingredients">材料のコレクション</param>
	/// <param name="frequency">回数</param>
	public void Subtract(IReadOnlyCollection<RecipeIngredient> ingredients)
	{
		static Quantity calculateQuantity(Quantity quantity) => quantity;

		Subtract(ingredients, calculateQuantity);
	}

	/// <summary>
	/// 減算します。
	/// </summary>
	/// <param name="ingredients">材料のコレクション</param>
	/// <param name="frequency">回数</param>
	public void Subtract(IReadOnlyCollection<RecipeIngredient> ingredients, Frequency frequency)
	{
		Quantity calculateQuantity(Quantity quantity) => quantity * frequency;

		Subtract(ingredients, calculateQuantity);
	}

	/// <summary>
	/// 減算します。
	/// </summary>
	/// <param name="ingredients">材料のコレクション</param>
	/// <param name="frequency">回数</param>
	private void Subtract(IReadOnlyCollection<RecipeIngredient> ingredients, Func<Quantity, Quantity> calculateQuantity)
	{
		foreach (RecipeIngredient ingredient in ingredients)
		{
			ItemMatter itemMatter = context.InventorySlotRepository.Find(ingredient.ItemId).Single();

			Quantity quantity = calculateQuantity(ingredient.Quantity);

			itemMatter.SubtractQuantity(quantity);

			if (itemMatter.Remains)
			{
				context.ItemMatterRepository.Save(itemMatter);
			}
			else
			{
				context.InventorySlotRepository.Remove(itemMatter.ItemMatterId);
				context.ItemMatterRepository.Delete(itemMatter);
			}
		}
	}

	#endregion
}

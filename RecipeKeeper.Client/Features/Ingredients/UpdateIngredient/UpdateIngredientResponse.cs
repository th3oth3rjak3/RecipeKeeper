namespace RecipeKeeper.Client.Features.Ingredients.UpdateIngredient;

public class UpdateIngredientResponse : IIngredient
{
    public required int Id { get; set; }
    public required int Position { get; set; }
    public required string Description { get; set; }
}

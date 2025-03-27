namespace RecipeKeeper.Client.Features.Ingredients.CreateIngredient;

public class CreateIngredientResponse : IIngredient
{
    public required int Id { get; set; }
    public required int Position { get; set; }
    public required string Description { get; set; }
}
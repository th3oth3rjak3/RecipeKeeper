namespace RecipeKeeper.Client.Features.Ingredients;

public class IngredientResponse
{
    public required int Id { get; init; }
    public required int Position { get; init; }
    public required string Description { get; init; }
}
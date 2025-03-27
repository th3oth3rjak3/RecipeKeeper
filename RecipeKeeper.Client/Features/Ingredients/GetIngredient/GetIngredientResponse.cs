namespace RecipeKeeper.Client.Features.Ingredients.GetIngredient;

public class GetIngredientResponse : IIngredient
{
    public required int Id { get; set; }
    public required int Position { get; set; }
    public required string Description { get; set; }
}
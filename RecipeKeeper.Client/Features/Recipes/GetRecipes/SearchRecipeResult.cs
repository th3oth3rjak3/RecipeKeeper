using System;

namespace RecipeKeeper.Client.Features.Recipes.GetRecipes;

public class SearchRecipeResult
{
    public string? QueryString { get; set; }
    public bool IncludeIngredients { get; set; }
    public bool IncludeInstructions { get; set; }
}

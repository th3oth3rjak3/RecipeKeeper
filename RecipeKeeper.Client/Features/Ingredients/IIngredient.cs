using System;

namespace RecipeKeeper.Client.Features.Ingredients;

public interface IIngredient
{
    public int Id { get; set; }
    public int Position { get; set; }
    public string Description { get; set; }
}

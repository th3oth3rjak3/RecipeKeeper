using System.ComponentModel.DataAnnotations;
using RecipeKeeper.Client.Features.Recipes.CreateRecipe;
using RecipeKeeper.Client.Features.Recipes.GetRecipes;
using RecipeKeeper.Client.Features.Recipes.UpdateRecipe;
using RecipeKeeper.Features.Ingredients;
using RecipeKeeper.Features.Instructions;

namespace RecipeKeeper.Features.Recipes;

public class Recipe
{
    [Required]
    public required int Id { get; set; }

    [Required]
    [StringLength(250, MinimumLength = 1)]
    public required string Name { get; set; }

    [StringLength(100)]
    public string? Author { get; set; }

    [StringLength(2000)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string? Difficulty { get; set; }

    [StringLength(100)]
    public string? EstimatedDuration { get; set; }

    public List<Ingredient> Ingredients { get; set; } = [];
    public List<Instruction> Instructions { get; set; } = [];
}

public static class RecipeExtensions
{
    public static Recipe ToRecipe(this CreateRecipeRequest request) =>
        new()
        {
            Id = 0,
            Name = request.Name ?? "",
            Description = request.Description,
            Author = request.Author,
            Difficulty = request.Difficulty,
            EstimatedDuration = request.EstimatedDuration,
        };

    public static CreateRecipeResponse ToCreateResponse(this Recipe recipe) =>
        new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Author = recipe.Author,
            Description = recipe.Description,
            Difficulty = recipe.Difficulty,
            EstimatedDuration = recipe.EstimatedDuration,
        };

    public static GetRecipeResponse ToGetResponse(this Recipe recipe) =>
        new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Author = recipe.Author,
            Description = recipe.Description,
            EstimatedDuration = recipe.EstimatedDuration,
            Difficulty = recipe.Difficulty,
            Ingredients = recipe.Ingredients.Select(ingredient => ingredient.ToGetResponse()).ToList(),
            Instructions = recipe.Instructions.Select(instruction => instruction.ToGetResponse()).ToList()
        };

    public static SearchRecipeResponse ToSearchResponse(this Recipe recipe) =>
        new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Author = recipe.Author,
            EstimatedDuration = recipe.EstimatedDuration,
            Difficulty = recipe.Difficulty,
            Description = recipe.Description,
        };

    public static UpdateRecipeResponse ToUpdateResponse(this Recipe recipe) =>
        new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Author = recipe.Author,
            Difficulty = recipe.Difficulty,
            EstimatedDuration = recipe.EstimatedDuration,
            Ingredients = recipe.Ingredients.Select(ingredient => ingredient.ToGetResponse()).ToList(),
            Instructions = recipe.Instructions.Select(instruction => instruction.ToGetResponse()).ToList(),
        };
}
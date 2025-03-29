using System.ComponentModel.DataAnnotations;
using RecipeKeeper.Client.Features.Instructions;
using RecipeKeeper.Client.Features.Instructions.CreateInstruction;
using RecipeKeeper.Features.Recipes;

namespace RecipeKeeper.Features.Instructions;

public class Instruction
{
    [Required]
    public int Id { get; init; }

    [Required]
    public int RecipeId { get; init; }

    [Required]
    public Recipe? Recipe { get; init; }

    [Required]
    public required int Position { get; init; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public required string Description { get; init; }
}

public static class InstructionExtensions
{
    public static Instruction ToInstruction(this CreateInstructionRequest request, int recipeId) =>
        new()
        {
            RecipeId = recipeId,
            Position = request.Position ?? 1,
            Description = request.Description ?? "",
        };

    public static InstructionResponse ToResponse(this Instruction instruction) =>
        new()
        {
            Id = instruction.Id,
            Position = instruction.Position,
            Description = instruction.Description,
        };
}
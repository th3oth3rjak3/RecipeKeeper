using System.ComponentModel.DataAnnotations;
using RecipeKeeper.Client.Features.Instructions.CreateInstruction;
using RecipeKeeper.Client.Features.Instructions.GetInstruction;
using RecipeKeeper.Client.Features.Instructions.UpdateInstruction;
using RecipeKeeper.Features.Recipes;

namespace RecipeKeeper.Features.Instructions;

public class Instruction
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int RecipeId { get; set; }

    [Required]
    public Recipe? Recipe { get; set; }

    [Required]
    public required int Position { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public required string Description { get; set; }
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

    public static CreateInstructionResponse ToCreateResponse(this Instruction instruction) =>
        new()
        {
            Id = instruction.Id,
            Position = instruction.Position,
            Description = instruction.Description,
        };

    public static GetInstructionResponse ToGetResponse(this Instruction instruction) =>
        new()
        {
            Id = instruction.Id,
            Position = instruction.Position,
            Description = instruction.Description,
        };

    public static UpdateInstructionResponse ToUpdateResponse(this Instruction instruction) =>
        new()
        {
            Id = instruction.Id,
            Position = instruction.Position,
            Description = instruction.Description,
        };
}
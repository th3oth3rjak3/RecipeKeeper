using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Client.Features.Instructions.UpdateInstruction;

public class UpdateInstructionRequest
{
    [Required]
    public int? Position { get; set; }

    [Required]
    public string? Description { get; set; }
}
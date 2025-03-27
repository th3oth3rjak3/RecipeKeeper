using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Client.Features.Instructions.CreateInstruction;

public class CreateInstructionRequest
{
    [Required]
    public int? Position { get; set; }

    [Required]
    public string? Description { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Client.Features.Instructions.UpdateInstruction;

public class UpdateInstructionRequest
{
    public int? Id { get; set; }

    [Required]
    public int RecipeId { get; set; }

    [Required]
    public int? Position { get; set; }

    [Required]
    public string? Description { get; set; }
}

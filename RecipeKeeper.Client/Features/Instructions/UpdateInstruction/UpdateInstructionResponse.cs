namespace RecipeKeeper.Client.Features.Instructions.UpdateInstruction;

public class UpdateInstructionResponse : IInstruction
{
    public required int Id { get; set; }
    public required int Position { get; set; }
    public required string Description { get; set; }
}

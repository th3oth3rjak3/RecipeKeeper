namespace RecipeKeeper.Client.Features.Instructions.CreateInstruction;

public class CreateInstructionResponse : IInstruction
{
    public required int Id { get; set; }
    public required int Position { get; set; }
    public required string Description { get; set; }
}

namespace RecipeKeeper.Client.Features.Instructions.CreateInstruction;

public class CreateInstructionResponse
{
    public required int Id { get; set; }
    public required int Position { get; set; }
    public required string Description { get; set; }
}

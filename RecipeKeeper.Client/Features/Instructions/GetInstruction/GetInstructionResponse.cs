namespace RecipeKeeper.Client.Features.Instructions.GetInstruction;

public class GetInstructionResponse
{
    public required int Id { get; set; }
    public required int Position { get; set; }
    public required string Description { get; set; }
}
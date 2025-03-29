namespace RecipeKeeper.Client.Features.Instructions;

public class InstructionResponse
{
    public required int Id { get; init; }
    public required int Position { get; init; }
    public required string Description { get; init; }
}
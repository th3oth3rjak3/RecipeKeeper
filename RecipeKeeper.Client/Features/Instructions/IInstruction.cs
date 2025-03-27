using System;

namespace RecipeKeeper.Client.Features.Instructions;

public interface IInstruction
{
    public int Id { get; set; }
    public int Position { get; set; }
    public string Description { get; set; }
}

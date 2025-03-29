using Functional;
using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Client.Features.Instructions;
using RecipeKeeper.Client.Features.Instructions.CreateInstruction;
using RecipeKeeper.Client.Features.Instructions.UpdateInstruction;
using RecipeKeeper.Persistence;
using static Functional.Prelude;

namespace RecipeKeeper.Features.Instructions;

public class InstructionService(RecipeKeeperContext context) {
    public async Task<Result<InstructionResponse, Exception>> CreateInstructionAsync(int recipeId, CreateInstructionRequest request) =>
        await TryAsync(async () => {
            var instruction = request.ToInstruction(recipeId);
            context.Instructions.Add(instruction);
            await context.SaveChangesAsync();
            return instruction.ToResponse();
        });

    public async Task<Result<InstructionResponse, Exception>> UpdateInstructionAsync(int recipeId, int id, UpdateInstructionRequest request) =>
        await TryAsync(async () => {
            var found = await context.Instructions.Where(instruction => instruction.RecipeId == recipeId && instruction.Id == id).FirstAsync();
            context.Entry(found).CurrentValues.SetValues(request);
            await context.SaveChangesAsync();
            return found.ToResponse();
        });

    public async Task<Result<Unit, Exception>> DeleteInstructionAsync(int recipeId, int id) =>
        await TryAsync(async () => {
            var found = await context.Instructions.Where(instruction => instruction.RecipeId == recipeId && instruction.Id == id).FirstAsync();
            context.Remove(found);
            await context.SaveChangesAsync();
        });
}
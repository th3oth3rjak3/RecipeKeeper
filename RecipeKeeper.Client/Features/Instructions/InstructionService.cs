using System.Net.Http.Json;
using Functional;
using RecipeKeeper.Client.Features.Instructions.CreateInstruction;
using RecipeKeeper.Client.Features.Instructions.UpdateInstruction;
using static Functional.Prelude;

namespace RecipeKeeper.Client.Features.Instructions;

public class InstructionService(HttpClient http)
{
    public async Task<Result<CreateInstructionResponse, Exception>> CreateInstructionAsync(int recipeId, CreateInstructionRequest request) =>
        await TryAsync(async () =>
        {
            var response = await http.PostAsJsonAsync($"/api/Recipes/{recipeId}/Instructions", request);
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while creating new instruction");
            var contents = await response.Content.ReadFromJsonAsync<CreateInstructionResponse>();
            return contents ?? throw new Exception("Error occurred while creating new instruction, response was empty.");
        });

    public async Task<Result<UpdateInstructionResponse, Exception>> UpdateInstructionAsync(int recipeId, int id, UpdateInstructionRequest request) =>
        await TryAsync(async () =>
        {
            var response = await http.PutAsJsonAsync($"/api/Recipes/{recipeId}/Instructions/{id}", request);
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while updating instruction");
            var contents = await response.Content.ReadFromJsonAsync<UpdateInstructionResponse>();
            return contents ?? throw new Exception("Error occurred while updating instruction, response was empty");
        });

    public async Task<Result<Unit, Exception>> DeleteInstructionAsync(int recipeId, int id) =>
        await TryAsync(async () =>
        {
            var response = await http.DeleteAsync($"/api/Recipes/{recipeId}/Instructions/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while deleting instruction");
            return Unit();
        });
}

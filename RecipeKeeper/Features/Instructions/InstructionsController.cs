using Microsoft.AspNetCore.Mvc;
using RecipeKeeper.Client.Features.Instructions;
using RecipeKeeper.Client.Features.Instructions.CreateInstruction;
using RecipeKeeper.Client.Features.Instructions.UpdateInstruction;
using RecipeKeeper.Extensions;
using Serilog;

namespace RecipeKeeper.Features.Instructions;

[Route("api/Recipes/{recipeId:int:required:min(1)}/Instructions")]
[ApiController]
public class InstructionsController(InstructionService instructionService) : ControllerBase {
    [HttpPost("")]
    [Produces<InstructionResponse>]
    public async Task<IResult> CreateInstructionAsync(int recipeId, [FromBody] CreateInstructionRequest request) =>
        await instructionService
            .CreateInstructionAsync(recipeId, request)
            .OkResult(Log.Logger);

    [HttpPut("{id:int:required:min(1)}")]
    [Produces<InstructionResponse>]
    public async Task<IResult> UpdateInstructionAsync(int recipeId, int id, [FromBody] UpdateInstructionRequest request) =>
        await instructionService
            .UpdateInstructionAsync(recipeId, id, request)
            .OkResult(Log.Logger);

    [HttpDelete("{id:int:required:min(1)}")]
    public async Task<IResult> DeleteInstructionAsync(int recipeId, int id) =>
        await instructionService
            .DeleteInstructionAsync(recipeId, id)
            .NoContentResult(Log.Logger);
}
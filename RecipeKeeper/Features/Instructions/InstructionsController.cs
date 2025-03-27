using Microsoft.AspNetCore.Mvc;
using RecipeKeeper.Client.Features.Instructions.CreateInstruction;
using RecipeKeeper.Client.Features.Instructions.UpdateInstruction;
using RecipeKeeper.Extensions;
using Serilog;

namespace RecipeKeeper.Features.Instructions
{
    [Route("api/Recipes")]
    [ApiController]
    public class InstructionsController(InstructionService instructionService) : ControllerBase
    {
        [HttpPost("{recipeId:int:required:min(1)}/Instructions")]
        [Produces<CreateInstructionResponse>]
        public async Task<IResult> CreateInstructionAsync(int recipeId, [FromBody] CreateInstructionRequest request) =>
            await instructionService
                .CreateInstructionAsync(recipeId, request)
                .OkResult(Log.Logger);

        [HttpPut("{recipeId:int:required:min(1)}/Instructions/{id:int:required:min(1)}")]
        [Produces<UpdateInstructionResponse>]
        public async Task<IResult> UpdateInstructionAsync(int recipeId, int id, [FromBody] UpdateInstructionRequest request) =>
            await instructionService
                .UpdateInstructionAsync(recipeId, id, request)
                .OkResult(Log.Logger);

        [HttpDelete("{recipeId:int:required:min(1)}/Instructions/{id:int:required:min(1)}")]
        public async Task<IResult> DeleteInstructionAsync(int recipeId, int id) =>
            await instructionService
                .DeleteInstructionAsync(recipeId, id)
                .NoContentResult(Log.Logger);
    }
}

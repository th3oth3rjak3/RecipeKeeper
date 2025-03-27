using Functional;
using static Functional.Prelude;

namespace RecipeKeeper.Extensions;

public static class IResultExtensions
{
    public static async Task<IResult> OkResult<T>(this Task<Result<T, Exception>> result, Serilog.ILogger logger) =>
        await result
            .TapErrorAsync(LogErrors<T>(logger))
            .MatchAsync(
                ok => Results.Ok(ok),
                () => Results.InternalServerError());

    public static async Task<IResult> NoContentResult<T>(this Task<Result<T, Exception>> result, Serilog.ILogger logger) =>
        await result
        .TapErrorAsync(LogErrors<T>(logger))
        .MatchAsync(
            ok => Results.NoContent(),
            err => Results.InternalServerError());

    public static Action<Exception> LogErrors<T>(Serilog.ILogger logger) =>
        (err) =>
        {
            logger.Error("Error for type {Type}: {Error}", typeof(T).Name, err.Message);
        };
}

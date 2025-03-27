using static Functional.Prelude;

namespace RecipeKeeper.Client.Builders;

public class QueryBuilder
{
    private Dictionary<string, string?> queryParams = [];

    public QueryBuilder Add(string property, object? value) =>
        Effect(() => queryParams.Add(property, value?.ToString()))
            .Pipe(() => this);

    public override string ToString() =>
        queryParams
            .Where(kvp => !string.IsNullOrEmpty(kvp.Value?.ToString()))
            .Select(kvp => $"{kvp.Key}={kvp.Value}")
            .Pipe(values => string.Join("&", values));
}

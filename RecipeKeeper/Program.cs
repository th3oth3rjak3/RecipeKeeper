using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using RecipeKeeper.Components;
using RecipeKeeper.Features.Ingredients;
using RecipeKeeper.Features.Instructions;
using RecipeKeeper.Features.Persistence;
using RecipeKeeper.Features.Recipes;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((ctx, config) =>
{
    if (ctx.HostingEnvironment.IsDevelopment())
    {
        config
            .MinimumLevel.Debug()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                theme: AnsiConsoleTheme.Code,
                applyThemeToRedirectedOutput: true);
    }
    else
    {
        config
            .MinimumLevel.Information()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                theme: AnsiConsoleTheme.Code,
                applyThemeToRedirectedOutput: true)
            .WriteTo.File(
                new JsonFormatter(),
                "logs/recipe_keeper.json",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30);
    }
});

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddControllers();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Server Services
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<IngredientService>();
builder.Services.AddScoped<InstructionService>();

builder.Services.AddDbContext<RecipeKeeperContext>(options =>
{

    options.UseSqlite(
        builder.Configuration.GetConnectionString("RecipeKeeper"),
        opts =>
        {
            opts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });
});

builder.Services.AddOpenApi("index");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
        .WithTitle("Recipe Keeper API")
        .WithTheme(ScalarTheme.Saturn)
        .WithSidebar(true)
        .WithDarkMode(true)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(RecipeKeeper.Client._Imports).Assembly);

app.Run();

using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Features.Ingredients;
using RecipeKeeper.Features.Instructions;
using RecipeKeeper.Features.Recipes;

namespace RecipeKeeper.Persistence;

public class RecipeKeeperContext(DbContextOptions<RecipeKeeperContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Instruction> Instructions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Recipe).Assembly);
    }
}
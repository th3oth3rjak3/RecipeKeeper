using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeKeeper.Features.Instructions;

public class InstructionConfiguration : IEntityTypeConfiguration<Instruction>
{
    public void Configure(EntityTypeBuilder<Instruction> builder)
    {
        builder.ToTable("Instructions");
        builder.HasKey(instruction => instruction.Id);
        builder.Property(instruction => instruction.Position).IsRequired();
        builder.Property(instruction => instruction.Description).IsRequired();

        builder
            .HasOne(instruction => instruction.Recipe)
            .WithMany(recipe => recipe.Instructions)
            .HasForeignKey(instruction => instruction.RecipeId);
    }
}

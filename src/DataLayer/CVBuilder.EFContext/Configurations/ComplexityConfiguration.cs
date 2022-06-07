using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class ComplexityConfiguration:IEntityTypeConfiguration<TeamBuild>
{
    public void Configure(EntityTypeBuilder<TeamBuild> builder)
    {
        builder.HasOne(x => x.Complexity).WithMany(x => x.TeamBuilds).OnDelete(DeleteBehavior.SetNull);
    }
}
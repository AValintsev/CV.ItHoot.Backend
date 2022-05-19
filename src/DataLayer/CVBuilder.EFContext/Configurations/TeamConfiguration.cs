using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class TeamConfiguration: IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams");
        builder.HasMany(x => x.Resumes).WithOne(x => x.Team);
        builder.HasOne(x => x.CreatedUser).WithMany("CreatedTeams");
        builder.HasOne(x => x.Client).WithMany("ClientTeams");
    }
}
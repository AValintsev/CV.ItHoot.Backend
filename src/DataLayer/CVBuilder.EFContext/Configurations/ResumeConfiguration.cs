﻿using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class ResumeConfiguration:IEntityTypeConfiguration<Resume>
{
    public void Configure(EntityTypeBuilder<Resume> builder)
    {
        builder.ToTable("Resumes");
        builder.HasOne(x => x.Position).WithMany(x => x.Resumes).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(x => x.CreatedUser).WithMany("CreatedResumes");
    }
}
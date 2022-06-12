using System;
using CVBuilder.EFContext.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CVBuilder.EFContext.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //todo
            //new DefaultRolesCreator(modelBuilder).Create();
            //new DefaultLanguagesCreator(modelBuilder).Create();
            //new DefaultTranslationsCreator(modelBuilder).Create();
        }

        public static void UseDataProtection(this ModelBuilder modelBuilder, ILookupProtectorKeyRing keyRing, ILookupProtector protector)
        {
            var converter = new ValueConverter<string, string>(
                s => protector.Protect(keyRing.CurrentKeyId, s),
                s => protector.Unprotect(keyRing.CurrentKeyId, s));

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    if (property.ClrType != typeof(string)
                        || !Attribute.IsDefined(property.PropertyInfo, typeof(ProtectedPersonalDataAttribute)))
                    {
                        continue;
                    }

                    modelBuilder
                        .Entity(entity.ClrType)
                        .Property(typeof(string), property.Name)
                        .HasConversion(converter);
                }
            }
        }

        public static void ConfigureEntities(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProposalConfiguration).Assembly);
            //modelBuilder.Entity<CvEducation>()
            //    .HasKey(x => new {x.CvId, x.EducationId});
            //modelBuilder.Entity<CvExperience>()
            //    .HasKey(x => new { x.CvId, x.ExperienceId });
            //modelBuilder.Entity<CvSkill>()
            //    .HasKey(x => new { x.CvId, x.SkillId });
            //modelBuilder.Entity<CvUserLanguage>()
            //    .HasKey(x => new { x.CvId, x.UserLanguageId });
            //modelBuilder.Entity<AccessToken>()
            //    .HasIndex(p => p.Token);
        }
    }
}

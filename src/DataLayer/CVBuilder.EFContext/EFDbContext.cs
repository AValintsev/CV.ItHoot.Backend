using CVBuilder.EFContext.Extensions;
using CVBuilder.Models;
using CVBuilder.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.EFContext
{
    public class EFDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Resume> Resumes { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<LevelSkill> LevelSkills { get; set; }
        public virtual  DbSet<LevelLanguage> LevelLanguages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<LevelLanguage>()
            //    .HasKey(bc => new { bc.UserLanguageId, bc.CvId, bc.Id });
            //modelBuilder.Entity<LevelLanguage>()
            //    .HasOne(bc => bc.UserLanguage)
            //    .WithMany(b => b.LevelLanguages)
            //    .HasForeignKey(bc => bc.UserLanguageId);
            //modelBuilder.Entity<LevelLanguage>()
            //    .HasOne(bc => bc.Cv)
            //    .WithMany(c => c.LevelLanguages)
            //    .HasForeignKey(bc => bc.CvId);


            //modelBuilder.Entity<LevelSkill>()
            //    .HasKey(bc => new { bc.SkillId, bc.CvId, bc.Id });
            //modelBuilder.Entity<LevelSkill>()
            //    .HasOne(bc => bc.Skill)
            //    .WithMany(b => b.LevelSkills)
            //    .HasForeignKey(bc => bc.SkillId);
            //modelBuilder.Entity<LevelSkill>()
            //    .HasOne(bc => bc.Cv)
            //    .WithMany(c => c.LevelSkills)
            //    .HasForeignKey(bc => bc.CvId);

            modelBuilder.ConfigureEntities();
            ////todo
            ////modelBuilder.UseDataProtection(
            ////    this.GetService<ILookupProtectorKeyRing>(),
            ////    this.GetService<ILookupProtector>());
            ///

            modelBuilder.Seed();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Commands.SharedCommands;
using CVBuilder.Application.CV.Responses.CvResponse;

namespace CVBuilder.Application.CV.Mapper
{
    using Models.Entities;

    class CVMapper : AppMapperBase
    {
        public CVMapper()
        {
            #region Commands

            CreateMap<CreateCvCommand, Cv>()
                .ForMember(x => x.LevelSkills, y => y.MapFrom(z => z.Skills))
                .ForMember(x => x.LevelLanguages, y => y.MapFrom(z => z.UserLanguages))
                .ForMember(x => x.Educations, y => y.MapFrom(x => x.Educations))
                .ForMember(x => x.Experiences, y => y.MapFrom(x => x.Experiences));

            CreateMap<UserLanguageCommand, LevelLanguage>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.UserLanguageId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.LanguageLevel, y => y.MapFrom(z => z.Level))
                .ForMember(x => x.UserLanguage, y => y.MapFrom(z =>
                    new UserLanguage()
                    {
                        Id = z.Id.GetValueOrDefault(),
                        Name = z.Name,
                        CreatedAt = DateTime.UtcNow
                    }));

            CreateMap<SkillCommand, LevelSkill>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.SkillId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.SkillLevel, y => y.MapFrom(z => z.Level))
                .ForMember(x => x.Skill, y => y.MapFrom(z =>
                    new Skill()
                    {
                        Id = z.Id.GetValueOrDefault(),
                        Name = z.Name,
                        CreatedAt = DateTime.UtcNow
                    }));
            CreateMap<EducationCommand, Education>()
                .ForMember(x => x.StartDate, y => y.MapFrom(z => z.StartDate.GetValueOrDefault().Date))
                .ForMember(x => x.EndDate, y => y.MapFrom(z => z.EndDate.GetValueOrDefault().Date));
            CreateMap<ExperienceCommand, Experience>()
                .ForMember(x => x.StartDate, y => y.MapFrom(z => z.StartDate.GetValueOrDefault().Date))
                .ForMember(x => x.EndDate, y => y.MapFrom(z => z.EndDate.GetValueOrDefault().Date));

            #endregion

            #region Result

            CreateMap<Cv, CvResult>()
                .ForMember(q => q.Picture, w => w.MapFrom(f => GetFile(f)))
                .ForMember(d => d.Skills, b => b.MapFrom(s => MapToSkillResult(s.LevelSkills)))
                .ForMember(d => d.UserLanguages, b => b.MapFrom(s => MapToUserLanguageResult(s.LevelLanguages)));

            CreateMap<Education, EducationResult>();
            CreateMap<Experience, ExperienceResult>();

            #endregion
        }

        private static List<UserLanguageResult> MapToUserLanguageResult(IEnumerable<LevelLanguage> levelLanguages)
        {
            return levelLanguages.Select(MapToUserLanguage).ToList();

            UserLanguageResult MapToUserLanguage(LevelLanguage levelLanguage)
            {
                return new UserLanguageResult
                {
                    Id = levelLanguage.UserLanguage.Id,
                    Name = levelLanguage.UserLanguage.Name,
                    Level = (int) levelLanguage.LanguageLevel,
                };
            }
        }

        private static List<SkillResult> MapToSkillResult(IEnumerable<LevelSkill> levelSkill)
        {
            return levelSkill.Select(MapToSkill).ToList();

            SkillResult MapToSkill(LevelSkill skill)
            {
                return new SkillResult
                {
                    Id = skill.SkillId,
                    Name = skill.Skill.Name,
                    Level = (int) skill.SkillLevel,
                };
            }
        }


        public static string GetFile(Cv cv)
        {
            if (cv.Files != null)
            {
                var rsoult = cv.Files.Count > 0
                    //todo Remove from mapper
                    ? "https://localhost:5001/api/v1/file/id?id=" + cv.Files[0].Id
                    : null;

                return rsoult;
            }

            return null;
        }

        public static string GetPictureUrl(File file)
        {
            //todo Remove from mapper
            return @"https://localhost:5001/api/v1/file/id?id=" + file.Id;
        }
    }
}
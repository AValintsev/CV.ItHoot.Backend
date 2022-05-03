using System;
using System.Collections.Generic;
using System.Linq;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Commands.SharedCommands;
using CVBuilder.Application.CV.Responses;
using CVBuilder.Application.CV.Responses.CvResponse;

namespace CVBuilder.Application.CV.Mapper
{
    using Models.Entities;
    class CVMapper : AppMapperBase
    {
        public CVMapper()
        {

            CreateMap<CreateCvCommand, Cv>()
                .ForMember(e => e.Files, d => d.MapFrom(c => c.Picture))
                .ForMember(x => x.LevelSkills, y => y.MapFrom(z => z.Skills));
            CreateMap<CVSkill, Skill>();
            CreateMap<CVSkill, LevelSkill>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.SkillId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.SkillLevel, y => y.MapFrom(z => z.Level))
                .ForMember(x => x.Skill, y => y.MapFrom(z => z.Id == null
                    ? new Skill()
                    {
                        Name = z.Name,
                        CreatedAt = DateTime.UtcNow
                    }
                    : null));
            CreateMap<CreateFileCommand, File>()
                .ForMember(p => p.ContentType, b => b.MapFrom(c => c.ContentType))
                .ForMember(p => p.Data, b => b.MapFrom(c => c.Data));


            CreateMap<Cv, CvCardResult>();

            CreateMap<Cv, CvResult>()
                .ForMember(q => q.Picture, w => w.MapFrom(f => GetFile(f)))
                .ForMember(d => d.Skills, b => b.MapFrom(s => MapToSkillResult(s.LevelSkills)))
                .ForMember(d => d.UserLanguages, b => b.MapFrom(s => MapToUserLanguageResult(s.LevelLanguages)));

            CreateMap<Experience, ExperienceResult>();
            CreateMap<Education, EducationResult>();

            CreateMap<EducationCommand, Education>();
            CreateMap<SkillCommand, Skill>();
            CreateMap<UserLanguageCommand, UserLanguage>();

            CreateMap<UpdateCvCommand, UpdateCvResult>();
        }

        private static List<UserLanguageResult> MapToUserLanguageResult(IEnumerable<LevelLanguage> levelLanguages)
        {
            return  levelLanguages.Select(MapToUserLanguage).ToList();

            UserLanguageResult MapToUserLanguage(LevelLanguage levelLanguage)
            {
                return new UserLanguageResult
                {
                    CvId = levelLanguage.CvId,
                    LanguageId = levelLanguage.UserLanguageId,
                    Name = levelLanguage.UserLanguage.Name,
                    Level = (int)levelLanguage.LanguageLevel,
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
                    Level = (int)skill.SkillLevel,
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

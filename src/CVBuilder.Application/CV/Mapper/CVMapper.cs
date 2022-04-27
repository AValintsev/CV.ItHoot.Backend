using System.Collections.Generic;
using System.Linq;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Commands.sharedCommands;
using CVBuilder.Application.CV.Responses;
using CVBuilder.Application.CV.Responses.CvResponses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.CV.Mapper
{
    class CVMapper : AppMapperBase
    {
        public CVMapper()
        {

            CreateMap<CreateCvCommand, Cv>()
                .ForMember(e => e.Files, d => d.MapFrom(c => c.Picture));


            CreateMap<CreateFileComand, File>()
                .ForMember(p => p.ContentType, b => b.MapFrom(c => c.ContentType))
                .ForMember(p => p.Data, b => b.MapFrom(c => c.Data));
            

            CreateMap<Cv, CvCardResult>();

            CreateMap<Cv, CvResult>()
                .ForMember(q => q.Picture, w => w.MapFrom(f => GetFile(f)))
                .ForMember(d => d.Skills, b => b.MapFrom(s => MapToSkillResult(s.LevelSkills)))
                .ForMember(d => d.UserLanguages, b => b.MapFrom(s => MapToUserLanguageResult(s.LevelLanguages)));

            CreateMap<Experience, ExpirianceResult>();
            CreateMap<Models.Entities.Education, EducationResult>();

            CreateMap<EducationCommand, CVBuilder.Models.Entities.Education>();
            CreateMap<SkillCommand, Models.Entities.Skill>();
            CreateMap<UserLanguageCommand, UserLanguage>();

            CreateMap<UpdateCvCommand, UpdateCvResult>();
        }

        public static List<UserLanguageResult> MapToUserLanguageResult(List<LevelLanguage> levelLanguages)
        {
            return  levelLanguages.Select(e => MapToUserLanguageResult(e)).ToList();

            UserLanguageResult MapToUserLanguageResult(LevelLanguage levelLanguage)
            {
                return new()
                {
                    CvId = levelLanguage.CvId,
                    LanguageId = levelLanguage.UserLanguageId,
                    Name = levelLanguage.UserLanguage.Name,
                    Level = (int)levelLanguage.LanguageLevel,
                };
            }
        }

        public static List<SkillResult> MapToSkillResult(List<LevelSkill> levelSkill)
        {
            return levelSkill.Select(e => MapToSkillResult(e)).ToList();

            SkillResult MapToSkillResult(LevelSkill levelSkill)
            {
                return new()
                {
                    CvId = levelSkill.CvId,
                    SkillId = levelSkill.SkillId,
                    Name = levelSkill.Skill.Name,
                    Level = (int)levelSkill.SkillLevel,
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

        public static string GetPictureUrl(Models.Entities.File file)
        {
            //todo Remove from mapper
            return @"https://localhost:5001/api/v1/file/id?id=" + file.Id;
        }
    }
}

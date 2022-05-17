using System;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Commands.SharedCommands;
using CVBuilder.Application.Resume.Responses.CvResponse;

namespace CVBuilder.Application.Resume.Mapper;

using Models.Entities;

class UpdateCvMapper : AppMapperBase
{
    public UpdateCvMapper()
    {
        #region Request

        CreateMap<UpdateResumeCommand, Resume>()
            .ForMember(x => x.LevelSkills, y => y.MapFrom(z => z.Skills))
            .ForMember(x => x.LevelLanguages, y => y.MapFrom(z => z.Languages))
            .ForMember(x => x.Educations, y => y.MapFrom(x => x.Educations))
            .ForMember(x => x.Experiences, y => y.MapFrom(x => x.Experiences));

        CreateMap<UpdateLanguageCommand, LevelLanguage>()
            .ForMember(x => x.LanguageId, y => y.MapFrom(z => z.LanguageId))
            .ForMember(x => x.LanguageLevel, y => y.MapFrom(z => z.Level))
            .ForMember(x => x.Language, y => y.MapFrom(z =>
                new Language()
                {
                    Id = z.LanguageId,
                    Name = z.LanguageName,
                    UpdatedAt = DateTime.UtcNow
                }));

        CreateMap<UpdateSkillCommand, LevelSkill>()
            .ForMember(x => x.SkillId, y => y.MapFrom(z => z.SkillId))
            .ForMember(x => x.SkillLevel, y => y.MapFrom(z => z.Level))
            .ForMember(x => x.Skill, y => y.MapFrom(z =>
                new Skill()
                {
                    Id = z.SkillId,
                    Name = z.SkillName,
                    UpdatedAt = DateTime.UtcNow
                }));
        CreateMap<UpdateEducationCommand, Education>();
        CreateMap<UpdateExperienceCommand, Experience>();

        #endregion


        #region Result

        CreateMap<Resume, UpdateCvResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));

        #endregion
    }
}
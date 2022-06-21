using System;
using System.Collections.Generic;
using System.Linq;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Commands.SharedCommands;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Application.Resume.Responses.CvResponse;

namespace CVBuilder.Application.Resume.Mapper;

using Models.Entities;

class ResumeMapper : AppMapperBase
{
    public ResumeMapper()
    {
        #region CreateResume

        CreateMap<CreateResumeCommand, Resume>()
            .ForMember(x => x.LevelSkills, y => y.MapFrom(z => z.Skills))
            .ForMember(x => x.CreatedUserId, y => y.MapFrom(z => z.UserId))
            .ForMember(x => x.LevelLanguages, y => y.MapFrom(z => z.UserLanguages))
            .ForMember(x => x.Educations, y => y.MapFrom(x => x.Educations))
            .ForMember(x => x.Experiences, y => y.MapFrom(x => x.Experiences));

        CreateMap<CreateLanguageCommand, LevelLanguage>()
            .ForMember(x => x.LanguageId, y => y.MapFrom(z => z.LanguageId))
            .ForMember(x => x.LanguageLevel, y => y.MapFrom(z => z.Level))
            .ForMember(x => x.Language, y => y.MapFrom(z =>
                new Language()
                {
                    Id = z.LanguageId,
                    Name = z.LanguageName,
                    CreatedAt = DateTime.UtcNow
                }));

        CreateMap<CreateSkillCommand, LevelSkill>()
            .ForMember(x => x.SkillId, y => y.MapFrom(z => z.SkillId))
            .ForMember(x => x.SkillLevel, y => y.MapFrom(z => z.Level))
            .ForMember(x => x.Skill, y => y.MapFrom(z =>
                new Skill()
                {
                    Id = z.SkillId,
                    Name = z.SkillName,
                    CreatedAt = DateTime.UtcNow
                }));
        CreateMap<CreateEducationCommand, Education>();
        CreateMap<CreateExperienceCommand, Experience>();

        #endregion

        CreateMap<UploadResumeImageCommand, Image>();


        #region Result

        CreateMap<Resume, ResumeResult>()
            .ForMember(q => q.Picture, w => w.MapFrom(f => f.Image.ImagePath))
            .ForMember(d => d.Skills, b => b.MapFrom(s => MapToSkillResult(s.LevelSkills)))
            .ForMember(d => d.Languages, b => b.MapFrom(s => MapToUserLanguageResult(s.LevelLanguages)));

        CreateMap<Education, EducationResult>();
        CreateMap<Experience, ExperienceResult>();
        CreateMap<Resume, ResumeCardResult>()
            .ForMember(x => x.Skills, y => y.MapFrom(x => x.LevelSkills))
            .ForMember(x => x.PositionName, y => y.MapFrom(z => z.Position.PositionName));
        CreateMap<LevelSkill, SkillResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.SkillName, y => y.MapFrom(z => z.Skill.Name));

        CreateMap<ResumeTemplate, ResumeTemplateResult>()
            .ForMember(x => x.TemplateId, y => y.MapFrom(z => z.Id));

        #endregion
    }

    #region Methods

    private static List<LanguageResult> MapToUserLanguageResult(IEnumerable<LevelLanguage> levelLanguages)
    {
        return levelLanguages.Select(MapToUserLanguage).ToList();

        LanguageResult MapToUserLanguage(LevelLanguage levelLanguage)
        {
            return new LanguageResult
            {
                Id = levelLanguage.Id,
                LanguageId = levelLanguage.LanguageId,
                LanguageName = levelLanguage?.Language?.Name,
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
                Id = skill.Id,
                SkillId = skill.SkillId,
                SkillName = skill?.Skill?.Name,
                Level = (int) skill.SkillLevel,
            };
        }
    }

    #endregion
}
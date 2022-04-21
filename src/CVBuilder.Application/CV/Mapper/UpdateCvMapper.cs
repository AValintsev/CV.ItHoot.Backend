using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Commands.sharedCommands;
using CVBuilder.Application.CV.Responses.CvResponses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.CV.Mapper
{
    class UpdateCvMapper : AppMapperBase
    {
        public UpdateCvMapper()
        {
            CreateMap<UpdateCvCommand,Cv>().ReverseMap();
            CreateMap<EducationCommand, CVBuilder.Models.Entities.Education>().ReverseMap();
            CreateMap<ExperienceCommand, Experience>().ReverseMap();
            CreateMap<UserLanguageCommand, UserLanguage>().ReverseMap();
            CreateMap<SkillCommand, Models.Entities.Skill>().ReverseMap();

            CreateMap<Cv, UpdateCvResult>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));
        }
    }
}

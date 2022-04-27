using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Commands.SharedCommands;
using CVBuilder.Application.CV.Responses.CvResponse;

namespace CVBuilder.Application.CV.Mapper
{
    using Models.Entities;
    class UpdateCvMapper : AppMapperBase
    {
        public UpdateCvMapper()
        {
            CreateMap<UpdateCvCommand,Cv>().ReverseMap();
            CreateMap<EducationCommand, Education>().ReverseMap();
            CreateMap<ExperienceCommand, Experience>().ReverseMap();
            CreateMap<UserLanguageCommand, UserLanguage>().ReverseMap();
            CreateMap<SkillCommand, Skill>().ReverseMap();

            CreateMap<Cv, UpdateCvResult>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));
        }
    }
}

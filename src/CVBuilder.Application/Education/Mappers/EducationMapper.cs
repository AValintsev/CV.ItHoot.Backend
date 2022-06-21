using CVBuilder.Application.Education.Commands;
using CVBuilder.Application.Education.Responses;

namespace CVBuilder.Application.Education.Mappers
{
    using Models.Entities;

    internal class EducationMapper : AppMapperBase
    {
        public EducationMapper()
        {
            CreateMap<Education, CreateEducationCommand>().ReverseMap();

            //CreateMap<CVBuilder.Models.Entities.Education, GetEducationByIdComand>().ReverseMap();
            CreateMap<Education, EducationByIdResult>().ReverseMap();
            CreateMap<Education, CreateEducationResult>();
        }
    }
}
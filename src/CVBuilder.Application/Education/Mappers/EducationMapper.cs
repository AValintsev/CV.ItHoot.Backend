using CVBuilder.Application.Education.Response;
using CVBuilder.Application.Education.Commands;

namespace CVBuilder.Application.Education.Mappers
{
    internal class EducationMapper : AppMapperBase
    {
        public EducationMapper()
        {
            CreateMap<CVBuilder.Models.Entities.Education, CreateEducationCommand>().ReverseMap();

            //CreateMap<CVBuilder.Models.Entities.Education, GetEducationByIdComand>().ReverseMap();
            CreateMap<CVBuilder.Models.Entities.Education, EducationByIdResult>().ReverseMap();
            CreateMap<CVBuilder.Models.Entities.Education, CreateEducationResult>();
            
        }
    }
}

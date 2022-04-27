using CVBuilder.Application.Education.Comands;
using CVBuilder.Application.Education.Commands;
using CVBuilder.Web.Contracts.V1.Requests.Educatio;

namespace CVBuilder.Web.Mappers
{
    public class EducationMapper : MapperBase
    {
        public EducationMapper()
        {
            CreateMap<CreateEducation, CreateEducationCommand>();
            CreateMap<GetEducationById, GetEducationByIdComand>();
            CreateMap<GetAllEducation, GetAllEducationsCommand>();
        }
    }
}

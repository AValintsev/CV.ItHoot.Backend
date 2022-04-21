using CVBuilder.Application.Education.Comands;
using CVBuilder.Application.Education.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Educatio;

namespace CVBuilder.Web.Mappers
{
    public class EducationMapper : MapperBase
    {
        public EducationMapper()
        {
            CreateMap<CreateEducation, CreateEducationComand>();
            CreateMap<GetEducationById, GetEducationByIdComand>();
            CreateMap<GetAllEducation, GetAllEducationsqComand>();
        }
    }
}

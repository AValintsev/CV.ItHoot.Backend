using CVBuilder.Application.Expiriance.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Experiance;

namespace CVBuilder.Web.Mappers
{
    public class ExperiancMapper : MapperBase
    {
        public ExperiancMapper()
        {
            CreateMap<CreateExperiance, CreateExperiencComand>();
            CreateMap<GetAllExperiances, GetAllExperiancesComand>();
            CreateMap<GetExperianceById, GetExperiancByIdComand>();
        }
    }
}

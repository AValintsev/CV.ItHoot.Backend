using CVBuilder.Application.Experience.Commands;
using CVBuilder.Application.Experience.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Experiance;

namespace CVBuilder.Web.Mappers
{
    public class ExperiancMapper : MapperBase
    {
        public ExperiancMapper()
        {
            CreateMap<CreateExperiance, CreateExperienceCommand>();
            CreateMap<GetAllExperiances, GetAllExperiencesQuery>();
            CreateMap<GetExperianceById, GetExperienceByIdQuery>();
        }
    }
}

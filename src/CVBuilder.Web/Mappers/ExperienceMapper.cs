using CVBuilder.Application.Experience.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Experiance;
using CreateExperienceCommand = CVBuilder.Application.Experience.Commands.CreateExperienceCommand;

namespace CVBuilder.Web.Mappers
{
    public class ExperienceMapper : MapperBase
    {
        public ExperienceMapper()
        {
            CreateMap<CreateExperiance, CreateExperienceCommand>();
            CreateMap<GetAllExperiances, GetAllExperiencesQuery>();
            CreateMap<GetExperianceById, GetExperienceByIdQuery>();
        }
    }
}

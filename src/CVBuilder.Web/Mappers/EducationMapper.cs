using CVBuilder.Application.Education.Commands;
using CVBuilder.Web.Contracts.V1.Requests.Education;
using CreateEducationCommand = CVBuilder.Application.Education.Commands.CreateEducationCommand;

namespace CVBuilder.Web.Mappers
{
    public class EducationMapper : MapperBase
    {
        public EducationMapper()
        {
            CreateMap<CreateEducation, CreateEducationCommand>();
            CreateMap<GetEducationById, GetEducationByIdCommand>();
            CreateMap<GetAllEducation, GetAllEducationsCommand>();
        }
    }
}

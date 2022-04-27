using CVBuilder.Application.Skill.Commands;
using CVBuilder.Application.Skill.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Skill;

namespace CVBuilder.Web.Mappers
{
    public class SkillMapper: MapperBase
    {
        public SkillMapper()
        {
            CreateMap<GetSkillByContainText, GetSkillByContainInTextQuery>();
            CreateMap<CreateSkillRequest, CreateSkillCommand>();
        }
    }
}

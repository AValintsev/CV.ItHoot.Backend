using CVBuilder.Application.Skill.Queries;
using CVBuilder.Web.Controllers.V1;

namespace CVBuilder.Web.Mappers
{
    public class SkillMapper: MapperBase
    {
        public SkillMapper()
        {
            CreateMap<GetSkillByContainText, GetSkillByContainInTextQuery>();
        }
    }
}

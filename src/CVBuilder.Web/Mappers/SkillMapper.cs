using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Application.Skill.Commands;
using CVBuilder.Application.Skill.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Skill;
using CVBuilder.Web.Contracts.V1.Responses.Skill;
using CreateSkillRequest = CVBuilder.Web.Contracts.V1.Requests.Skill.CreateSkillRequest;
using UpdateSkillRequest = CVBuilder.Web.Contracts.V1.Requests.Skill.UpdateSkillRequest;

namespace CVBuilder.Web.Mappers
{
    public class SkillMapper : MapperBase
    {
        public SkillMapper()
        {
            CreateMap<GetSkillByContainText, GetSkillByContainInTextQuery>();
            CreateMap<CreateSkillRequest, CreateSkillCommand>();
            CreateMap<UpdateSkillRequest, UpdateSkillCommand>();
            CreateMap<GetAllSkillRequest, GetAllSkillQuery>();
            CreateMap<SkillResult, SkillResponse>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.SkillName));
        }
    }
}
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using CVBuilder.Web.Contracts.V1.Responses.Skill;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CVBuilder.Application.Skill.Queries;

namespace CVBuilder.Web.Controllers.V1
{
    public class SkillController : BaseApiController
    {
        [HttpPost(ApiRoutes.SkillRoute.CreateSkill)]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }

        [HttpGet(ApiRoutes.SkillRoute.SkillsGetAll)]
        public async Task<IActionResult> GetSkills()
        {
       
            return Ok(new SkillsResponse(){
                Skills = new List<SkillResponse>()
                {
                    new SkillResponse()
                    {
                        Id = 1,
                        Name = ".NET-C#"
                    },
                    new SkillResponse()
                    {
                        Id = 2,
                        Name = "Java"
                    } 
                }
            });
        }

        [HttpGet(ApiRoutes.SkillRoute.GetSkill)]
        public async Task<IActionResult> GetSkill([FromQuery] GetSkillByContainText query)
        {
            var command = Mapper.Map<GetSkillByContainInTextQuery>(query);
            var result = await Mediator.Send(command);

            return Ok(result);
        }
    }

    public class GetSkillByContainText
    { 
        public string Content { get; set; }
    }
}

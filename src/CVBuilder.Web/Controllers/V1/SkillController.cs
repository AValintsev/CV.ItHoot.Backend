using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Skill.Commands;
using CVBuilder.Application.Skill.DTOs;
using CVBuilder.Application.Skill.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Skill;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1
{
    public class SkillController : BaseApiController
    {
        
        /// <summary>
        /// Create a new Skill
        /// </summary>
        [HttpPost(ApiRoutes.SkillRoute.CreateSkill)]
        public async Task<ActionResult<SkillDTO>> Create([FromBody] CreateSkillRequest request)
        {
            var command = Mapper.Map<CreateSkillCommand>(request);
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Get Skill by content Text
        /// </summary>
        [HttpGet(ApiRoutes.SkillRoute.GetSkill)]
        public async Task<ActionResult<IEnumerable<SkillDTO>>> GetSkill([FromQuery] GetSkillByContainText query)
        {
            var command = Mapper.Map<GetSkillByContainInTextQuery>(query);
            var result =  await Mediator.Send(command);

            return Ok(result);
        }
    }
}

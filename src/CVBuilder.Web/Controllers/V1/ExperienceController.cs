using System.Collections.Generic;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Experiance;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CVBuilder.Application.Experience.Commands;
using CVBuilder.Application.Experience.Queries;
using CVBuilder.Application.Experience.Responses;

namespace CVBuilder.Web.Controllers.V1
{
    public class ExperienceController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Experience.CreateExperience)]
        public async Task<ActionResult<CreateExperienceResult>> Create(CreateExperiance query)
        {
            var command = Mapper.Map<CreateExperienceCommand>(query);
            var response = await Mediator.Send(command);
            
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Experience.ExperienceGetAll)]
        public async Task<ActionResult<IEnumerable<CreateExperienceResult>>> GetAllExperiences([FromQuery]GetAllExperiances query)
        {
            var command = Mapper.Map<GetAllExperiencesQuery>(query);
            var response = await Mediator.Send(command);

            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Experience.GetExperience)]
        public async Task<ActionResult<GetExperienceByIdResult>> GetExperience([FromQuery] GetExperianceById query)
        {
            var command = Mapper.Map<GetExperienceByIdQuery>(query);
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}

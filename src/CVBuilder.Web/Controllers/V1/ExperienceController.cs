using CVBuilder.Application.Expiriance.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Experiance;
using CVBuilder.Web.Contracts.V1.Responses.Experience;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVBuilder.Web.Controllers.V1
{
    public class ExperienceController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Experience.CreateExperience)]
        public async Task<IActionResult> Create(CreateExperiance query)
        {
            var comand = Mapper.Map<CreateExperiencComand>(query);
            var respons = await Mediator.Send(comand);
            
            return Ok(respons);
        }
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Experience.ExperienceGetAll)]
        public async Task<IActionResult> GetAllExperiences([FromQuery]GetAllExperiances query)
        {
            var comand = Mapper.Map<GetAllExperiancesComand>(query);
            var response = await Mediator.Send(comand);

            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Experience.GetExperience)]
        public async Task<IActionResult> GetExperience([FromQuery] GetExperianceById query)
        {
            var comand = Mapper.Map<GetExperiancByIdComand>(query);
            var respons = await Mediator.Send(comand);
            return Ok(respons);
        }
    }
}

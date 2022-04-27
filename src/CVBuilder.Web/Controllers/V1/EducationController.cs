using CVBuilder.Application.Education.Comands;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Educatio;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CVBuilder.Application.Education.Commands;

namespace CVBuilder.Web.Controllers.V1
{
    public class EducationController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost(ApiRoutes.EducationRoute.CreateEducation)]
        public async Task<IActionResult> Create(CreateEducation query)
        {
            var command = Mapper.Map<CreateEducationCommand>(query);
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.EducationRoute.EducationGetAll)]
        public async Task<IActionResult> GetAllEducations([FromQuery] GetAllEducation query)
        {
            var command = Mapper.Map<GetAllEducationsCommand>(query);
            var result = await Mediator.Send(command);
            return Ok(result);
        
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.EducationRoute.GetEducation)]
        public async Task<IActionResult> GetEducations([FromQuery] GetEducationById query)
        {
            var command = Mapper.Map<GetEducationByIdComand>(query);
            var response = await Mediator.Send(command);
            
            return Ok(response);
        }
    }
}

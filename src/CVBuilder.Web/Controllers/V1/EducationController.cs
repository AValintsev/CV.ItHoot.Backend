//qusing CVBuilder.Web.Contracts.V1;
using CVBuilder.Application.Education.Comands;
using CVBuilder.Application.Education.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Educatio;
using CVBuilder.Web.Contracts.V1.Responses.Education;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVBuilder.Web.Controllers.V1
{
    public class EducationController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost(ApiRoutes.EducationRoute.CreateEducation)]
        public async Task<IActionResult> Create(CreateEducation queri)
        {
            var conamd = Mapper.Map<CreateEducationComand>(queri);
            var response = await Mediator.Send(conamd);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.EducationRoute.EducationGetAll)]
        public async Task<IActionResult> GetAllEducations([FromQuery] GetAllEducation queri)
        {
            var comand = Mapper.Map<GetAllEducationsqComand>(queri);
            var resault = await Mediator.Send(comand);
            return Ok(resault);
        
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.EducationRoute.GetEducation)]
        public async Task<IActionResult> GetEducations([FromQuery] GetEducationById queri)
        {
            var comand = Mapper.Map<GetEducationByIdComand>(queri);
            var respons = await Mediator.Send(comand);
            
            return Ok(respons);
        }
    }
}

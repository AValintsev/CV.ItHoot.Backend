using System.Collections.Generic;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Educatio;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CVBuilder.Application.Education.Commands;
using CVBuilder.Application.Education.Responses;
using EducationResult = CVBuilder.Application.CV.Responses.CvResponse.EducationResult;

namespace CVBuilder.Web.Controllers.V1
{
    public class EducationController : BaseApiController
    {
        
        /// <summary>
        /// Create a new Education
        /// </summary>
        [AllowAnonymous]
        [HttpPost(ApiRoutes.EducationRoute.CreateEducation)]
        public async Task<ActionResult<CreateEducationResult>> Create(CreateEducation query)
        {
            var command = Mapper.Map<CreateEducationCommand>(query);
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Get list of Educations
        /// </summary>
        [AllowAnonymous]
        [HttpGet(ApiRoutes.EducationRoute.GetAllEducation)]
        public async Task<ActionResult<IEnumerable<EducationResult>>> GetAllEducations([FromQuery] GetAllEducation query)
        {
            var command = Mapper.Map<GetAllEducationsCommand>(query);
            var result = await Mediator.Send(command);
            return Ok(result);
        
        }

        /// <summary>
        /// Get Education by ID
        /// </summary>
        [AllowAnonymous]
        [HttpGet(ApiRoutes.EducationRoute.GetEducation)]
        public async Task<ActionResult<EducationByIdResult>> GetEducations([FromRoute] GetEducationById query)
        {
            var command = Mapper.Map<GetEducationByIdCommand>(query);
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}

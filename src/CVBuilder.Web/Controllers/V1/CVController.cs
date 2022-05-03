using System.Threading.Tasks;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Queries;
using CVBuilder.Application.CV.Responses.CvResponse;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.CV;
using CVBuilder.Web.Contracts.V1.Responses.CV;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1
{
    public class CVController : BaseAuthApiController
    {
        /// <summary>
        /// Create a new CV
        /// </summary>
        [HttpPost(ApiRoutes.CV.CreateCv)]
        public async Task<ActionResult<CvResult>> Create(CreateCvRequest request)
        {
            var command = Mapper.Map<CreateCvCommand>(request);
            command.UserId = LoggedUserId;
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Get list of CV
        /// </summary>
        [HttpGet(ApiRoutes.CV.GetAllCv)]
        public async Task<ActionResult<GetAllCvCardResponse>> GetAllCvCard([FromQuery]GetAllCvCardRequest request)
        {
            var command = Mapper.Map<GetAllCvCardQueries>(request);
            command.UserId = LoggedUserId;
            command.UserRoles = LoggedUserRoles;
            var response = await Mediator.Send(command);
            var result = Mapper.Map<GetAllCvCardResponse>(response);
            return Ok(result);
        }
    
        /// <summary>
        /// Get CV by ID
        /// </summary>
        [HttpGet(ApiRoutes.CV.GetCvById)]
        public async Task<ActionResult<CvResult>> GetCvById(int  id)
        {
            var command = new GetCvByIdQueries
            {
                Id = id,
                UserId = LoggedUserId,
                UserRoles = LoggedUserRoles
            };
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Updates an existing CV
        /// </summary>
        [HttpPut(ApiRoutes.CV.UpdateCv)]
        public async Task<ActionResult<CvResult>> UpdateCv([FromBody] RequestCvUpdate request)
        {
            var command = Mapper.Map<UpdateCvCommand>(request);
            command.UserId = LoggedUserId;
            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}

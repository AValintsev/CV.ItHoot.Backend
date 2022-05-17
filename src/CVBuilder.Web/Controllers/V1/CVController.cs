using System.Collections.Generic;
using System.IO;
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
        /// Get a PDF file
        /// </summary>
        [HttpGet(ApiRoutes.CV.CvFile)]
        public async Task<ActionResult> CvFile(int id)
        {
            var command = new GetPdfByIdQueries
            {
                ResumeId = id
            };

            var result = await Mediator.Send(command);
            return Ok(result);
        }

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
        public async Task<ActionResult<IEnumerable<CvCardResponse>>> GetAllCvCard(
            [FromQuery] GetAllCvCardRequest request)
        {
            var command = Mapper.Map<GetAllCvCardQueries>(request);
            command.UserId = LoggedUserId;
            command.UserRoles = LoggedUserRoles;
            var response = await Mediator.Send(command);
            var result = Mapper.Map<List<CvCardResponse>>(response.CvCards);
            return Ok(result);
        }

        /// <summary>
        /// Get CV by ID
        /// </summary>
        [HttpGet(ApiRoutes.CV.GetCvById)]
        public async Task<ActionResult<CvResult>> GetCvById(int id)
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

        /// <summary>
        /// Deleting an existing CV
        /// </summary>
        [HttpDelete(ApiRoutes.CV.DeleteCv)]
        public async Task<ActionResult<CvResult>> DeleteCv(int id)
        {
            var command = new DeleteCvCommand
            {
                Id = id
            };
            var response = await Mediator.Send(command);
            return Ok();
        }
    }
}
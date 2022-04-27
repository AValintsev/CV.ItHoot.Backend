using System.Threading.Tasks;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.CV;
using CVBuilder.Web.Contracts.V1.Responses.CV;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1
{
    public class CVController : BaseApiController
    {
        [HttpPost(ApiRoutes.CV.CreateCV)]
        //[Authorize(Policy = Constants.Policy.User)]
        public async Task<IActionResult> Create(CreateCvRequest request)
        {
            
            var command = Mapper.Map<CreateCvCommand>(request);
            var res = await Mediator.Send(command);
            //var b = new 

            //command.UserId = LoggedUserId;

            //var response = await Mediator.Send(command);

            return Ok();
        }

        [HttpGet(ApiRoutes.CV.GetAllCvCards)]
        public async Task<IActionResult> GetAllCvCard([FromQuery] GetAllCvCardRequest request)
        {
            var command = Mapper.Map<GetAllCvCardQueries>(request); 
            // command.UserId = LoggedUserId;
            //command.UserRoles = LoggedUserRoles;

            var response = await Mediator.Send(command);
            var result = Mapper.Map<GetAllCvCardResponse>(response);

            return Ok(result);
        }

        [HttpGet(ApiRoutes.CV.GetCvById)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCvById([FromQuery] GetCvByIdRequest request)
        {
            var command = Mapper.Map<GetCvByIdQueries>(request);
            //command.UserId = LoggedUserId;

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpPost(ApiRoutes.CV.UpdateCv)]
        public async Task<IActionResult> UpdateCv([FromBody] RequestCvUpdate request)
        {
            var command = Mapper.Map<UpdateCvCommand>(request);
            //command.UserId = LoggedUserId;

            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}

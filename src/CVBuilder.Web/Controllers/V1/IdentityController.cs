﻿using System.Threading.Tasks;
using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Identity;
using CVBuilder.Web.Contracts.V1.Responses.Identity;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1
{
    public class IdentityController : BaseAuthApiController
    {
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Register)]
        [ProducesResponseType(typeof(AuthSuccessResponse),StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = Mapper.Map<RegisterCommand>(request);
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(Mapper.Map<AuthFailedResponse>(response));
            }

            return Ok(Mapper.Map<AuthSuccessResponse>(response));
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Login)]
        [ProducesResponseType(typeof(AuthSuccessResponse),StatusCodes.Status200OK)]
        public async Task<IActionResult> WebLogin([FromBody] WebLoginRequest request)
        {
            var command = Mapper.Map<WebLoginCommand>(request);
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(Mapper.Map<AuthFailedResponse>(response));
            }

            return Ok(Mapper.Map<AuthSuccessResponse>(response));
        }

        [HttpGet(ApiRoutes.Identity.GetCurrentUserByToken)]
        [ProducesResponseType(typeof(AuthSuccessResponse),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentUserByToken()
        {
            var command = Mapper.Map<GetCurrentUserByTokenCommand>(new GetCurrentUserByTokenRequest());
            command.UserId = LoggedUserId;

            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(Mapper.Map<AuthFailedResponse>(response));
            }

            return Ok(Mapper.Map<AuthSuccessResponse>(response));
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Refresh)]
        [ProducesResponseType(typeof(AuthSuccessResponse),StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var command = Mapper.Map<RefreshTokenCommand>(request);
            var response = await Mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(Mapper.Map<AuthFailedResponse>(response));
            }

            return Ok(Mapper.Map<AuthSuccessResponse>(response));
        }

        [HttpPost(ApiRoutes.Identity.Logout)]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var command = Mapper.Map<LogoutCommand>(request);
            command.UserId = LoggedUserId;
            var response = await Mediator.Send(command);
            if (!response)
            {
                return BadRequest();
            }

            return NoContent();
        }

        

        [HttpPost(ApiRoutes.Identity.GenerateToken)]
        public async Task<ActionResult<UserAccessTokenResult>> GenerateToken()
        {
            var command = new CreateUserAccessTokenCommand()
            {
                UserId = LoggedUserId
            };

            var response = await Mediator.Send(command);

            return Ok(response);
        }


    }
}
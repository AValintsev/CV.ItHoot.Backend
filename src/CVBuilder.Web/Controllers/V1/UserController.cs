using System.Threading.Tasks;
using CVBuilder.Application.User.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class UserController : BaseAuthApiController
{
    [HttpGet(ApiRoutes.User.ByRole)]
    public async Task<ActionResult> GetUserByRole([FromQuery] string roleName)
    {
        var command = new GetUsersByRoleQuery
        {
            RoleName = roleName
        };
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Services.Pagination;
using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Queries;
using CVBuilder.Application.User.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.User;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class UserController : BaseAuthApiController
{
    
    [HttpGet(ApiRoutes.User.GetAllUsers)]
    public async Task<ActionResult> GetAllUsers([FromQuery]GetAllUsersRequest request)
    {
        var validFilter = new GetAllUsersRequest(request.Page, request.PageSize, request.Term)
        {
            Sort = request.Sort,
            Order = request.Order,
        };

        var command = Mapper.Map<GetAllUsersQuery>(validFilter);
        var response = await Mediator.Send(command);
        
        var result = new PagedResponse<SmallUserResult>(response.Item2, validFilter.Page,
            validFilter.PageSize, response.Item1);
        
        return Ok(result);
    }

    [HttpPut(ApiRoutes.User.ChangeUserRole)]
    public async Task<ActionResult> ChangeUserRole(ChangeUserRoleRequest request)
    {
        var command = new ChangeUserRoleCommand
        {
            UserId = request.UserId,
            RoleId = request.RoleId
        };
        
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    
    [HttpGet(ApiRoutes.User.ByRole)]
    public async Task<ActionResult> GetUserByRole(string roleName)
    {
        var command = new GetUsersByRoleQuery
        {
            RoleName = roleName
        };
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
}
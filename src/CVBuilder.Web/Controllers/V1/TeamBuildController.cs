using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Application.TeamBuild.Commands;
using CVBuilder.Application.TeamBuild.Queries;
using CVBuilder.Application.TeamBuild.Result;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.TeamBuild;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class TeamBuildController:BaseAuthApiController
{
    /// <summary>
    /// Create a new TeamBuild
    /// </summary>
    [HttpPost(ApiRoutes.TeamBuild.CreateTeamBuild)]
    public async Task<ActionResult<TeamBuildResult>> CreateTeamBuild(CreateTeamBuildRequest request)
    {
        var command = Mapper.Map<CreateTeamBuildCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    /// <summary>
    /// Updates an existing TeamBuild
    /// </summary>
    [HttpPut(ApiRoutes.TeamBuild.UpdateTeamBuild)]
    public async Task<ActionResult<TeamBuildResult>> UpdateTeamBuild(UpdateTeamBuildRequest request)
    {
        var command = Mapper.Map<UpdateTeamBuildCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    /// <summary>
    /// Get list of TeamBuild
    /// </summary>
    [HttpGet(ApiRoutes.TeamBuild.GetAllTeamBuilds)]
    public async Task<ActionResult<List<TeamBuildResult>>> GetAllTeams()
    {
        var command = new GetAllTeamBuildsQuery();
        var result = await Mediator.Send(command);
        return result;
    }
}
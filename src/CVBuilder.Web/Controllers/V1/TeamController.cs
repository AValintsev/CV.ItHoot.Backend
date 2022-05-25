using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Team.Commands;
using CVBuilder.Application.Team.Queries;
using CVBuilder.Application.Team.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Team;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class TeamController : BaseAuthApiController
{
    /// <summary>
    /// Create a new Team
    /// </summary>
    [HttpPost(ApiRoutes.Team.CreateTeam)]
    public async Task<ActionResult<TeamResult>> CreateTeam(CreateTeamRequest request)
    {
        var command = Mapper.Map<CreateTeamCommand>(request);
        command.UserId = LoggedUserId;
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    /// <summary>
    /// Approve Team
    /// </summary>
    [HttpPost(ApiRoutes.Team.ApproveTeam)]
    public async Task<ActionResult<TeamResult>> ApproveTeam(ApproveTeamRequest request)
    {
        var command = Mapper.Map<ApproveTeamCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing Team
    /// </summary>
    [HttpPut(ApiRoutes.Team.UpdateTeam)]
    public async Task<ActionResult<TeamResult>> UpdateTeam(UpdateTeamRequest request)
    {
        var command = Mapper.Map<UpdateTeamCommand>(request);
        command.UserId = LoggedUserId;
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get list of Team
    /// </summary>
    [HttpGet(ApiRoutes.Team.GetAllTeams)]
    public async Task<ActionResult<List<SmallTeamResult>>> GetAllTeams()
    {
        var command = new GetAllTeamsQuery();
        var result = await Mediator.Send(command);
        return result;
    }
    
    /// <summary>
    /// Get list of archive Team
    /// </summary>
    [HttpGet(ApiRoutes.Team.GetAllArchiveTeams)]
    public async Task<ActionResult<List<SmallTeamResult>>> GetAllArchiveTeams()
    {
        var command = new GetAllArchiveTeamsQuery();
        var result = await Mediator.Send(command);
        return result;
    }

    
    /// <summary>
    /// Get team by ID
    /// </summary>
    [HttpGet(ApiRoutes.Team.GetTeamById)]
    public async Task<ActionResult<TeamResult>> GetTeamById([FromRoute]int id)
    {
        var command = new GetTeamByIdQuery()
        {
            Id = id
        };
        var result = await Mediator.Send(command);
        return result;
    }
}
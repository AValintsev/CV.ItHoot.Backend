using System.Collections.Generic;
using CVBuilder.Application.TeamBuild.Result;
using MediatR;

namespace CVBuilder.Application.TeamBuild.Commands;

public class CreateTeamBuildCommand:IRequest<TeamBuildResult>
{
    public string ProjectTypeName { get; set; }
    public string EstimationName { get; set; }
    public int ComplexityId { get; set; }
    public List<CreateTeamBuildPositionCommand> Positions { get; set; }
}
using System.Collections.Generic;
using CVBuilder.Application.TeamBuild.Result;
using MediatR;

namespace CVBuilder.Application.TeamBuild.Commands;

public class UpdateTeamBuildCommand:IRequest<TeamBuildResult>
{
    public int Id { get; set; }
    public string ProjectTypeName { get; set; }
    public int Estimation { get; set; }
    public int ComplexityId { get; set; }
    public List<UpdateTeamBuildPositionCommand> Positions { get; set; }
}
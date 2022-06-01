using System.Collections.Generic;

namespace CVBuilder.Application.TeamBuild.Result;

public class TeamBuildResult
{
    public int Id { get; set; }
    public string ProjectTypeName { get; set; }
    public string EstimationName { get; set; }
    public ComplexityResult Complexity { get; set; }
    public List<TeamBuildPositionResult> Positions { get; set; }
}
using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.TeamBuild;

public class CreateTeamBuildRequest
{
    public string ProjectTypeName { get; set; }
    public int ComplexityId { get; set; }
    public List<CreateTeamBuildPositionRequest> Positions { get; set; }
}
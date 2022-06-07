namespace CVBuilder.Web.Contracts.V1.Requests.TeamBuild;

public class CreateTeamBuildPositionRequest
{
    public int PositionId { get; set; }
    public int CountMembers { get; set; }
}
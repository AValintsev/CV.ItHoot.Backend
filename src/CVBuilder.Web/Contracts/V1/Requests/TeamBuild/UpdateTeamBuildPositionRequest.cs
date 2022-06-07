namespace CVBuilder.Web.Contracts.V1.Requests.TeamBuild;

public class UpdateTeamBuildPositionRequest
{
    public int Id { get; set; }
    public int PositionId { get; set; }
    public int CountMembers { get; set; }
}
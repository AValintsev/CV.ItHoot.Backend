namespace CVBuilder.Application.TeamBuild.Commands;

public class UpdateTeamBuildPositionCommand
{
    public int Id { get; set; }
    public int PositionId { get; set; }
    public int CountMembers { get; set; }
}
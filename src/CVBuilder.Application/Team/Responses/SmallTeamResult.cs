using CVBuilder.Models;

namespace CVBuilder.Application.Team.Responses;

public class SmallTeamResult
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public int CountResumes { get; set; }
    public string StatusTeam { get; set; }
}
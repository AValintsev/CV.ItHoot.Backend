using System;

namespace CVBuilder.Application.Team.Responses;

public class SmallTeamResult
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public string ClientUserName { get; set; }
    public int TeamSize { get; set; }
    public string LastUpdated { get; set; }
    public string CreatedUserName { get; set; }
    public string StatusTeam { get; set; }
}
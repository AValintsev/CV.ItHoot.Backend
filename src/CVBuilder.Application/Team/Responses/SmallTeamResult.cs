using System;
using CVBuilder.Models;

namespace CVBuilder.Application.Team.Responses;

public class SmallTeamResult
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public string ClientUserName { get; set; }
    public int TeamSize { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowContacts { get; set; }
    public string LastUpdated { get; set; }
    public string CreatedUserName { get; set; }
    public StatusTeam StatusTeam { get; set; }
}
using System.Collections.Generic;
using CVBuilder.Models;

namespace CVBuilder.Web.Contracts.V1.Requests.Team;

public class UpdateTeamRequest
{
    public int Id { get; set; }
    public int? ClientId { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowContacts { get; set; }
    public bool ShowCompanyNames { get; set; }
    public int ResumeTemplateId { get; set; }
    public StatusTeam StatusTeam { get; set; } 
    public string TeamName { get; set; }
    public List<UpdateResumeRequest> Resumes { get; set; }
}
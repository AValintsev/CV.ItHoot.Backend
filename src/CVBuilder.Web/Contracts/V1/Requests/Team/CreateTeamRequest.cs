using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.Team;

public class CreateTeamRequest
{
    public string TeamName { get; set; }
    public int ClientId { get; set; }
    public bool ShowLogo { get; set; }
    public int ResumeTemplateId { get; set; }
    public int? TeamBuildId { get; set; }
    public bool ShowContacts { get; set; }
    public List<CreateResumeRequest> Resumes { get; set; }
}
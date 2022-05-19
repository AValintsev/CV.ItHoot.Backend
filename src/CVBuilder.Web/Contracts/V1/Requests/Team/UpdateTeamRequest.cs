using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.Team;

public class UpdateTeamRequest
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public List<UpdateResumeRequest> Resumes { get; set; }
}
using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.Team;

public class ApproveTeamRequest
{
    public int TeamId { get; set; }
    public List<ApproveTeamResumeRequest> Resumes { get; set; }
}
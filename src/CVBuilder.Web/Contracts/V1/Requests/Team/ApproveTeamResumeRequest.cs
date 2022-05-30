namespace CVBuilder.Web.Contracts.V1.Requests.Team;

public class ApproveTeamResumeRequest
{
    public int Id { get; set; }
    public bool IsSelected { get; set; }
}
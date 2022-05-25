using CVBuilder.Models;

namespace CVBuilder.Application.Team.Responses;

public class ResumeResult
{
    public int Id { get; set; }
    public int ResumeId { get; set; }
    public StatusTeamResume StatusResume { get; set; }
    public string ResumeName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
using System.Collections.Generic;

namespace CVBuilder.Application.Team.Responses;

public class TeamResult
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public List<ResumeResult> Resumes { get; set; }
}
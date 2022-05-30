namespace CVBuilder.Application.Team.Responses;

public class TeamResumeResult
{
    public bool ShowLogo { get; set; }
    public int ResumeTemplateId { get; set; }
    public Resume.Responses.CvResponse.ResumeResult Resume { get; set; }
}
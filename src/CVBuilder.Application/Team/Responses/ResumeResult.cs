namespace CVBuilder.Application.Team.Responses;

public class ResumeResult
{
    public int Id { get; set; }
    public int ResumeId { get; set; }
    public bool IsSelected { get; set; }
    public string ResumeName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
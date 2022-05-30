namespace CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest;

public class CreateSkillRequest
{
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public int Level { get; set; }
}
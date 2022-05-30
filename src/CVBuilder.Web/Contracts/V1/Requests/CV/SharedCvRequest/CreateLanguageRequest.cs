namespace CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest;

public class CreateLanguageRequest
{
    public int LanguageId { get; set; }
    public string LanguageName { get; set; }
    public int Level { get; set; }
}
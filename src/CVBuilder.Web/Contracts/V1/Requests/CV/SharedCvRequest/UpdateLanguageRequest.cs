namespace CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest
{
    public class UpdateLanguageRequest
    {
        public int? Id { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int Level { get; set; }
    }
}

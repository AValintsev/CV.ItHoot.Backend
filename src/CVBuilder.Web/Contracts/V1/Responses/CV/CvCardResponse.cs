namespace CVBuilder.Web.Contracts.V1.Responses.CV
{
    public class CvCardResponse
    {
        public int Id { get; set; }
        public string CvName { get; set; }
        public bool IsDraft { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
    }
}

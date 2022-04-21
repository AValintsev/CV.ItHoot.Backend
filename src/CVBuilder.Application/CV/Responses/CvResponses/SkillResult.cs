namespace CVBuilder.Application.CV.Responses.CvResponses
{
    public class SkillResult
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int CvId { get; set; }
        public int SkillId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
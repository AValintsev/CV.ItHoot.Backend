using System.Collections.Generic;
using CVBuilder.Application.CV.Responses.CvResponse;

namespace CVBuilder.Application.CV.Responses
{
    using Models.Entities;
    public class CvCardResult
    {
        public int Id { get; set; }
        public string CvName { get; set; }
        public bool IsDraft { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public List<SkillResult> Skills { get; set; }
    }
}

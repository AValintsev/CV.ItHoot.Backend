using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses.CvResponse;

namespace CVBuilder.Application.Resume.Responses
{
    public class ResumeCardResult
    {
        public int Id { get; set; }
        public string ResumeName { get; set; }
        public bool IsDraft { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<SkillResult> Skills { get; set; }
    }
}

using System.Collections.Generic;

namespace CVBuilder.Application.Resume.Responses.CvResponse
{
    public class ResumeResult
    {
        public int Id { get; set; }
        public string ResumeName { get; set; }
        public bool IsDraft { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string RequiredPosition { get; set; }
        public string Birthdate { get; set; }
        public string Picture { get; set; }
        public string AboutMe { get; set; }
        public List<EducationResult> Educations { get; set; }
        public List<ExperienceResult> Experiences { get; set; }
        public List<LanguageResult> Languages { get; set; }
        public List<SkillResult> Skills { get; set; }
    }
}
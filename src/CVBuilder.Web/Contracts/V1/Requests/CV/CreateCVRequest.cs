using System.Collections.Generic;
using CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest;

namespace CVBuilder.Web.Contracts.V1.Requests.CV
{
    public class CreateCvRequest
    {
        public string CvName { get; set; }
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
        public string AboutMe { get; set; }

        public List<EducationRequest> Educations { get; set; }
        public List<ExperienceRequest> Experiences { get; set; }
        public List<SkillRequest> Skills { get; set; } = new();
        public List<UserLanguageRequest> UserLanguages { get; set; } = new();
    }


}

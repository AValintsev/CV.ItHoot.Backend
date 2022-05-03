using System;
using System.Collections.Generic;
using CVBuilder.Models;

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
        // public IFormFile Picture { get; set; }
        public string AboutMe { get; set; }

        public List<CVEducationRequest> Educations { get; set; }
        public List<CVExperienceRequest> Experiences { get; set; }
        public List<CVSkillRequest> Skills { get; set; } = new();
        public List<CVLanguageRequesst> UserLanguages { get; set; } = new();
    }

    public class CVSkillRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public SkillLevel Level { get; set; }
    }

    public class CVLanguageRequesst
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public LanguageLevel Level { get; set; }
    }

    public class CVExperienceRequest
    {
        public string Company { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class CVEducationRequest
    {
        public string InstitutionName { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }


}

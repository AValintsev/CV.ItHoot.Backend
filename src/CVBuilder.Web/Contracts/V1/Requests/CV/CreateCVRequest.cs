using System.Collections.Generic;
using CVBuilder.Models.Entities;
using Microsoft.AspNetCore.Http;

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

        public List<Education> Educations { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<CVSkill> Skills { get; set; } = new();
        public List<CVLanguage> UserLanguages { get; set; } = new();
    }

    public class CVSkill
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }

    public class CVLanguage
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }


}

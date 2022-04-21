using CVBuilder.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBuilder.Web.Contracts.V1.Responses.CV
{
    public class CvResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CvName { get; set; }
        public bool IsDraft { get; set; }
        public string UserName { get; set; }
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
        public List<Models.Entities.Education> Educations { get; set; }
        public List<Models.Entities.Experience> Experiences { get; set; }
        public List<UserLanguage> UserLanguages { get; set; }
        public List<Models.Entities.Skill> Skills { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBuilder.Web.Contracts.V1.Requests.CV
{
    public class RequestCvUpdate
    {
        public int Id { get; set; }
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
        //public IFormFile Picture { get; set; }
        public string AboutMe { get; set; }
        public List<RequestEducation> Educations { get; set; }
        public List<RequestExperience> Experiences { get; set; }
        public List<RequestSkill> Skills { get; set; }
        public List<RequestUserLanguage> UserLanguages { get; set; }

        public List<RequestSkill> RSkills { get; set; }
        public List<RequestUserLanguage> RUserLanguages { get; set; }
        public List<RequestEducation> REducations { get; set; }
        public List<RequestExperience> RExperiences { get; set; }


    }
}

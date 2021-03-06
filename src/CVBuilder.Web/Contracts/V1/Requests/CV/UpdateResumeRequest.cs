using System.Collections.Generic;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Web.Contracts.V1.Requests.CV.SharedCvRequest;
using CVBuilder.Web.Contracts.V1.Requests.Position;

namespace CVBuilder.Web.Contracts.V1.Requests.CV
{
    public class UpdateResumeRequest
    {
        public int Id { get; set; }
        public string ResumeName { get; set; }
        public bool IsDraft { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PositionRequest Position { get; set; }
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
        public List<UpdateEducationRequest> Educations { get; set; }
        public List<UpdateExperienceRequest> Experiences { get; set; }
        public List<UpdateSkillRequest> Skills { get; set; }
        public List<UpdateLanguageRequest> Languages { get; set; }

    }
}

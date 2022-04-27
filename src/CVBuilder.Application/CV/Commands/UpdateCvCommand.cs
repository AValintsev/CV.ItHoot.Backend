using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using CVBuilder.Application.CV.Commands.SharedCommands;
using CVBuilder.Application.CV.Responses.CvResponse;

namespace CVBuilder.Application.CV.Commands
{
    public class UpdateCvCommand : IRequest<UpdateCvResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
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
        public IFormFile Picture { get; set; }
        public string AboutMe { get; set; }
        public List<EducationCommand> Educations { get; set; }
        public List<ExperienceCommand> Experiences { get; set; }
        public List<SkillCommand> Skills { get; set; }
        public List<UserLanguageCommand> UserLanguages { get; set; }

        //todod 
        public List<SkillCommand> RSkills { get; set; }
        public List<EducationCommand> REducations { get; set; }
        public List<ExperienceCommand> RExperiences { get; set; }
        public List<UserLanguageCommand> RUserLanguages { get; set; }
    }
}
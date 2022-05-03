using MediatR;
using System.Collections.Generic;
using CVBuilder.Application.CV.Commands.SharedCommands;
using CVBuilder.Application.CV.Responses.CvResponse;
using CVBuilder.Models;


namespace CVBuilder.Application.CV.Commands
{
    public class CreateCvCommand : IRequest<CvResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
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
        public List<CreateFileCommand> Picture { get; set; } = new();
        public List<EducationCommand> Educations { get; set; } = new();
        public List<ExperienceCommand> Experiences { get; set; } = new();
        public List<CVSkill> Skills { get; set; } = new();
        public List<CVLanguage> UserLanguages { get; set; } = new();
    }
    public class CVSkill
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public SkillLevel Level { get; set; }
    }

    public class CVLanguage
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public LanguageLevel Level { get; set; }
    }
}

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
        public List<CreateFileComand> Picture { get; set; } = new List<CreateFileComand>();
        public List<EducationCommand> Educations { get; set; } = new List<EducationCommand>();
        public List<ExperienceCommand> Experiences { get; set; } = new List<ExperienceCommand>();
        //public List<SkillCommand> Skills { get; set; } = new List<SkillCommand>();
        //public List<UserLanguageCommand> UserLanguages { get; set; } = new List<UserLanguageCommand>();

        public List<CVSkill> Skills { get; set; } = new List<CVSkill>();
        public List<CVLanguage> UserLanguages { get; set; } = new List<CVLanguage>();
    }
    public class CVSkill
    {
        public int? Id { get; set; }
        public int CvId { get; set; }
        public int Order { get; set; }
        public int? SkillId { get; set; }
        public string Name { get; set; }
        public SkillLevel Level { get; set; }
    }

    public class CVLanguage
    {
        public int? Id { get; set; }
        public int Order { get; set; }
        public int CvId { get; set; }
        public int? LanguageId { get; set; }
        public string Name { get; set; }
        public LanguageLevel Level { get; set; }
    }
}

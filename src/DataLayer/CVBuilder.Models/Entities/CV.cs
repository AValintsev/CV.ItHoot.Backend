using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities
{
    public class Cv : IDeletableEntity<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDraft { get; set; }
        
        public string CvName { get; set; }
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
        public List<File> Files { get; set; }
        public List<Education> Educations { get; set; }
        public List<Experience> Experiences { get; set; }

        public List<LevelLanguage> LevelLanguages { get; set; } = new List<LevelLanguage>();
        public List<LevelSkill> LevelSkills { get; set; } = new List<LevelSkill>();
    }
}

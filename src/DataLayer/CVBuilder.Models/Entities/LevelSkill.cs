using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities
{
    public class LevelSkill: IDeletableEntity<int>, IOrderlyEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int Order { get; set; }


        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }
}

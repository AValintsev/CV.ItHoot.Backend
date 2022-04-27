using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities
{
    public class Skill : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LevelSkill> LevelSkills { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

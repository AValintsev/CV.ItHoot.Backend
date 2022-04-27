using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities
{
    public class UserLanguage : IEntity<int>, IOrderlyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LevelLanguage> LevelLanguages { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Order { get; set; }
    }
}

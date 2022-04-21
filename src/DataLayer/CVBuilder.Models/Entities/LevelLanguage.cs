using System;
using System.Collections.Generic;
using System.Text;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities
{
    public class LevelLanguage : IDeletableEntity<int>, IOrderlyEntity 
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Cv Cv { get; set; }
        public int CvId { get; set; }
        public UserLanguage UserLanguage { get; set; }
        public  int UserLanguageId { get; set; }
        public  LanguageLevel LanguageLevel { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int Order { get; set; }
    }
}

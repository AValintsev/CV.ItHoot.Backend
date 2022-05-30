using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class LevelLanguage : IDeletableEntity<int>, IOrderlyEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public LanguageLevel LanguageLevel { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int Order { get; set; }
}
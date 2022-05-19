using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class TeamResume:IDeletableEntity<int>
{
    public int Id { get; set; }
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public bool IsSelected { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
}
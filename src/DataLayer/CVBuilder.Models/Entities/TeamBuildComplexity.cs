using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class TeamBuildComplexity:IEntity<int>
{
    public int Id { get; set; }
    public string ComplexityName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
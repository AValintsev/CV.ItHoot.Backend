using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class TeamBuild:IEntity<int>
{
    public int Id { get; set; }
    public string ProjectTypeName { get; set; }
    public int Estimation { get; set; }
    public int? ComplexityId { get; set; }
    public TeamBuildComplexity Complexity { get; set; }
    public List<TeamBuildPosition> Positions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public List<Team> Teams { get; set; }
}
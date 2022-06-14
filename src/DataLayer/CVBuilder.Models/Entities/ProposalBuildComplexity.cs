using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ProposalBuildComplexity:IEntity<int>
{
    public int Id { get; set; }
    public string ComplexityName { get; set; }
    public List<ProposalBuild>  ProposalBuilds { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
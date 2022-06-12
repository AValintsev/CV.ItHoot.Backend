using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ProposalBuild:IEntity<int>
{
    public int Id { get; set; }
    public string ProjectTypeName { get; set; }
    public int Estimation { get; set; }
    public int? ComplexityId { get; set; }
    public ProposalBuildComplexity Complexity { get; set; }
    public List<ProposalBuildPosition> Positions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public List<Proposal> Proposals { get; set; }
}
using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

using Models.Entities;

public class ProposalBuildPosition : IEntity<int>
{
    public int Id { get; set; }
    public int PositionId { get; set; }
    public int ProposalBuildId { get; set; }
    public ProposalBuild ProposalBuild { get; set; }
    public Position Position { get; set; }
    public int CountMembers { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
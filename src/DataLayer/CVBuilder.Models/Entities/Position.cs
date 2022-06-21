using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Position : IDeletableEntity<int>
{
    public int Id { get; set; }
    public string PositionName { get; set; }
    public List<Resume> Resumes { get; set; }
    public List<ProposalBuildPosition> ProposalBuildPositions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
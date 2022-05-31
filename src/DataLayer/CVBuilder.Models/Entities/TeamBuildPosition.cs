using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;
using Models.Entities;
public class TeamBuildPosition:IEntity<int>
{
    public int Id { get; set; }
    public int PositionId { get; set; }
    public int TeamBuildId { get; set; }
    public TeamBuild TeamBuild { get; set; }
    public Position Position { get; set; }
    public int CountMembers { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
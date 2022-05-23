using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Team: IDeletableEntity<int>
{
    public int Id { get; set; }
    public int? CreatedUserId { get; set; }
    public User CreatedUser { get; set; }
    
    public int? ClientId { get; set; }
    public User Client { get; set; }
    public string TeamName { get; set; }
    public List<TeamResume> Resumes { get; set; }
    public StatusTeam StatusTeam { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
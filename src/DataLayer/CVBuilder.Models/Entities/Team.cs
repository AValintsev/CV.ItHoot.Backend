using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Team: IDeletableEntity<int>
{
    public int Id { get; set; }
    public int? CreatedUserId { get; set; }
    public User CreatedUser { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowContacts { get; set; }
    public int? ClientId { get; set; }
    public User Client { get; set; }
    public string TeamName { get; set; }
    
    public int ResumeTemplateId { get; set; }
    public ResumeTemplate ResumeTemplate { get; set; }
    
    public int? TeamBuildId { get; set; }
    public TeamBuild TeamBuild { get; set; }
    public List<TeamResume> Resumes { get; set; }
    public StatusTeam StatusTeam { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
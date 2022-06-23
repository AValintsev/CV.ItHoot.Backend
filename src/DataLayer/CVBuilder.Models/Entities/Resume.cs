using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Resume : IDeletableEntity<int>
{
    public int Id { get; set; }
    public int? CreatedUserId { get; set; }
    public User CreatedUser { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDraft { get; set; }

    public int? ImageId { get; set; }
    public Image Image { get; set; }

    public int? PositionId { get; set; }

    public int? ResumeTemplateId { get; set; }
    public ResumeTemplate ResumeTemplate { get; set; }
    public Position Position { get; set; }
    public string ResumeName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string RequiredPosition { get; set; }
    public string Birthdate { get; set; }
    public string AboutMe { get; set; }
    public List<Education> Educations { get; set; }
    public List<Experience> Experiences { get; set; }
    public List<LevelLanguage> LevelLanguages { get; set; }
    public List<LevelSkill> LevelSkills { get; set; }
    public decimal SalaryRate { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public int? CountDaysUnavailable { get; set; }
}
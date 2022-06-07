﻿using System.Collections.Generic;
using CVBuilder.Models;
using SkillResult = CVBuilder.Application.Skill.DTOs.SkillResult;

namespace CVBuilder.Application.Team.Responses;

public class ResumeResult
{
    public int Id { get; set; }
    public int ResumeId { get; set; }
    public StatusTeamResume StatusResume { get; set; }
    public int PositionId { get; set; }
    public string PositionName { get; set; }
    public string ResumeName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Picture { get; set; }
    public string ShortUrl { get; set; }
    public List<SkillResult> Skills { get; set; }
}
﻿using System;
using System.Collections.Generic;
using CVBuilder.Models;
using CVBuilder.Web.Contracts.V1.Responses.Skill;

namespace CVBuilder.Web.Contracts.V1.Responses.CV
{
    public class ResumeCardResponse
    {
        public int Id { get; set; }
        public string ResumeName { get; set; }
        public bool IsDraft { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PositionName { get; set; }
        public decimal SalaryRate { get; set; }
        public string AboutMe { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<SkillResponse> Skills { get; set; } 
    }
}
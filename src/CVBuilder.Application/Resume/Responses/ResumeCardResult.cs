using System;
using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Models;

namespace CVBuilder.Application.Resume.Responses
{
    public class ResumeCardResult
    {
        public int Id { get; set; }
        public string ResumeName { get; set; }
        public string PositionName { get; set; }
        public bool IsDraft { get; set; }
        public decimal SalaryRate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<SkillResult> Skills { get; set; }
    }
}
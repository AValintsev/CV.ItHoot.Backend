﻿using System;
using System.Collections.Generic;

namespace CVBuilder.Application.CV.Responses.CvResponses
{
    public class CvResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CvName { get; set; }
        public bool IsDraft { get; set; }
        public string UserName { get; set; }
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
        public string Picture { get; set; }
        public string AboutMe { get; set; }
        public List<EducationResult> Educations { get; set; }
        public List<ExpirianceResult> Experiences { get; set; }
        public List<UserLanguageResult> UserLanguages { get; set; }
        public List<SkillResult> Skills { get; set; }
    }
}
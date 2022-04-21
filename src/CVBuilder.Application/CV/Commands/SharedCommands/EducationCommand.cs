﻿using System;

namespace CVBuilder.Application.CV.Commands.sharedCommands
{
    public class EducationCommand
    {
        public int Id { get; set; }
        public int CvId { get; set; }
        public string InstitutionName { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

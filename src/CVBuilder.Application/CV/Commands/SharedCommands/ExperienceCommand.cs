﻿using System;

namespace CVBuilder.Application.CV.Commands.sharedCommands
{
     public class ExperienceCommand
    {
        public int Id { get; set; }
        public int CvId { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

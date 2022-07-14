using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ResumeTemplate : IEntity<int>
{
    public int Id { get; set; }
    public string TemplateName { get; set; }
    public string Html { get; set; }
    public byte[] Docx { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
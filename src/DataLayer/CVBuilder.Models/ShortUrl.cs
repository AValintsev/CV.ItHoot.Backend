using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models;

public class ShortUrl: IEntity<int>
{
    public int Id { get; set; }
    public string Url { get; set; }
    
    public List<User> Users { get; set; }
    public List<TeamResume> TeamResumes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
﻿using System.Collections.Generic;
using CVBuilder.Models;

namespace CVBuilder.Application.Team.Responses;

public class TeamResult
{
    public int Id { get; set; }
    public TeamClientResult Client { get; set; }
    public StatusTeam StatusTeam { get; set; }
    public string TeamName { get; set; }
    public List<ResumeResult> Resumes { get; set; }
}
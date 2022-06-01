﻿using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.TeamBuild;

public class UpdateTeamBuildRequest
{
    public int Id { get; set; }
    public string ProjectTypeName { get; set; }
    public string EstimationName { get; set; }
    public int ComplexityId { get; set; }
    public List<UpdateTeamBuildPositionRequest> Positions { get; set; }
}
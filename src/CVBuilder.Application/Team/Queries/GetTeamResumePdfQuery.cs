﻿using System.Collections.Generic;
using System.IO;
using MediatR;

namespace CVBuilder.Application.Team.Queries;

public class GetTeamResumePdfQuery:IRequest<Stream>
{
    public int TeamId { get; set; }
    public int TeamResumeId { get; set; }
    public string JwtToken { get; set; }
}
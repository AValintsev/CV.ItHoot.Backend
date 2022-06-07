using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumesByTeamBuildQuery:IRequest<List<ResumeCardResult>>
{
    public int TeamBuildId { get; set; }
}
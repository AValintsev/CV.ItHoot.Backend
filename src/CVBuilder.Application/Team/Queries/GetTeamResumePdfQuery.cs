using System.Collections.Generic;
using System.IO;
using MediatR;

namespace CVBuilder.Application.Team.Queries;

public class GetTeamResumePdfQuery:IRequest<Stream>
{
    public List<string> UserRoles { get; set; }
    public int TeamId { get; set; }
    public int ResumeId { get; set; }
    public string JwtToken { get; set; }
}
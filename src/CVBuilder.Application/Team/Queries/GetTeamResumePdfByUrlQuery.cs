using System.Collections.Generic;
using System.IO;
using MediatR;

namespace CVBuilder.Application.Team.Queries;

public class GetTeamResumePdfByUrlQuery:IRequest<Stream>
{
    public List<string> UserRoles { get; set; }
    public string JwtToken { get; set; }
    public string ShortUrl { get; set; }
}
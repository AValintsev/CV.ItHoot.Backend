using System.Collections.Generic;
using System.IO;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumePdfByUrlQuery : IRequest<Stream>
{
    public List<string> UserRoles { get; set; }
    public string JwtToken { get; set; }
    public string ShortUrl { get; set; }
}
using System.Collections.Generic;
using System.IO;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumeDocxByUrlQuery : IRequest<Stream>
{
    public int? UserId { get; set; }
    public List<string> UserRoles { get; set; }
    public string ShortUrl { get; set; }

}
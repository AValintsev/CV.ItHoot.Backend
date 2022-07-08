using System.Collections.Generic;
using System.IO;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumePdfQuery : IRequest<Stream>
{
    public int ProposalId { get; set; }
    public int ProposalResumeId { get; set; }
    public List<string> UserRoles { get; set; }
    public int? UserId { get; set; }
    
    public string JwtToken { get; set; }
}
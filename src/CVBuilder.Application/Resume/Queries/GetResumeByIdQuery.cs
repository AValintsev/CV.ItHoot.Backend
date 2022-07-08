using System.Collections.Generic;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Responses.CvResponse;
using MediatR;

namespace CVBuilder.Application.Resume.Queries
{
    public class GetResumeByIdQuery : IRequest<ResumeResult>
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
        public PrintFooter PrintFooter { get; set; }
    }
}
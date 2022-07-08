using System.Collections.Generic;
using System.IO;
using MediatR;

namespace CVBuilder.Application.Resume.Queries
{
    public class GetPdfByIdQueries : IRequest<Stream>
    {
        public int? UserId { get; set; }
        public List<string> UserRoles { get; set; }
        public int ResumeId { get; set; }
        public string JwtToken { get; set; }
    }
}
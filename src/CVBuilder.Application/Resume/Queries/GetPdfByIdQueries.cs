using System.IO;
using MediatR;

namespace CVBuilder.Application.Resume.Queries
{
    public class GetPdfByIdQueries : IRequest<Stream>
    {
        public int ResumeId { get; set; }
        public string JwtToken { get; set; }
    }
}
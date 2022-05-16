using System.IO;
using MediatR;

namespace CVBuilder.Application.CV.Queries
{
    public class GetPdfByIdQueries:IRequest<Stream>
    {
        public int ResumeId { get; set; }
    }
}
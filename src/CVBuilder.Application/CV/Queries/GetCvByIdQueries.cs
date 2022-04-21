using System.Collections.Generic;
using CVBuilder.Application.CV.Responses.CvResponses;
using MediatR;

namespace CVBuilder.Application.CV.Queries
{
    public class GetCvByIdQueries : IRequest<CvResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}

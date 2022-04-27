using CVBuilder.Application.CV.Responses;
using MediatR;
using System.Collections.Generic;

namespace CVBuilder.Application.CV.Queries
{
    public class GetAllCvCardQueries : IRequest<GetAllCvCardResult>
    {
        public int UserId { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}

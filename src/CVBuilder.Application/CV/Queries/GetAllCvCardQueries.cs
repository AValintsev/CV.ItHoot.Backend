using CVBuilder.Application.CV.Responses;
using MediatR;
using System.Collections.Generic;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.CV.Queries
{
    public class GetAllCvCardQueries : IRequest<GetAllCvCardResult>
    {
        public int UserId { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}

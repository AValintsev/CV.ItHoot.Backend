using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Queries
{
    public class GetAllResumeCardQueries : IRequest<List<ResumeCardResult>>
    {
        public int UserId { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}

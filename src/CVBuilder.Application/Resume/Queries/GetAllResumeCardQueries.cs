using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Queries
{
    public class GetAllResumeCardQueries : IRequest<(int, List<ResumeCardResult>)>
    {
        public int UserId { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
        public string Term { get; set; }
        public List<int> Positions { get; set; }
        public List<int> Skills { get; set; }
        public bool IsArchive { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
    }
}
using System.Collections.Generic;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumeHtmlByIdQuery : IRequest<string>
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public IEnumerable<string> UserRoles { get; set; }
}
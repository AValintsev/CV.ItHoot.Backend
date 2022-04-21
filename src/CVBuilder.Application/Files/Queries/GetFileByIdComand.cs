using CVBuilder.Application.Files.Responses;
using MediatR;

namespace CVBuilder.Application.Files.Queries
{
    public class GetFileByIdComand : IRequest<FileResult>
    {
        public int Id { get; set; }
    }
}

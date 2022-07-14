using System.IO;
using MediatR;

namespace CVBuilder.Application.Resume.Queries
{
    public class GetDocxByIdQueries : IRequest<Stream>
    {
        public int Id { get; private set; }

        public GetDocxByIdQueries(int id)
        {
            Id = id;
        }
    }
}
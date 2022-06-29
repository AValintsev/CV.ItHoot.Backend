using CVBuilder.Application.Client.Responses;
using MediatR;

namespace CVBuilder.Application.Client.Queries
{
    public class GetClientByIdQuery : IRequest<ClientResponse>
    {
        public int Id { get; private set; }
        public GetClientByIdQuery(int id)
        {
            Id = id;
        }
    }
}

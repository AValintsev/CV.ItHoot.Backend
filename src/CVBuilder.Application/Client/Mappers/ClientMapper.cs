using CVBuilder.Application.Client.Responses;

namespace CVBuilder.Application.Client.Mappers
{
    internal class ClientMapper : AppMapperBase
    {
        public ClientMapper()
        {
            CreateMap<Models.User, ClientListItemResponse>();
        }
    }
}
using System;
using System.Linq;
using CVBuilder.Application.Client.Responses;

namespace CVBuilder.Application.Client.Mappers
{
    internal class ClientMapper : AppMapperBase
    {
        public ClientMapper()
        {
            CreateMap<Models.User, ClientListItemResponse>()
                .ForMember(
                    x => x.Proposals, 
                    y => y.MapFrom(z => String.Join(", ", z.ClientProposals.Select(p => p.ProposalName).ToList())));
        }
    }
}
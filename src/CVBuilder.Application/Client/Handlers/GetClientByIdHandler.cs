using AutoMapper;
using CVBuilder.Application.Client.Queries;
using CVBuilder.Application.Client.Responses;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.Client.Handlers
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, ClientResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Models.User, int> _userRepository;
        public GetClientByIdHandler(IMapper mapper, IRepository<Models.User, int> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }


        public async Task<ClientResponse> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var query = _userRepository.Table;

            query = query.Where(u => u.Id == request.Id);

            var resultClient = await query.FirstOrDefaultAsync();

            if (resultClient == null)
            {
                throw new NotFoundException("No client in db");
            }

            var result = _mapper.Map<ClientResponse>(resultClient);
            return result;
        }
    }
}
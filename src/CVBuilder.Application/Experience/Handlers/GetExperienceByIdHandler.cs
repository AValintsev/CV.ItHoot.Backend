using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Experience.Queries;
using CVBuilder.Application.Experience.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Experience.Handlers
{
    internal class GetExperienceByIdHandler : IRequestHandler<GetExperienceByIdQuery, GetExperienceByIdResult>
    {
        private readonly IRepository<Models.Entities.Experience,int> _repository;
        private readonly IMapper _mapper;
        public GetExperienceByIdHandler(IMapper mapper, IRepository<Models.Entities.Experience, int> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<GetExperienceByIdResult> Handle(GetExperienceByIdQuery request, CancellationToken cancellationToken)
        {
            var result =  await _repository.GetByIdAsync(request.Id);
            
            if (result == null)
            {
                throw new NotFoundException("Experience not found");
            }
            
            var response =  _mapper.Map<GetExperienceByIdResult>(result);

            return response;
        }
    }
}

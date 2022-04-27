using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Expiriance.Queries;
using CVBuilder.Application.Expiriance.Respons;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
namespace CVBuilder.Application.Expiriance.Heandlers
{
    internal class CreateExperienceHEandler : IRequestHandler<CreateExperiencComand, CreateExpirienceResult>
    {

        private IRepository<Experience, int> _repository;
        private IMapper _mapper; 

        public CreateExperienceHEandler(IRepository<Experience,int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<CreateExpirienceResult> Handle(CreateExperiencComand request, CancellationToken cancellationToken)
        {
            var expirience = _mapper.Map<Experience>(request);
            await _repository.CreateAsync(expirience);
            var result = _mapper.Map<CreateExpirienceResult>(expirience);
          
            return result;
        }
    }
}

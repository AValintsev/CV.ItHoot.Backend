using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Repository;
using MediatR;
using AutoMapper;
using CVBuilder.Application.Education.Commands;
using CVBuilder.Application.Education.Response;

namespace CVBuilder.Application.Education.Handlers
{
    internal class CreateEducationHeandler : IRequestHandler<CreateEducationCommand, CreateEducationResult>
    {
        private IRepository<CVBuilder.Models.Entities.Education, int> _repository;
        private IMapper _mapper;
        public CreateEducationHeandler(IRepository<CVBuilder.Models.Entities.Education, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CreateEducationResult> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            var newEducation = _mapper.Map<CVBuilder.Models.Entities.Education>(request);
            await _repository.CreateAsync(newEducation);
            var result = _mapper.Map<CreateEducationResult>(newEducation);

            return result;
        }
    }
}

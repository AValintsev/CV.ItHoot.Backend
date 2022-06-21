using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Repository;
using MediatR;
using AutoMapper;
using CVBuilder.Application.Education.Commands;
using CVBuilder.Application.Education.Responses;

namespace CVBuilder.Application.Education.Handlers
{
    using Models.Entities;

    internal class CreateEducationHandler : IRequestHandler<CreateEducationCommand, CreateEducationResult>
    {
        private readonly IRepository<Education, int> _repository;
        private readonly IMapper _mapper;

        public CreateEducationHandler(IRepository<Education, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateEducationResult> Handle(CreateEducationCommand request,
            CancellationToken cancellationToken)
        {
            var newEducation = _mapper.Map<Education>(request);
            await _repository.CreateAsync(newEducation);
            var result = _mapper.Map<CreateEducationResult>(newEducation);

            return result;
        }
    }
}
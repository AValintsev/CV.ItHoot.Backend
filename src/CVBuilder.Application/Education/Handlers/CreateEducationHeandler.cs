using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Education.Queries;
using CVBuilder.Repository;
using MediatR;
using CVBuilder.Models.Entities;
using AutoMapper;
using CVBuilder.Application.Education.Response;

namespace CVBuilder.Application.Education.Handlers
{
    internal class CreateEducationHeandler : IRequestHandler<CreateEducationComand, CreateEducationResult>
    {
        private IRepository<CVBuilder.Models.Entities.Education, int> _repository;
        private IMapper _mapper;
        public CreateEducationHeandler(IRepository<CVBuilder.Models.Entities.Education, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CreateEducationResult> Handle(CreateEducationComand request, CancellationToken cancellationToken)
        {
            var newEducation = _mapper.Map<CVBuilder.Models.Entities.Education>(request);
            await _repository.CreateAsync(newEducation);
            var result = _mapper.Map<CreateEducationResult>(newEducation);

            return result;
        }
    }
}

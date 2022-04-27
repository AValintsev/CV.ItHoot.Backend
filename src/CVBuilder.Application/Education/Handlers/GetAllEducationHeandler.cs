using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.CV.Responses.CvResponses;
using CVBuilder.Application.Education.Commands;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Education.Handlers
{
    internal class GetAllEducationHeandler : IRequestHandler<GetAllEducationsCommand, List<EducationResult>>
    {
        private IRepository<Models.Entities.Education,int> _repository;
        private IMapper _mapper;
        public GetAllEducationHeandler(IRepository<Models.Entities.Education, int> repository, IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<List<EducationResult>> Handle(GetAllEducationsCommand request, CancellationToken cancellationToken)
        {
            var educations = await _repository.GetListAsync();
            var result = _mapper.Map<List<EducationResult>>(educations);

            return result;

        }
    }
}

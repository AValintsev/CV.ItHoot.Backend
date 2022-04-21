using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.CV.Responses.CvResponses;
using CVBuilder.Application.Education.Queries;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Education.Handlers
{
    internal class GetAllEducationHeandler : IRequestHandler<GetAllEducationsqComand, List<EducationResult>>
    {
        private IRepository<Models.Entities.Education,int> _repository;
        private IMapper _mapper;
        public GetAllEducationHeandler(IRepository<Models.Entities.Education, int> repository, IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<List<EducationResult>> Handle(GetAllEducationsqComand request, CancellationToken cancellationToken)
        {
            var educations = await _repository.GetListAsync();
            var result = _mapper.Map<List<EducationResult>>(educations);

            return result;

        }
    }
}

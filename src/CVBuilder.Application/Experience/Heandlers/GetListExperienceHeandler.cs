using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Expiriance.Queries;
using CVBuilder.Application.Expiriance.Respons;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Expiriance.Heandlers
{
    public class GetListExperienceHeandler : IRequestHandler<GetAllExperiancesComand, List<ExperianceResult>>
    {
        private IRepository<Experience, int> _repository;
        private IMapper _mapper;
        public GetListExperienceHeandler(IRepository<Experience,int> repository, IMapper mapper )
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<ExperianceResult>> Handle(GetAllExperiancesComand request, CancellationToken cancellationToken)
        {
            var expiriance =  await _repository.Table.ToListAsync();

            var result = _mapper.Map<List<ExperianceResult>>(expiriance);
            
            return result;
        }
    }


}

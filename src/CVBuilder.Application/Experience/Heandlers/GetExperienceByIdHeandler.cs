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

namespace CVBuilder.Application.Expiriance.Heandlers
{
    internal class GetExperienceByIdHeandler : IRequestHandler<GetExperiancByIdComand, GetExpirianceByIdResult>
    {
        private IRepository<Experience,int> _repository;
        private IMapper _mapper;
        public GetExperienceByIdHeandler(IMapper mapper, IRepository<Experience, int> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<GetExpirianceByIdResult> Handle(GetExperiancByIdComand request, CancellationToken cancellationToken)
        {
            var resault =  await _repository.GetByIdAsync(request.Id);
            var respons =  _mapper.Map<GetExpirianceByIdResult>(resault);

            return respons;
        }
    }
}

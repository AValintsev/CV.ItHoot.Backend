using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Education.Comands;
using CVBuilder.Application.Education.Response;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Education.Handlers
{
    internal class GetEducationByIdHeandlers : IRequestHandler<GetEducationByIdComand, EducationByIdResult>
    {
        IMapper _mapper;
        IRepository<CVBuilder.Models.Entities.Education, int> _repository;
        public GetEducationByIdHeandlers(IMapper mapper, IRepository<CVBuilder.Models.Entities.Education, int> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<EducationByIdResult> Handle(GetEducationByIdComand request, CancellationToken cancellationToken)
        {
            var education = await _repository.Table.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            var result = _mapper.Map<EducationByIdResult>(education);
            return result;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Education.Commands;
using CVBuilder.Application.Education.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Education.Handlers
{
    using Models.Entities;
    internal class GetEducationByIdHandler : IRequestHandler<GetEducationByIdCommand, EducationByIdResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Education, int> _repository;
        public GetEducationByIdHandler(IMapper mapper, IRepository<Education, int> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<EducationByIdResult> Handle(GetEducationByIdCommand request, CancellationToken cancellationToken)
        {
            var education = await _repository.Table.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            
            if (education == null)
            {
                throw new NotFoundException("Education not found");
            }
            
            var result = _mapper.Map<EducationByIdResult>(education);
            return result;
        }
    }
}

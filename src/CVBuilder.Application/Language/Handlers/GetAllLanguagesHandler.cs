using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Language.Queries;
using CVBuilder.Application.Language.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Language.Handlers
{
    public class GetAllLanguagesHandler : IRequestHandler<GetAllLanguagesQuery, List<LanguageResult>>
    {
        private readonly IRepository<UserLanguage, int> _repository;
        private readonly IMapper _mapper;

        public GetAllLanguagesHandler(IRepository<UserLanguage, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LanguageResult>> Handle(GetAllLanguagesQuery request,
            CancellationToken cancellationToken)
        {
            var educations = await _repository.GetListAsync();
            var result = _mapper.Map<List<LanguageResult>>(educations);
            
            return result;
        }
    }
}
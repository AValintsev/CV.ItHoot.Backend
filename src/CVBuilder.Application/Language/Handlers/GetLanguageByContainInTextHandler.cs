using AutoMapper;
using CVBuilder.Application.Language.DTOs;
using CVBuilder.Application.Language.Queries;
using CVBuilder.Repository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Language.Handlers
{
    public class GetLanguageByContainInTextHandler : IRequestHandler<GetLanguageByContainInTextQuery, IEnumerable<LanguageDTO>>
    {
        private readonly IRepository<Models.Entities.Language, int> _languageRepository;
        private readonly IMapper _mapper;
        public GetLanguageByContainInTextHandler(IMapper mapper,
            IRepository<Models.Entities.Language, int> languageRepository)
        {
            _mapper = mapper;
            _languageRepository = languageRepository;
        }
        public async Task<IEnumerable<LanguageDTO>> Handle(GetLanguageByContainInTextQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                request.Content = "";
            }

            var languages = await _languageRepository
                .Table
                .Where(language => language.Name.ToLower().StartsWith(request.Content))
                .Take(10)
                .ToListAsync();


            var result = _mapper.Map<IEnumerable<LanguageDTO>>(languages);

            return result;
        }
    }
}

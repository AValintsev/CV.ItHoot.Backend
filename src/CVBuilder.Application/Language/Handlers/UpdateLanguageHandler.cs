using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Language.Commands;
using CVBuilder.Application.Language.DTOs;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Language.Handlers
{
    public class UpdateLanguageHandler : IRequestHandler<UpdateLanguageCommand, LanguageDTO>
    {
        private readonly IRepository<Models.Entities.Language, int> _languageRepository;
        private readonly IMapper _mapper;

        public UpdateLanguageHandler(IRepository<Models.Entities.Language, int> languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<LanguageDTO> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var model = new Models.Entities.Language
            {
                Id = request.Id,
                Name = request.Name,
                UpdatedAt = DateTime.Now
            };
            var skill = await _languageRepository.UpdateAsync(model);
            return _mapper.Map<LanguageDTO>(skill);
        }
    }
}
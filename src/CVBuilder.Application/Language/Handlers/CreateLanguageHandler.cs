using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Language.Commands;
using CVBuilder.Application.Language.DTOs;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Language.Handlers
{
    public class CreateLanguageHandler : IRequestHandler<CreateLanguageCommand, LanguageDTO>
    {
        private readonly IRepository<UserLanguage, int> _repository;
        private readonly IMapper _mapper;

        public CreateLanguageHandler(IRepository<UserLanguage, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LanguageDTO> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                request.Name = "";
            }

            var model = new UserLanguage
            {
                Name = request.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var language = await _repository.CreateAsync(model);

            return _mapper.Map<LanguageDTO>(language);
        }
    }
}
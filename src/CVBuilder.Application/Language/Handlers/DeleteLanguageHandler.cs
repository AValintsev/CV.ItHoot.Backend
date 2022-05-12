using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Language.Commands;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Language.Handlers
{
    public class DeleteLanguageHandler: IRequestHandler<DeleteLanguageCommand, bool>
    {
        private readonly IRepository<UserLanguage, int> _languageRepository;
        private readonly IMapper _mapper;
        public DeleteLanguageHandler(IRepository<UserLanguage, int> languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            await _languageRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
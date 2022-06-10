using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.Resume.Handlers
{
    public class RecoverResumeHandler : IRequestHandler<RecoverResumeCommand, ResumeCardResult>
    {
        private readonly IMapper _mapper;
        private readonly IDeletableRepository<Models.Entities.Resume, int> _resumeRepository;

        public RecoverResumeHandler(
            IMapper mapper,
            IDeletableRepository<Models.Entities.Resume, int> resumeRepository)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }

        public async Task<ResumeCardResult> Handle(RecoverResumeCommand command, CancellationToken cancellationToken)
        {
            var resume = await _resumeRepository.RecoverAsync(command.Id);

            return _mapper.Map<ResumeCardResult>(resume);
        }

    }
}
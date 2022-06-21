using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers
{
    public class DeleteResumeHandler : IRequestHandler<DeleteResumeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Models.Entities.Resume, int> _resumeRepository;

        public DeleteResumeHandler(
            IMapper mapper,
            IRepository<Models.Entities.Resume, int> resumeRepository)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteResumeCommand command, CancellationToken cancellationToken)
        {
            var resume = await _resumeRepository.GetByIdAsync(command.Id);
            // resume.DeletedAt = DateTime.UtcNow;
            await _resumeRepository.DeleteAsync(resume);
            return true;
        }
    }
}
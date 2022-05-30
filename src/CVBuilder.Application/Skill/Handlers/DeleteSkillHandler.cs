using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Skill.Commands;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Skill.Handlers
{
    using Models.Entities;
    public class DeleteSkillHandler : IRequestHandler<DeleteSkillCommand, bool>
    {
        private readonly IRepository<Skill, int> _skillRepository;
        private readonly IMapper _mapper;
        public DeleteSkillHandler(IRepository<Skill, int> cvRepository, IMapper mapper)
        {
            _skillRepository = cvRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
             await _skillRepository.DeleteAsync(request.Id);
             return true;
        }
    }
}
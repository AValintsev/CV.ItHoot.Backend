using System;
using AutoMapper;
using CVBuilder.Application.Skill.Commands;
using CVBuilder.Application.Skill.DTOs;
using CVBuilder.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.Skill.Handlers
{
    internal class CreateSkillHandler : IRequestHandler<CreateSkillCommand, SkillResult>
    {
        private readonly IRepository<Models.Entities.Skill, int> _skillRepository;
        private readonly IMapper _mapper;
        public CreateSkillHandler(IRepository<Models.Entities.Skill, int> cvRepository, IMapper mapper)
        {
            _skillRepository = cvRepository;
            _mapper = mapper;
        }
        public async Task<SkillResult> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                request.Name = "";
            }

            var model = new Models.Entities.Skill
            {
                Name = request.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var skill = await _skillRepository.CreateAsync(model);

            return _mapper.Map<SkillResult>(skill);
        }
    }
}

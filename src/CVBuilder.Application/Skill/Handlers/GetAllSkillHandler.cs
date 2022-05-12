using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Skill.DTOs;
using CVBuilder.Application.Skill.Queries;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Skill.Handlers
{
    using Models.Entities;

    public class GetAllSkillHandle : IRequestHandler<GetAllSkillQuery, IEnumerable<SkillDTO>>
    {
        private readonly IRepository<Skill, int> _skillRepository;
        private readonly IMapper _mapper;

        public GetAllSkillHandle(IRepository<Skill, int> cvRepository, IMapper mapper)
        {
            _skillRepository = cvRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<SkillDTO>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            var skills = await _skillRepository
                .Table.ToListAsync(cancellationToken: cancellationToken);

            var result = _mapper.Map<List<SkillDTO>>(skills);

            return result;
        }
    }
}
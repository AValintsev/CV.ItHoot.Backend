using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class GetSkillsByTextContentHandler: IRequestHandler<GetSkillByContainInTextQuery, IEnumerable<SkillDTO>>
    {
        private readonly IRepository<Models.Entities.Skill, int> _skillRepository;
        private readonly IMapper _mapper;
        public GetSkillsByTextContentHandler(IRepository<Models.Entities.Skill, int> cvRepository, IMapper mapper)
        {
            _skillRepository = cvRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<SkillDTO>> Handle(GetSkillByContainInTextQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                request.Content = "";
            }

            var skills=  await _skillRepository
                .Table
                .Where(skill => skill.Name.ToLower().StartsWith(request.Content.ToLower()))
                .Take(10)
                .ToListAsync();


           var result = _mapper.Map<List<SkillDTO>>(skills);

           return result;
        }
    }
}

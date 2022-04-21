using System;
using System.Collections.Generic;
using System.Linq;
using CVBuilder.Application.CV.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using AutoMapper;
using CVBuilder.Application.CV.Responses.CvResponses;
using CVBuilder.Models;

namespace CVBuilder.Application.CV.Handlers
{
    public class CreateCVHandler : IRequestHandler<CreateCvCommand, CvResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Cv, int> _cvRepository;
        private readonly IRepository<Models.Entities.Skill, int> _skillRepository;
        private readonly IRepository<Models.Entities.LevelSkill, int> _levelSkillRepository;

        public CreateCVHandler(
            IMapper mapper,
            IRepository<Cv, int> cvRepository,
            IRepository<Models.Entities.Skill, int> skillRepository, IRepository<LevelSkill, int> levelSkill)
        {
            _cvRepository = cvRepository;
            _skillRepository = skillRepository;
            _levelSkillRepository = levelSkill;
            _mapper = mapper;
        }

        public async Task<CvResult> Handle(CreateCvCommand command, CancellationToken cancellationToken)
        {
            var newCv = _mapper.Map<Cv>(command);

            var newSkillsWithLevel = GetNewSkills(command.Skills);
            await _skillRepository.CreateManyAsync(newSkillsWithLevel.Select(skill => skill.Key ).ToList());
            var existSkillsWithLevel = GetExistsSkill(command.Skills);

            var allSkill = newSkillsWithLevel.Concat(existSkillsWithLevel);

            //await _skillRepository.CreateManyAsync(newSkills);
            //var existingSkill = GetExistingSkills(command.Skills);

            var cv = await _cvRepository.CreateAsync(newCv);
            return null;
        }

        private List<LevelSkill> CreateNewSkillLevel(List<KeyValuePair<Models.Entities.Skill, SkillLevel>> skills, Cv cv)
        {
            return skills.Select(skill => new LevelSkill()
            {
                Skill = skill.Key,
                SkillId = skill.Key.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Cv = cv,
                CvId = cv.Id,
            }).ToList();
        }

        private List<KeyValuePair<Models.Entities.Skill, SkillLevel>> GetNewSkills(IEnumerable<CVSkill> skills)
        {
            return skills
                .Where(skill => skill.SkillId == null)
                .Select(skill => new KeyValuePair<Models.Entities.Skill, SkillLevel>(new Models.Entities.Skill
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Name = skill.Name,
                },
                skill.Level)).ToList();
        }

        private List<KeyValuePair<Models.Entities.Skill, SkillLevel>> GetExistsSkill(IEnumerable<CVSkill> skills)
        {
            return skills
                .Where(skill => skill.SkillId == null)
                .Select(skill => new KeyValuePair<Models.Entities.Skill, SkillLevel>(new Models.Entities.Skill
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Name = skill.Name,
                },
                skill.Level)).ToList();
        }
    }
}

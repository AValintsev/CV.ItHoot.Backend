﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers
{
    using Models.Entities;

    internal class CreateResumeHandler : IRequestHandler<CreateResumeCommand, ResumeResult>
    {
        private readonly IMapper _mapper;
        private readonly IDeletableRepository<Resume, int> _cvRepository;
        private readonly IRepository<Resume, int> _resumeRepository;
        private readonly IRepository<Skill, int> _skillRepository;
        private readonly IRepository<Language, int> _languageRepository;

        public CreateResumeHandler(
            IMapper mapper,
            IDeletableRepository<Resume, int> cvRepository,
            IRepository<Resume, int> resumeRepository,
            IRepository<Skill, int> skillRepository,
            IRepository<Language, int> languageRepository)
        {
            _resumeRepository = resumeRepository;
            _skillRepository = skillRepository;
            _languageRepository = languageRepository;
            _mapper = mapper;
            _cvRepository = cvRepository;
        }

        public async Task<ResumeResult> Handle(CreateResumeCommand command, CancellationToken cancellationToken)
        {
            var resume = _mapper.Map<Resume>(command);
            await CheckSkillsDuplicate(resume);
            await CheckLanguageDuplicate(resume);
            CheckHiddenValues(resume);
            resume = await _resumeRepository.CreateAsync(resume);
            return _mapper.Map<ResumeResult>(resume);
        }

        private void CheckHiddenValues(Resume resume)
        {
        }

        private async Task CheckLanguageDuplicate(Resume resume)
        {
            var allLanguage = await _languageRepository.GetListAsync();
            foreach (var cvLanguage in resume.LevelLanguages)
            {
                var language = allLanguage
                    .FirstOrDefault(x => x.Id == cvLanguage.LanguageId || x.Name == cvLanguage.Language?.Name);
                if (language == null)
                    continue;
                cvLanguage.Language = language;
                cvLanguage.LanguageId = language.Id;
            }
        }

        private async Task CheckSkillsDuplicate(Resume resume)
        {
            var allSkills = await _skillRepository.GetListAsync();
            foreach (var cvSkill in resume.LevelSkills)
            {
                var skill = allSkills.FirstOrDefault(x => x.Id == cvSkill.SkillId || x.Name == cvSkill.Skill?.Name);
                if (skill == null)
                    continue;
                cvSkill.SkillId = skill.Id;
                cvSkill.Skill = skill;
            }
        }
    }
}
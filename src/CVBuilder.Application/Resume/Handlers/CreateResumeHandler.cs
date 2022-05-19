using System.Linq;
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
        private readonly IRepository<Resume, int> _cvRepository;
        private readonly IRepository<Skill, int> _skillRepository;
        private readonly IRepository<Language, int> _languageRepository;

        public CreateResumeHandler(
            IMapper mapper,
            IRepository<Resume, int> cvRepository,
            IRepository<Skill, int> skillRepository, IRepository<LevelSkill, int> levelSkill,
            IRepository<Language, int> languageRepository)
        {
            _cvRepository = cvRepository;
            _skillRepository = skillRepository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<ResumeResult> Handle(CreateResumeCommand command, CancellationToken cancellationToken)
        {
            var cv = _mapper.Map<Resume>(command);
            await CheckSkillsDuplicate(cv);
            await CheckLanguageDuplicate(cv);
            cv = await _cvRepository.CreateAsync(cv);
            return _mapper.Map<ResumeResult>(cv);
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
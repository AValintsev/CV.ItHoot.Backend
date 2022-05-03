using CVBuilder.Application.CV.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CVBuilder.Repository;
using AutoMapper;
using CVBuilder.Application.CV.Responses.CvResponse;

namespace CVBuilder.Application.CV.Handlers
{
    using Models.Entities;

    internal class CreateCVHandler : IRequestHandler<CreateCvCommand, CvResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Cv, int> _cvRepository;
        private readonly IRepository<Skill, int> _skillRepository;
        private readonly IRepository<UserLanguage, int> _languageRepository;
        public CreateCVHandler(
            IMapper mapper,
            IRepository<Cv, int> cvRepository,
            IRepository<Skill, int> skillRepository, IRepository<LevelSkill, int> levelSkill, IRepository<UserLanguage, int> languageRepository)
        {
            _cvRepository = cvRepository;
            _skillRepository = skillRepository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<CvResult> Handle(CreateCvCommand command, CancellationToken cancellationToken)
        {
            var cv = _mapper.Map<Cv>(command);
            await CheckSkillsDuplicate(cv);
            await CheckLanguageDuplicate(cv);
            cv = await _cvRepository.CreateAsync(cv);
            return _mapper.Map<CvResult>(cv);
        }

        private async Task CheckLanguageDuplicate(Cv cv)
        {
            var allLanguage = await _languageRepository.GetListAsync();
            foreach (var language in allLanguage)
            {
                foreach (var cvLanguage in cv.LevelLanguages)
                {
                    if (language.Name == cvLanguage.UserLanguage.Name)
                    {
                        cvLanguage.UserLanguage = language;
                        cvLanguage.UserLanguageId = language.Id;
                    }     
                }
            }
        }
        private async Task CheckSkillsDuplicate(Cv cv)
        {
            var allSkills = await _skillRepository.GetListAsync();
            foreach (var skill in allSkills)
            {
                foreach (var cvSkill in cv.LevelSkills)
                {
                    if (skill.Name == cvSkill.Skill.Name)
                    {
                        cvSkill.Skill = skill;
                        cvSkill.SkillId = skill.Id;
                    }                        
                }
            }
        }
        
        
    }
}
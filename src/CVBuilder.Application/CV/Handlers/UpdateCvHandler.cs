using AutoMapper;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Repository;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.CV.Responses.CvResponse;

namespace CVBuilder.Application.CV.Handlers
{
    using Models.Entities;
    class UpdateCvHandler : IRequestHandler<UpdateCvCommand, CvResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Cv, int> _cvRepository;
        private readonly IRepository<Skill, int> _skillRepository;
        private readonly IRepository<UserLanguage, int> _languageRepository;
        public UpdateCvHandler(
            IMapper mapper,
            IRepository<Cv, int> cvRepository,
            IRepository<Skill, int> skillRepository,
            IRepository<UserLanguage, int> userLanguageRepository
           )
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
            _skillRepository = skillRepository;
            _languageRepository = userLanguageRepository;
        }
        public async Task<CvResult> Handle(UpdateCvCommand request, CancellationToken cancellationToken)
        {
            var cv = _mapper.Map<Cv>(request);
            await CheckLanguageDuplicate(cv);
            await CheckSkillsDuplicate(cv);
            var res = await _cvRepository.UpdateAsync(cv);
            return _mapper.Map<CvResult>(res);
        }
        
        private async Task CheckLanguageDuplicate(Cv cv)
        {
            var allLanguage = await _languageRepository.GetListAsync();
            foreach (var cvLanguage in cv.LevelLanguages)
            {
                var language = allLanguage
                    .FirstOrDefault(x => x.Id == cvLanguage.UserLanguageId || x.Name == cvLanguage.UserLanguage?.Name);
                if (language == null)
                    continue;
                cvLanguage.UserLanguage = language;
                cvLanguage.UserLanguageId = language.Id;
            }
        }

        private async Task CheckSkillsDuplicate(Cv cv)
        {
            var allSkills = await _skillRepository.GetListAsync();
            foreach (var cvSkill in cv.LevelSkills)
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

using AutoMapper;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Repository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.CV.Responses.CvResponse;

namespace CVBuilder.Application.CV.Handlers
{
    using Models.Entities;
    class UpdateCvHandler : IRequestHandler<UpdateCvCommand, UpdateCvResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Cv, int> _cvRepository;

        private readonly IRepository<Skill, int> _skillRepository;
        private readonly IRepository<UserLanguage, int> _userLanguageRepository;
        private readonly IRepository<Experience, int> _experienceRepository;
        private readonly IRepository<Education, int> _educationRepository;
        public UpdateCvHandler(
            IMapper mapper,
            IRepository<Cv, int> cvRepository,
            IRepository<Education, int> educationRepository,
            IRepository<Experience, int> experienceRepository,
            IRepository<UserLanguage, int> userLanguageRepository,
            IRepository<Skill, int> skillRepository)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
            _skillRepository = skillRepository;
            _userLanguageRepository = userLanguageRepository;
            _experienceRepository = experienceRepository;
            _educationRepository = educationRepository;
        }
        public async Task<UpdateCvResult> Handle(UpdateCvCommand request, CancellationToken cancellationToken)
        {
            if (request.RSkills.Count > 0) {
                 var temp = _mapper.Map<List<Skill>>(request.RSkills);
                 await  _skillRepository.RemoveManyAsync(temp);
            }

            if (request.RUserLanguages.Count > 0)
            {
                var temp = _mapper.Map<List<UserLanguage>>(request.RUserLanguages);
                await _userLanguageRepository.RemoveManyAsync(temp);
            }

            if (request.RExperiences.Count > 0)
            {
                await _experienceRepository.RemoveManyAsync(_mapper.Map<List<Experience>>(request.RExperiences));
            }

            if (request.REducations.Count > 0)
            {
                await _educationRepository.RemoveManyAsync(_mapper.Map<List<Education>>(request.REducations));
            }

            var cv = _mapper.Map<Cv>(request);
            var res = await _cvRepository.UpdateAsync(cv);
            return _mapper.Map<UpdateCvResult>(res);

        }
    }
}

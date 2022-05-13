using System;
using System.Collections.Generic;
using AutoMapper;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Repository;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.CV.Responses.CvResponse;
using CVBuilder.Models;

namespace CVBuilder.Application.CV.Handlers
{
    using Models.Entities;
    class UpdateCvHandler : IRequestHandler<UpdateCvCommand, CvResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Cv, int> _cvRepository;
        private readonly IRepository<Skill, int> _skillRepository;
        private readonly IRepository<LevelSkill, int> _skillLevelRepository;
        private readonly IRepository<UserLanguage, int> _languageRepository;
        public UpdateCvHandler(
            IMapper mapper,
            IRepository<Cv, int> cvRepository,
            IRepository<Skill, int> skillRepository,
            IRepository<UserLanguage, int> userLanguageRepository, IRepository<LevelSkill, int> skillLevelRepository)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
            _skillRepository = skillRepository;
            _languageRepository = userLanguageRepository;
            _skillLevelRepository = skillLevelRepository;
        }
        public async Task<CvResult> Handle(UpdateCvCommand request, CancellationToken cancellationToken)
        {
            var cv = _mapper.Map<Cv>(request);
            var cvDto = await _cvRepository.GetByIdAsync(cv.Id,"Educations,Experiences,LevelLanguages,LevelSkills");
           MapFromRequest(cv,cvDto);
            var res = await _cvRepository.UpdateAsync(cvDto);
            return _mapper.Map<CvResult>(res);
        }

        public void MapFromRequest(Cv requestCV, Cv dtoCv)
        {
            dtoCv.UserId = requestCV.UserId;
            dtoCv.UpdatedAt = DateTime.UtcNow;
            dtoCv.AboutMe = requestCV.AboutMe;
            dtoCv.CvName = requestCV.CvName;
            dtoCv.FirstName = requestCV.FirstName;
            dtoCv.LastName = requestCV.LastName;
            dtoCv.Email = requestCV.Email;
            dtoCv.Site = requestCV.Site;
            dtoCv.Phone = requestCV.Phone;
            dtoCv.Code = requestCV.Code;
            dtoCv.Country = requestCV.Country;
            dtoCv.City = requestCV.City;
            dtoCv.Street = requestCV.Street;
            dtoCv.RequiredPosition = requestCV.RequiredPosition;
            dtoCv.Birthdate = requestCV.Birthdate;
            dtoCv.Educations = requestCV.Educations;
            dtoCv.Experiences = requestCV.Experiences;
            dtoCv.LevelLanguages = requestCV.LevelLanguages;
            dtoCv.LevelSkills = requestCV.LevelSkills;
        }
    }
}

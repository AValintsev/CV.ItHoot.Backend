using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

class UpdateResumeHandler : IRequestHandler<UpdateResumeCommand, ResumeResult>
{
    private readonly IMapper _mapper;
    private readonly IDeletableRepository<Resume, int> _resumeRepository;

    public UpdateResumeHandler(IMapper mapper, IDeletableRepository<Resume, int> resumeRepository)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
    }

    public async Task<ResumeResult> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
    {
        var resume = _mapper.Map<Resume>(request);
        var resumeDb = await _resumeRepository
            .GetByIdAsync(resume.Id,  "Educations,Experiences,LevelLanguages,LevelSkills");

        if (resumeDb == null)
            throw new NotFoundException("Resume not found");

        
        MapFromRequest(resumeDb, resume);
        MapHiddenValues(resumeDb, resume);
        var result = await _resumeRepository.UpdateAsync(resumeDb);
        return _mapper.Map<ResumeResult>(result);
    }

    private void MapHiddenValues(Resume resumeDb, Resume resume)
    {
        resumeDb.AvailabilityStatus = resume.AvailabilityStatus;
        resumeDb.SalaryRate = resume.SalaryRate;
        resumeDb.CountDaysUnavailable = resume.CountDaysUnavailable;
    }

    private static void MapFromRequest(Resume resumeDb, Resume resume)
    {
        resumeDb.UpdatedAt = DateTime.UtcNow;
        resumeDb.AboutMe = resume.AboutMe;
        resumeDb.ResumeTemplateId = resume.ResumeTemplateId == 0 ? 3 : resume.ResumeTemplateId;
        resumeDb.ResumeName = resume.ResumeName;
        resumeDb.FirstName = resume.FirstName;
        resumeDb.LastName = resume.LastName;
        resumeDb.PositionId = resume.PositionId;
        resumeDb.Email = resume.Email;
        resumeDb.Site = resume.Site;
        resumeDb.Phone = resume.Phone;
        resumeDb.Code = resume.Code;
        resumeDb.Country = resume.Country;
        resumeDb.City = resume.City;
        resumeDb.Street = resume.Street;
        resumeDb.RequiredPosition = resume.RequiredPosition;
        resumeDb.Birthdate = resume.Birthdate;
        resumeDb.Educations = resume.Educations;
        resumeDb.Experiences = resume.Experiences;
        resumeDb.LevelLanguages = resume.LevelLanguages;
        resumeDb.LevelSkills = resume.LevelSkills;
    }
}
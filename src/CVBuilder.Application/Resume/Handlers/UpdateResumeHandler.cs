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
    private readonly IDeletableRepository<Resume, int> _cvRepository;

    public UpdateResumeHandler(IMapper mapper, IDeletableRepository<Resume, int> cvRepository)
    {
        _cvRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<ResumeResult> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
    {
        var resume = _mapper.Map<Resume>(request);
        var resumeDto = await _cvRepository
            .GetByIdAsync(resume.Id,  "Educations,Experiences,LevelLanguages,LevelSkills");

        if (resumeDto == null)
            throw new NotFoundException("Resume not found");


        MapFromRequest(resume, resumeDto);
        var result = await _cvRepository.UpdateAsync(resumeDto);
        return _mapper.Map<ResumeResult>(result);
    }

    private void MapFromRequest(Resume requestResume, Resume dtoResume)
    {
        dtoResume.UpdatedAt = DateTime.UtcNow;
        dtoResume.AboutMe = requestResume.AboutMe;
        dtoResume.ResumeTemplateId = requestResume.ResumeTemplateId == 0 ? 3 : requestResume.ResumeTemplateId;
        dtoResume.ResumeName = requestResume.ResumeName;
        dtoResume.FirstName = requestResume.FirstName;
        dtoResume.LastName = requestResume.LastName;
        dtoResume.PositionId = requestResume.PositionId;
        dtoResume.Email = requestResume.Email;
        dtoResume.Site = requestResume.Site;
        dtoResume.Phone = requestResume.Phone;
        dtoResume.Code = requestResume.Code;
        dtoResume.Country = requestResume.Country;
        dtoResume.City = requestResume.City;
        dtoResume.Street = requestResume.Street;
        dtoResume.RequiredPosition = requestResume.RequiredPosition;
        dtoResume.Birthdate = requestResume.Birthdate;
        dtoResume.Educations = requestResume.Educations;
        dtoResume.Experiences = requestResume.Experiences;
        dtoResume.LevelLanguages = requestResume.LevelLanguages;
        dtoResume.LevelSkills = requestResume.LevelSkills;
    }
}
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ResumeResult = CVBuilder.Application.Resume.Responses.CvResponse.ResumeResult;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class UpdateSalaryRateHandler : IRequestHandler<UpdateSalaryRateResumeCommand, ResumeCardResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Resume, int> _resumeRepository;

    public UpdateSalaryRateHandler(IMapper mapper, IRepository<Resume, int> resumeRepository)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
    }

    public async Task<ResumeCardResult> Handle(UpdateSalaryRateResumeCommand request, CancellationToken cancellationToken)
    {
        var resumeDb = await _resumeRepository.Table
            .Include(x => x.ResumeTemplate)
            .Include(x => x.Image)
            .Include(x => x.Educations)
            .Include(x => x.Experiences)
            .Include(x => x.LevelSkills)
            .ThenInclude(l => l.Skill)
            .Include(x => x.Position)
            .Include(x => x.LevelLanguages)
            .ThenInclude(l => l.Language)
            .FirstOrDefaultAsync(x => x.Id == request.ResumeId, cancellationToken: cancellationToken);

        if (resumeDb == null)
            throw new NotFoundException("Resume not found");

        resumeDb.SalaryRate = request.SalaryRate;
        resumeDb.Id = 0;
        resumeDb = await _resumeRepository.CreateAsync(resumeDb);
        return _mapper.Map<ResumeCardResult>(resumeDb);
    }
}
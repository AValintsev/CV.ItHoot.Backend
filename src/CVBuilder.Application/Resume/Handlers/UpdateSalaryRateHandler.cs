using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Repository;
using MediatR;
using ResumeResult = CVBuilder.Application.Resume.Responses.CvResponse.ResumeResult;

namespace CVBuilder.Application.Resume.Handlers;
using Models.Entities;

public class UpdateSalaryRateHandler: IRequestHandler<UpdateSalaryRateResumeCommand, ResumeResult>
{
    private readonly IMapper _mapper;
    private readonly IDeletableRepository<Resume, int> _resumeRepository;

    public UpdateSalaryRateHandler(IMapper mapper, IDeletableRepository<Resume, int> resumeRepository)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
    }

    public async Task<ResumeResult> Handle(UpdateSalaryRateResumeCommand request, CancellationToken cancellationToken)
    {
        var resumeDb = await _resumeRepository
            .GetByIdAsync(request.ResumeId);

        if (resumeDb == null)
            throw new NotFoundException("Resume not found");

        resumeDb.SalaryRate = request.SalaryRate;
        
        var result = await _resumeRepository.UpdateAsync(resumeDb);
        return _mapper.Map<ResumeResult>(result);
    }

 
   
}
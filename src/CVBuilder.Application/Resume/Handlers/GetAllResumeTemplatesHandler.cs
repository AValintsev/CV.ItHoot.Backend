using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;
using Models.Entities;
public class GetAllResumeTemplatesHandler: IRequestHandler<GetAllResumeTemplatesQuery, List<ResumeTemplateResult>>
{
    private readonly IRepository<ResumeTemplate, int> _repository;
    private readonly IMapper _mapper;

    public GetAllResumeTemplatesHandler(IRepository<ResumeTemplate, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ResumeTemplateResult>> Handle(GetAllResumeTemplatesQuery request, CancellationToken cancellationToken)
    {
        var templates = await _repository.Table.ToListAsync(cancellationToken: cancellationToken);
        var result = _mapper.Map<List<ResumeTemplateResult>>(templates);
        return result;
    }
}
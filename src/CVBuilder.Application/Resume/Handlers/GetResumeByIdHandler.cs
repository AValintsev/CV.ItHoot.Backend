using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Extensions;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers
{
    using Models.Entities;
    public class GetResumeByIdHandler : IRequestHandler<GetResumeByIdQuery, ResumeResult>
    {
        private readonly IRepository<Resume, int> _cvRepository;
        private readonly IMapper _mapper;
        public GetResumeByIdHandler(
            IRepository<Resume, int> cvRepository,
            IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<ResumeResult> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
        {
            Resume resume;
            var resumeRequest =  _cvRepository.Table
                .Include(x => x.Image)
                .Include(x => x.Educations)
                .Include(x => x.Experiences)
                .Include(x => x.LevelSkills)
                .ThenInclude(l => l.Skill)
                .Include(x => x.Position)
                .Include(x => x.LevelLanguages)
                .ThenInclude(l => l.Language);
            // if (request.UserRoles.Contains(Enums.RoleTypes.Admin.ToString()))
            // {
               
                   resume = await resumeRequest.FirstOrDefaultAsync(x=>x.Id == request.Id,cancellationToken: cancellationToken);
            // }
            // else
            // {
                // resume = await resumeRequest.FirstOrDefaultAsync(x =>
                //     x.CreatedUserId == request.UserId && x.Id == request.Id, cancellationToken: cancellationToken);
            // }
           

            if (resume == null)
            {
                throw new NotFoundException("Resume not found");
            }
            

            var cvResult = _mapper.Map<ResumeResult>(resume);
            return cvResult;
        }
    }
}

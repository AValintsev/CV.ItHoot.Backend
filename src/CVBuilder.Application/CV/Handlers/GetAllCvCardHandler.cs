using AutoMapper;
using CVBuilder.Application.CV.Responses;
using CVBuilder.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CVBuilder.Application.CV.Queries;

namespace CVBuilder.Application.CV.Handlers
{
    public class GetAllCvCardHandler : IRequestHandler<GetAllCvCardQueries, GetAllCvCardResult>
    {
        private readonly IRepository<Models.Entities.Cv, int> _cvRepository;
        private readonly IMapper _mapper;

        public GetAllCvCardHandler(IRepository<Models.Entities.Cv, int> cvRepository, IMapper mapper)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<GetAllCvCardResult> Handle(GetAllCvCardQueries request, CancellationToken cancellationToken)
        {
            List<Cv> result = new List<Cv>();
            if (request.UserRoles.Contains("HR"))
            {
                result = await _cvRepository.Table
                    .Where(x => x.IsDraft == false)
                    .ToListAsync();
            }
            else if (request.UserRoles.Contains("Admin"))
            {
                result = await _cvRepository.GetListAsync();
            }
            else if (request.UserRoles.Contains("User"))
            {
                result = await _cvRepository.Table
                    .Where(x => x.UserId == request.UserId)
                    .ToListAsync();
            }

            return new GetAllCvCardResult
            {
                CvCards = _mapper.Map<List<CvCardResult>>(result),
            };
        }
    }
}

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Responses.CvResponse;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.CV.Handlers
{
    public class DeleteCvHandler:IRequestHandler<DeleteCvCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Cv, int> _cvRepository;

        public DeleteCvHandler(
            IMapper mapper,
            IRepository<Cv, int> cvRepository)
        {
            _cvRepository = cvRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteCvCommand command, CancellationToken cancellationToken)
        {
            var cv = _cvRepository.Table.Include(x=>x.Files).FirstOrDefault(x => x.Id == command.Id);
            cv.Files = null;
            await _cvRepository.UpdateAsync(cv);
            await _cvRepository.DeleteAsync(cv.Id);
            return true;
        }

    }
}
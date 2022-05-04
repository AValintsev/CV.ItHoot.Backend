using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Responses.CvResponse;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

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
            await _cvRepository.DeleteAsync(command.Id);
            return true;
        }

    }
}
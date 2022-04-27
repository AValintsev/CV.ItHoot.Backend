using AutoMapper;
using CVBuilder.Application.Files.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Files.Queries;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Files.Handlers
{
    public class GetFileByIdHandler : IRequestHandler<GetFileByIdComand, FileResult>
    {
        private readonly IRepository<File, int> _fileRepository;
        private readonly IMapper _mapper;

        public GetFileByIdHandler(
            IRepository<File, int> fileRepository,
            IMapper mapper
        )
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }
        public async Task<FileResult> Handle(GetFileByIdComand request, CancellationToken cancellationToken)
        {
            var file =  await _fileRepository.Table.FirstOrDefaultAsync(file => file.Id == request.Id, cancellationToken);

            return _mapper.Map<FileResult>(file);
        }
    }
}

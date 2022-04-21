using AutoMapper;
using CVBuilder.Application.Files.Comands;
using CVBuilder.Application.Files.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Files.Queries;

namespace CVBuilder.Application.Files.Handlers
{
    class GetFileByIdHandler : IRequestHandler<GetFileByIdComand, FileResult>
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
            File file =  _fileRepository.Table.FirstOrDefault(file => file.Id == request.Id);

            return _mapper.Map<FileResult>(file);
        }
    }
}

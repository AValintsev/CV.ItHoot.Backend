using AutoMapper;
using CVBuilder.Application.Files.Comands;
using CVBuilder.Application.Files.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CVBuilder.Application.Files.Handlers
{
    class UploadFileHeandler : IRequestHandler<UploadFileComand, FileUploadResult>
    {
        private readonly IRepository<File, int> _fileRepository;
        private readonly IMapper _mapper;

        public UploadFileHeandler(
            IRepository<File, int> fileRepository,
            IMapper mapper
            )
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<FileUploadResult> Handle(UploadFileComand request, CancellationToken cancellationToken)
        {
            File file = _mapper.Map<File>(request);

            var respons = await _fileRepository.CreateAsync(file);

            return _mapper.Map<FileUploadResult>(respons);

        }
    }
}

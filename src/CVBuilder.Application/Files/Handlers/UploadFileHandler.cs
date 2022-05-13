using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using CVBuilder.Application.Files.Comands;
using CVBuilder.Application.Files.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Files.Handlers
{
    class UploadFileHandler : IRequestHandler<UploadFileComand, FileUploadResult>
    {
        private readonly IRepository<File, int> _fileRepository;
        private readonly IMapper _mapper;

        public UploadFileHandler(
            IRepository<File, int> fileRepository,
            IMapper mapper
            )
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<FileUploadResult> Handle(UploadFileComand request, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<File>(request);
            file.CreatedAt = DateTime.UtcNow;
            var files =  await _fileRepository.Table.Where(x=>x.CvId == request.IdCv).ToListAsync(cancellationToken: cancellationToken);
            await _fileRepository.RemoveManyAsync(files);
            var response = await _fileRepository.CreateAsync(file);

            return _mapper.Map<FileUploadResult>(response);

        }
    }
}

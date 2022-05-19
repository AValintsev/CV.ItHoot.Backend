using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class UploadImageHandler : IRequestHandler<UploadResumeImageCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Resume, int> _cvRepository;
    private readonly IImageService _imageService;

    public UploadImageHandler(IMapper mapper, IRepository<Resume, int> cvRepository, IImageService imageService)
    {
        _mapper = mapper;
        _cvRepository = cvRepository;
        _imageService = imageService;
    }

    public async Task<bool> Handle(UploadResumeImageCommand request, CancellationToken cancellationToken)
    {
        var resume = await _cvRepository.Table
            .Include(x => x.Image)
            .FirstOrDefaultAsync(x => x.Id == request.ResumeId, cancellationToken: cancellationToken);
        
        var imagePath = await _imageService.UploadImage(request.FileType, request.Data);

        if (resume.Image == null)
        {
            resume.Image = new Image
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImagePath = imagePath
            };
        }
        else
        {
            resume.Image.ImagePath = imagePath;
            resume.Image.UpdatedAt = DateTime.UtcNow;
        }
     
        await _cvRepository.UpdateAsync(resume);
        return true;
    }
}
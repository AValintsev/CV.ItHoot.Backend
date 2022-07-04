using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Exceptions;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class UploadImageHandler : IRequestHandler<UploadResumeImageCommand, ImageResult>
{
    private readonly IMapper _mapper;
    private readonly IDeletableRepository<Resume, int> _cvRepository;
    private readonly IRepository<Image, int> _imageRepository;
    private readonly IImageService _imageService;

    public UploadImageHandler(IMapper mapper, IDeletableRepository<Resume, int> cvRepository, IImageService imageService, IRepository<Image, int> imageRepository)
    {
        _mapper = mapper;
        _cvRepository = cvRepository;
        _imageService = imageService;
        _imageRepository = imageRepository;
    }

    public async Task<ImageResult> Handle(UploadResumeImageCommand request, CancellationToken cancellationToken)
    {
        Image image = null;
        
        if (request.ResumeId != null)
        {
            var resume = await _cvRepository.Table
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == request.ResumeId, cancellationToken: cancellationToken);
            var imagePath = await _imageService.UploadImage(request.FileType, request.Data);
            if (resume == null)
                throw new NotFoundException("Resume not found");
            
            if (resume.Image == null )
            {
                image = new Image
                {
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ImagePath = imagePath
                };
                resume.Image = image;
            }
            else
            {
                resume.Image.ImagePath = imagePath;
                resume.Image.UpdatedAt = DateTime.UtcNow;
            }
            await _cvRepository.UpdateAsync(resume);
        }
        else
        {
            var imagePath = await _imageService.UploadImage(request.FileType, request.Data);
            
            image = new Image
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImagePath = imagePath
            };
            image = await _imageRepository.CreateAsync(image);
        }

        var result = _mapper.Map<ImageResult>(image);
        return result;
    }
}
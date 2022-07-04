using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class UploadResumeImageCommand : IRequest<ImageResult>
{
    public int? ResumeId { get; set; }
    public byte[] Data { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
}
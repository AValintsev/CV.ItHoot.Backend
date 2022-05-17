using MediatR;
using Microsoft.AspNetCore.Http;

namespace CVBuilder.Application.Resume.Commands;

public class UploadResumeImageCommand:IRequest<bool>
{
    public int ResumeId { get; set; }
    public byte[] Data { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
}
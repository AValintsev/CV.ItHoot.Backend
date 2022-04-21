using CVBuilder.Application.Files.Responses;
using MediatR;

namespace CVBuilder.Application.Files.Comands
{
    public class UploadFileComand : IRequest<FileUploadResult>
    {
        public int? IdCv { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
    }
}

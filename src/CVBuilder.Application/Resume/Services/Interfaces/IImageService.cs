using System.Threading.Tasks;

namespace CVBuilder.Application.Resume.Services.Interfaces;

public interface IImageService
{
    Task<string> UploadImage(string fileType, byte[] image);
}
using System.Threading.Tasks;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Resume.Services.Interfaces;

public interface IImageService
{
    Task<string> UploadImage(string fileType, byte[] image);
}
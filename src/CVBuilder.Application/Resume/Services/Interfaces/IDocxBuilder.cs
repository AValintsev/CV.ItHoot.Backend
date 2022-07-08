using CVBuilder.Application.Resume.Responses.CvResponse;
using System.IO;
using System.Threading.Tasks;

namespace CVBuilder.Application.Resume.Services.Interfaces;

public interface IDocxBuilder
{
    public Task<Stream> BindTemplateAsync(ResumeResult resume, string templatePath);
}
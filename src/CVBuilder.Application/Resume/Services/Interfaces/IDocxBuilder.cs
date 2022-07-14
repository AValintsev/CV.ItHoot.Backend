using CVBuilder.Application.Resume.Responses.CvResponse;
using System.IO;
using System.Threading.Tasks;

namespace CVBuilder.Application.Resume.Services.Interfaces;

public interface IDocxBuilder
{
    /// <summary>
    ///     Generate docx file stream from template path
    /// </summary>
    public Task<Stream> BindTemplateAsync(ResumeResult resume, string templatePath, bool isShowLogoFooter = false);
    /// <summary>
    ///     Generate docx file stream from template as byte array
    /// </summary>
    public Task<Stream> BindTemplateAsync(ResumeResult resume, byte[] template, bool isShowLogoFooter = false);
}
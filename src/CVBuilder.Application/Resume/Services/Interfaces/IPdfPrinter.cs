using System.IO;
using System.Threading.Tasks;

namespace CVBuilder.Application.Resume.Services.Interfaces;

public interface IPdfPrinter
{
    Task<Stream> PrintPdfAsync(string html);
}
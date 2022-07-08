using System.IO;
using System.Threading.Tasks;
using Page = PuppeteerSharp.Page;

namespace CVBuilder.Application.Resume.Services.Interfaces;

public interface IBrowserPdfPrinter
{
    Task<Page> LoadPageAsync(string url,string jwt = null);
    Task<Stream> PrintPdfAsync();
    
}
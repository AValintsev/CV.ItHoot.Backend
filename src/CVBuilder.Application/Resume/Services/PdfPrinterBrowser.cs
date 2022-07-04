using System.IO;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Services.Interfaces;
using PuppeteerSharp;
using MediaType = PuppeteerSharp.Media.MediaType;

namespace CVBuilder.Application.Resume.Services;

public class PdfPrinterBrowser: IPdfPrinter
{
    private readonly BrowserExtension _browser;

    public PdfPrinterBrowser(BrowserExtension browser)
    {
        _browser = browser;
    }

    public async Task<Stream> PrintPdfAsync(string html)
    {
        var browser = _browser.Browser;
        await using var page = await browser.NewPageAsync();
        await page.EmulateMediaTypeAsync(MediaType.Print);
        await page.SetContentAsync(html);
        var height = await page.EvaluateExpressionAsync<int>("document.body.offsetHeight");
        var pdfOptions = new PdfOptions()
        {
            PrintBackground = true,
            Height = height,
            Width = "210mm",
            PreferCSSPageSize = true
        };
        
        var stream = await page.PdfStreamAsync(pdfOptions);
        return stream;
    }
}
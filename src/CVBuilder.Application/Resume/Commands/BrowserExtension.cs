using System;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace CVBuilder.Application.Resume.Commands;

public class BrowserExtension : IAsyncDisposable
{
    private Browser _browser;

    public Browser Browser => _browser ??= Init();

    private static Browser Init()
    {
        var browser = Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            Args = new[]
            {
                "--no-sandbox"
            }
        }).Result;
        return browser;
    }


  
    
    
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await _browser.DisposeAsync();
    }
}

public class BrowserPdfGenerator
{
    private readonly Page _page;
    public BrowserPdfGenerator(Page page) => _page = page;
    
    public async Task SetJwtTokenAsync(string jwt)
    {
        if (!string.IsNullOrEmpty(jwt))
        {
            await _page.EvaluateExpressionOnNewDocumentAsync(
                $"window.localStorage.setItem('JWT_TOKEN', '{jwt}');");
        }
    }

    public async Task LoadPageAsync(string url, string waitForSelector = null, MediaType mediaType = MediaType.Print)
    {
        await _page.GoToAsync(url);
        await _page.EmulateMediaTypeAsync(mediaType);

        if (waitForSelector != null)
        {
            await _page.WaitForSelectorAsync(waitForSelector, new WaitForSelectorOptions()
            {
                Visible = true,
                Timeout = 5000
            });
        }
    }

    public async Task<Stream> GetStreamPdfAsync(PdfOptions pdfOptions = null)
    {
        pdfOptions ??= new PdfOptions()
        {
            PrintBackground = true,
            Format = PaperFormat.A4,
        };
        var file = await _page.PdfStreamAsync(pdfOptions);
        return file;
    }
}
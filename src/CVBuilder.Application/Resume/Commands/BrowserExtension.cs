using System;
using System.Threading.Tasks;
using PuppeteerSharp;

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
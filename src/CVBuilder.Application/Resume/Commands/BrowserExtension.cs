using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace CVBuilder.Application.Resume.Commands;

public class BrowserExtension:IAsyncDisposable
{
    public Browser Browser { get; private set; }
   
    public async void Init()
    {
        Browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            Args = new[]
            {
                "--no-sandbox"
            }
        });
    }

    public async ValueTask DisposeAsync()
    {
        await Browser.DisposeAsync();
    }
}
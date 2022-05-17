using PuppeteerSharp;

namespace CVBuilder.Application.Resume.Commands;

public class BrowserExtension
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
}
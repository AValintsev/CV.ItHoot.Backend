using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;

namespace CVBuilder.Application.CV.Commands;

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
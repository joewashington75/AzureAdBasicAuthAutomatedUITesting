using Cocona;
using Microsoft.Playwright;
using PlaywightAzureAdBasicAuth;

var builder = CoconaApp.CreateBuilder();
var app = builder.Build();

app.AddCommand(async ([Argument(Description = "Username to login with")] string userName,
    [Argument(Description = "Password to login with")] string password,
    [Argument(Description = "The url of your application as a starting point where your login button is located")] string applicationUrl,
    [Argument(Description = "The html id of your login button")] string loginButtonId) =>
{
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchPersistentContextAsync("", new BrowserTypeLaunchPersistentContextOptions()
    {
        Headless = false,
        Args = new List<string>
            {
                "-incognito"
            },
        HttpCredentials = new HttpCredentials
        {
            Username = userName,
            Password = password
        }
    });

    var page = browser.Pages.First();

    await page.GoToApplicationAsync(applicationUrl);
    await page.ClickLogonAsync(loginButtonId);
    await page.SetAzureAdLoginDetailsAsync(userName);

    Console.ReadLine();
});

app.Run();

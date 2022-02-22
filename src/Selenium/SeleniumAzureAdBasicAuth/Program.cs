using Cocona;
using OpenQA.Selenium.Chrome;
using SeleniumAzureAdBasicAuth;

var builder = CoconaApp.CreateBuilder();
var app = builder.Build();

app.AddCommand(([Argument(Description = "Username to login with")] string userName, 
        [Argument(Description = "Password to login with")] string password, 
        [Argument(Description = "The url for the auth domain that you are redirected to when you click next after inserting your username on the Azure AD login screen")] string authDomain, 
        [Argument(Description = "The url of your application as a starting point where your login button is located")]string applicationUrl, 
        [Argument(Description = "The html id of your login button")] string loginButtonId) =>
    {
        var options = new ChromeOptions();
        options.AddArguments("--incognito");

        using var driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        driver
            .GoToApplication(applicationUrl)
            .ClickLogon(loginButtonId)
            .SetAzureAdLoginDetails(userName)
            .AzureAdRedirectWithAuthDetails(userName, password, authDomain);
        Console.ReadLine();
    });

app.Run();

using System.Net;
using OpenQA.Selenium;

namespace SeleniumAzureAdBasicAuth;

public static class AzureAdBasicAuthSteps
{
    public static IWebDriver GoToApplication(this IWebDriver driver, string url)
    {
        driver
            .Navigate()
            .GoToUrl(url);
        return driver;
    }

    public static IWebDriver ClickLogon(this IWebDriver driver, string loginId)
    {
        var loginButton = driver.FindElement(By.Id(loginId));
        loginButton.Click();
        return driver;
    }

    public static IWebDriver SetAzureAdLoginDetails(this IWebDriver driver, string userName, string adLoginButtonId = "idSIButton9", string userNameTextBoxId = "i0116")
    {
        var adLoginButton = driver.FindElement(By.Id(adLoginButtonId));
        var userNameTextBox = driver.FindElement(By.Id(userNameTextBoxId));
        userNameTextBox.SendKeys(userName);
        adLoginButton.Click();
        return driver;
    }

    public static IWebDriver AzureAdRedirectWithAuthDetails(this IWebDriver driver, string userName, string password, string domainToReplace)
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        var newUrlWithUserCredentials = driver.Url.Replace(domainToReplace, $"{WebUtility.HtmlEncode(userName)}:{password}@{domainToReplace}");
        driver.Navigate().GoToUrl(newUrlWithUserCredentials);
        return driver;
    }
}
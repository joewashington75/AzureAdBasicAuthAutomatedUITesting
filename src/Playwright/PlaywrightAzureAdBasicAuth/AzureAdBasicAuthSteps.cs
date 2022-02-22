using Microsoft.Playwright;

namespace PlaywightAzureAdBasicAuth;

public static class AzureAdBasicAuthSteps
{
    public static async Task<IPage> GoToApplicationAsync(this IPage page, string url)
    {
        await page.GotoAsync(url);
        return page;
    }

    public static async Task<IPage> ClickLogonAsync(this IPage page, string loginId)
    {
        var loginButton = page.Locator($"[id={loginId}]");
        await loginButton.ClickAsync();
        return page;
    }

    public static async Task<IPage> SetAzureAdLoginDetailsAsync(this IPage page, string userName, string adLoginButtonId = "idSIButton9", string userNameTextBoxId = "i0116")
    {
        var adLoginButton = page.Locator($"[id={adLoginButtonId}]");
        var userNameTextBox = page.Locator($"[id={userNameTextBoxId}]");
        await userNameTextBox.FillAsync(userName);
        await adLoginButton.ClickAsync();
        return page;
    }
}

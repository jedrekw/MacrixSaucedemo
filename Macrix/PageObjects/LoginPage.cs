using Macrix.Configuration;
using OpenQA.Selenium;

namespace Macrix.PageObjects;

public sealed class LoginPage
{
    private static readonly By LoginField = By.Id("user-name");
    private static readonly By PasswordField = By.Id("password");
    private static readonly By LoginSubmitButton = By.Id("login-button");
    private static readonly By PageHeader = By.CssSelector(".app_logo");
    private static readonly By LoginErrorContainer = By.CssSelector("*[data-test='error']");
    
    private static IWebDriver _webDriver;
    private static readonly ConfigSettings Config = Hooks.Hooks.config;

    public static void OpenBrowserOnHomepage()
    {
        var baseUrl = Config.BaseUrl;
        _webDriver = Mth.GetWebdriver();
        _webDriver.Navigate().GoToUrl(baseUrl);
        Mth.WaitUntilVisible(LoginField, 10);
    }

    public static void LogInWithStandardUser()
    {
        var username = Config.UsernameStd;
        var password = Config.Password;
       
        Mth.SendKeys(LoginField, username, 10);
        Mth.SendKeys(PasswordField, password, 10);
        Mth.Click(LoginSubmitButton, 10);
        Mth.WaitUntilVisible(PageHeader, 10);
    }
    
    public static void LogInWithLockedOutUser()
    {
        var username = Config.UsernameLocked;
        var password = Config.Password;
       
        Mth.SendKeys(LoginField, username, 10);
        Mth.SendKeys(PasswordField, password, 10);
        Mth.Click(LoginSubmitButton, 10);
    }

    public static void AssertLockedOutUserCantLogIn()
    {
        Mth.WaitUntilVisible(LoginErrorContainer, 10);
        Mth.AssertElementContains(LoginErrorContainer, "Epic sadface: Sorry, this user has been locked out.");
    }
}
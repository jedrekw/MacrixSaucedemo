using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
namespace Macrix.Configuration;

public sealed class Mth
{
    private static IWebDriver _webDriver;
    private const double DefaultTimeout = 10;
    
    public static IWebDriver GetWebdriver()
    {
        return _webDriver = Hooks.Hooks._container.Resolve<IWebDriver>();
    }

    public static IWebElement WaitUntilVisible(By locator, double timeout=DefaultTimeout)
    {
        var wait = new WebDriverWait(GetWebdriver(), TimeSpan.FromSeconds(timeout));
        return wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }
    
    public static void Click(By locator, double timeout=DefaultTimeout)
    {
        WaitUntilVisible(locator, timeout).Click();
    }
    
    public static void SendKeys(By locator, string text, double timeout=DefaultTimeout)
    {
        WaitUntilVisible(locator, timeout).Clear();
        WaitUntilVisible(locator, timeout).SendKeys(text);
    }
    
    public static void Select(By locator, String OptionText, double timeout=DefaultTimeout)
    {
        SelectElement dropDown = new SelectElement(GetWebdriver().FindElement(locator));
        dropDown.SelectByText(OptionText);
    }
    
    public static void AssertPageContains(string text)
    {
        Assert.IsTrue(GetWebdriver().FindElement(By.TagName("body")).Text.Contains(text));
    }
    
    public static void AssertPageContains(double text)
    {
        Assert.IsTrue(GetWebdriver().FindElement(By.TagName("body")).Text.Contains(text.ToString()));
    }
    
    public static void AssertElementContains(By locator, string text)
    {
        Assert.IsTrue(GetWebdriver().FindElement(locator).Text.Contains(text));
    }
    
    public static void Sleep(int timeout)
    {
        Thread.Sleep((timeout*1000));
    }
    
}
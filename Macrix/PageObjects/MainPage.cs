using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
namespace Macrix.Configuration;

[Binding]
public sealed class MainPage
{
    private static readonly By ItemNameElements = By.ClassName("inventory_item_name");
    private static readonly By GoToCartButton = By.CssSelector(".shopping_cart_link");
    private static readonly By CartContainer = By.CssSelector(".cart_list");

    public static double Element1Price;
    public static double Element2Price;
    public static double SumOfPurchases;
    public static readonly string ProductName1st = "Sauce Labs Bike Light";
    public static readonly string ProductName2nd = "Sauce Labs Bolt T-Shirt";
    public static void Add2Products()
    {
        IWebElement element1 = null;
        IWebElement element2 = null;
        ReadOnlyCollection<IWebElement> itemElements = Mth.GetWebdriver().FindElements(ItemNameElements);
        foreach (IWebElement element in itemElements)
        {
            if (element.Text == ProductName1st)
                element1 = element;
            if (element.Text == ProductName2nd) 
                element2 = element;
        }

        IWebElement element1AddToCart = element1.FindElement(By.XPath("./../../.."))
            .FindElement(By.XPath(".//button[text()='Add to cart']"));
        IWebElement element1PriceLoc = element1.FindElement(By.XPath("./../../.."))
            .FindElement(By.XPath(".//div[@Class= 'inventory_item_price']"));
        Element1Price = double.Parse(Regex.Match(element1PriceLoc.Text, @"[0-9]{0,3}(\.[0-9]{0,2})").Value, CultureInfo.InvariantCulture);

        IWebElement element2AddToCart = element2.FindElement(By.XPath("./../../.."))
            .FindElement(By.XPath(".//button[text()='Add to cart']"));
        IWebElement element2PriceLoc = element2.FindElement(By.XPath("./../../.."))
            .FindElement(By.XPath(".//div[@Class= 'inventory_item_price']"));
        Element2Price = double.Parse(Regex.Match(element2PriceLoc.Text, @"[0-9]{0,3}(\.[0-9]{0,2})").Value, CultureInfo.InvariantCulture);

        element1AddToCart.Click();
        element2AddToCart.Click();

        SumOfPurchases = Element1Price + Element2Price;
    }

    public static void GoToCart()
    {
        Mth.Click(GoToCartButton, 10);
        Mth.WaitUntilVisible(CartContainer, 10);
    }
}
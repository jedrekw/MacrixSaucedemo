using System.Collections.ObjectModel;
using Macrix.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Macrix.PageObjects;

public sealed class MainPage
{
    private static readonly By ItemNameElements = By.ClassName("inventory_item_name");
    private static readonly By GoToCartButton = By.CssSelector(".shopping_cart_link");
    private static readonly By CartContainer = By.CssSelector(".cart_list");
    private static readonly By SortingDropdown = By.ClassName("product_sort_container");

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
        Element1Price = Mth.ParsePrice(element1PriceLoc);

        IWebElement element2AddToCart = element2.FindElement(By.XPath("./../../.."))
            .FindElement(By.XPath(".//button[text()='Add to cart']"));
        IWebElement element2PriceLoc = element2.FindElement(By.XPath("./../../.."))
            .FindElement(By.XPath(".//div[@Class= 'inventory_item_price']"));
        Element2Price = Mth.ParsePrice(element2PriceLoc);

        element1AddToCart.Click();
        element2AddToCart.Click();

        SumOfPurchases = Element1Price + Element2Price;
    }

    public static void GoToCart()
    {
        Mth.Click(GoToCartButton, 10);
        Mth.WaitUntilVisible(CartContainer, 10);
    }
    
    public static void SelectSortingAZ()
    {
        Mth.Select(SortingDropdown,  "Name (A to Z)", 10);
    }
    
    public static void SelectSortingZA()
    {
        Mth.Select(SortingDropdown,  "Name (Z to A)", 10);
    }
    
    public static bool CheckIfProductsAreSortedByName(bool Descending)
    {
        List<string> ProductNames = new List<string>();
        ReadOnlyCollection<IWebElement> itemElements = Mth.GetWebdriver().FindElements(ItemNameElements);
        foreach (IWebElement element in itemElements)
        {
            ProductNames.Add(element.Text);
        }
        
        List<string> ProductNamesOriginal = ProductNames;
        ProductNames.Sort();
        if (Descending)
        {
            ProductNames.Reverse();
        }
        
        if (ProductNamesOriginal.SequenceEqual(ProductNames))
        {
            return true;
        }
        return false;
    }

    public static void CheckIfProductsAreSortedCorrectlyAscendingAZ()
    {
        Assert.True(CheckIfProductsAreSortedByName(false));
    }
    
    public static void CheckIfProductsAreSortedCorrectlyDescendingZA()
    {
        Assert.True(CheckIfProductsAreSortedByName(true));
    }
}
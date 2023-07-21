using Macrix.Configuration;
using OpenQA.Selenium;

namespace Macrix.PageObjects;

public sealed class CartPage
{
    private static readonly By CheckoutButton = By.Id("checkout");

    public static void CheckIfProductsWereAddedToCart()
    {
        Mth.AssertPageContains(MainPage.ProductName1st);
        Mth.AssertPageContains(MainPage.ProductName2nd);
        Mth.AssertPageContains(MainPage.Element1Price);
        Mth.AssertPageContains(MainPage.Element2Price);
    }

    public static void GoToCheckout()
    {
        Mth.Click(CheckoutButton, 10);
        Mth.WaitUntilVisible(CheckoutPage.CheckoutUserDataContainer, 10);
    }
    
}
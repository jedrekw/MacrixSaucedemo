using System.Globalization;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using NUnit.Framework;

namespace Macrix.Configuration;

public sealed class CheckoutPage
{
    private static readonly By CheckoutFirstNameField = By.Id("first-name");
    private static readonly By CheckoutLastNameField = By.Id("last-name");
    private static readonly By CheckouPostalCodeField = By.Id("postal-code");
    private static readonly By ContinueCheckoutButton = By.Id("continue");
    public static readonly By CheckoutUserDataContainer = By.ClassName("checkout_info");
    private static readonly By SummarySubtotalPriceLabel = By.CssSelector(".summary_subtotal_label");
    private static readonly By SummaryTaxLabel = By.CssSelector(".summary_tax_label");
    private static readonly By SummaryTotalPriceLabel = By.CssSelector(".summary_total_label");
    private static readonly By FinishCheckoutButton = By.Id("finish");
    private static readonly By CheckoutCompleteContainer = By.Id("checkout_complete_container");

    public static void FillTheCheckoutForm(string firstName, string lastName, string postalCode)
    {
        Mth.SendKeys(CheckoutFirstNameField, firstName, 10);
        Mth.SendKeys(CheckoutLastNameField, lastName, 10);
        Mth.SendKeys(CheckouPostalCodeField, postalCode, 10);
    }

    public static void ContinueCheckout()
    {
        Mth.Click(ContinueCheckoutButton,10);
    }
    
    public static void CheckIfSumOfPurchasesIsCorrect()
    {
        double SummarySubtotalPrice = double.Parse(Regex.Match(Mth.WaitUntilVisible(SummarySubtotalPriceLabel, 10).Text, @"[0-9]{0,3}(\.[0-9]{0,2})").Value, CultureInfo.InvariantCulture);
        Assert.AreEqual(SummarySubtotalPrice, MainPage.SumOfPurchases);
        
        double SummaryTaxValue = double.Parse(Regex.Match(Mth.WaitUntilVisible(SummaryTaxLabel, 10).Text, @"[0-9]{0,3}(\.[0-9]{0,2})").Value, CultureInfo.InvariantCulture);
        double SummaryTotalPrice = double.Parse(Regex.Match(Mth.WaitUntilVisible(SummaryTotalPriceLabel, 10).Text, @"[0-9]{0,3}(\.[0-9]{0,2})").Value, CultureInfo.InvariantCulture);
        
        double TotalPriceWithTax = Math.Round((SummarySubtotalPrice + SummaryTaxValue), 2, MidpointRounding.AwayFromZero);

        Assert.AreEqual(TotalPriceWithTax, SummaryTotalPrice);
    }
    
    public static void CheckIfProductsAreDisplayedOnConfirmationScreen()
    {
        Mth.AssertPageContains(MainPage.ProductName1st);
        Mth.AssertPageContains(MainPage.ProductName2nd);
        Mth.AssertPageContains(MainPage.Element1Price);
        Mth.AssertPageContains(MainPage.Element2Price);
    }

    public static void FinishCheckout()
    {
        Mth.Click(FinishCheckoutButton, 10);
        Mth.WaitUntilVisible(CheckoutCompleteContainer, 10);
    }
    
    public static void CheckIfCheckoutSuccessfullyProcessed()
    {
        Mth.AssertPageContains("Checkout: Complete!");
        Mth.AssertPageContains("Thank you for your order!");
        Mth.AssertPageContains("Your order has been dispatched, and will arrive just as fast as the pony can get there!");
    }
    
}
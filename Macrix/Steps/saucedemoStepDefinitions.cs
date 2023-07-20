namespace Macrix.Steps;
 using Macrix.Configuration;
 using TechTalk.SpecFlow;


[Binding]
public sealed class StepDefinitions
{
    
    [Given("User opens browser on homepage")]
   public void GivenOpenBrowserOnHomepage()
   {
        LoginPage.OpenBrowserOnHomepage();
   }

   [Given(@"Log in with Standard User")]
   public void GivenLogsInWithStandardUser()
   {
       LoginPage.LogInWithStandardUser();
   }
   
   [When(@"Add two products to cart")]
   public void WhenAdds2Products()
   {
       MainPage.Add2Products();
   }
   
   [When(@"Go to cart")]
   public void WhenGoesToCart()
   {
       MainPage.GoToCart();
   }
   
   [Then(@"The products should be added to cart")]
   public void ThenChecksIfProductsWereAdded()
   {
       CartPage.CheckIfProductsWereAddedToCart();
   }
   
   [When(@"Go To Checkout")]
   public void WhenGoesToCheckout()
   {
       CartPage.GoToCheckout();
   }
   
   [When(@"Fill The Checkout Form And Proceed")]
   public void WhenFillTheCheckoutFormAndProceed()
   {
       CheckoutPage.FillTheCheckoutForm("Jedrzej", "Wojcieszczyk", "54-616");
       CheckoutPage.ContinueCheckout();
   }
   
   [Then(@"Sum of purchases should be correct")]
   public void ThenSumOfPurchasesShouldBeCorrect()
   {
       CheckoutPage.CheckIfSumOfPurchasesIsCorrect();
   }
   
   [Then(@"Products should be displayed on confirmation screen")]
   public void ThenProductsAreDisplayedOnConfirmationScreen()
   {
       CheckoutPage.CheckIfProductsAreDisplayedOnConfirmationScreen();
   }
   
   [When(@"Finish Checkout")]
   public void WhenUserFInishesCheckout()
   {
      CheckoutPage.FinishCheckout();
   }
      
    [Then(@"The Checkout should be Successfully Processed")]
    public void ThenCheckIfCheckoutSuccessfullyProcessed()
   {
      CheckoutPage.CheckIfCheckoutSuccessfullyProcessed();
   }
}

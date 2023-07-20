Feature: Saucedemo test cases
	
@Smoke
Scenario: Log in and buy 2 products
	Given User opens browser on homepage
	And Log in with Standard User
	When Add two products to cart
	And Go to cart
	Then The products should be added to cart
	When Go To Checkout
	And Fill The Checkout Form And Proceed
	Then Sum of purchases should be correct
	And Products should be displayed on confirmation screen
	When Finish Checkout
	Then The Checkout should be Successfully Processed
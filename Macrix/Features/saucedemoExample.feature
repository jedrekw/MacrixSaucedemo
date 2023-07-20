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
	
Scenario: Check Locked-out user can't login
	Given User opens browser on homepage
	When Log in with Locked-out User
	Then Assert Locked-out user can't login
	
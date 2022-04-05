Feature: Cart

This will Test cart functionalities

@Cart
Scenario: Add Item To Cart
	Given Given I Navigate to the Home page
	When I click Add <product> to Cart
		| product    | 
		| Toothbrush |
	When I click to open the cart
	Then The product should be added to Cart
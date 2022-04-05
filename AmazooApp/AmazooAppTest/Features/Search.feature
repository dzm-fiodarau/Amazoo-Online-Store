Feature: Search

This will test The search feature

@SearchTest
Scenario: Searching for a valid product
	Given I Navigate at the Home page
	When I enter a valid <ProductName> in the search bar
		| ProductName  | 
		| jeans		   |
	And I click the Search button
	Then I should see a Page with the matching products

@SearchTest
Scenario: Search for an Invalid product
	Given I Navigate at the Home page
	When I enter a valid <ProductName> in the search bar
		| ProductName  | 
		| hsgdh		   |
	And I click the Search button
	Then I should get "There is no available products at the moment."

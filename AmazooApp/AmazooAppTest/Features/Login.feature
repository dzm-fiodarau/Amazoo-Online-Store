Feature: Login
	Login to AmazooApp
@LoginTest
Scenario: Logging in with valid credentials
	Given I am at the Home page
	And I Navigate to the Login page
	And enter the following details
		| Email   | Password |
		| a@a.com | 1qazxsW@ |
	And I click the login button 
	Then I should navigate the home page

@LoginTest
Scenario: Logging in with invalid credentials
	Given I am the Home Page
	And I Go to the Login page
	When I enter <Email> and <Password>
		| Email   | Password |
		| test@gamil.com | password |
		#| Dimztry@gmail.com | engRules123 |
		#| kirat@gmail.com | compsci |
	And I click the login button 
	Then I should Navigate the Login Page
	

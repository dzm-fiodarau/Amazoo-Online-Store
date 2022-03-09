Feature: Login
	Login to AmazooApp
@ignore
Scenario: AmazooApp should navigate to the login page given that user is not logged in
	Given I launch the Application
	And I click login link
	And enter the following details
		| Email   | Password |
		| a@a.com | 1qazxsW@ |
	And I click the login button 
	Then I should see the home page

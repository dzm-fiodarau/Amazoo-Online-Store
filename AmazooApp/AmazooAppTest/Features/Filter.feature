Feature: Filter

This Test group will focus on Filter fucntions and 
make sure there is no regression in the future

@FilterTest
Scenario: Filter Products by Type
	Given I am at the Home Page
	When A user click on clothing
	And i click on ApplyFIlters button
	Then products should be filtered

@FilterTest
Scenario: Filter Products by Brand
	Given I am at the Home Page
	When A user click on Nintendo
	And i click on ApplyFIlters button
	Then products count will be <count>
	Examples: 
	| count |
	| 1     |

@FilterTest
Scenario: Filter Products by Brand and Type
	Given I am at the Home Page
	When A user click on Nintendo
	And A user click on clothing
	And i click on ApplyFIlters button
	Then products count will be <count>
	Examples: 
	| count |
	| 4     |

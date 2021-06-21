Feature: RegisterSale
	As owner i want to register a sale

@tag1
Scenario: As owner i want to register a sale
	Given the owner wants to register on sale endpoint
	When owner register a new sale
	| DateAndTime                       | ClientFullName |
	| 2021-06-25T17:48:58.2811058-05:00 | Lucero Alba    | 
	Then I register my sale successfully
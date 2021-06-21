Feature: AddDetailsToSale
	As owner i want to add sale details

@tag1
Scenario: As owner i want to add sale details
	Given the owner wants to register on sale detail endpoint
	When owner register a new sale detail
	| Description    | Quantity |
	| "Extra Salad"  |    2     | 
	Then I register my sale detail successfully
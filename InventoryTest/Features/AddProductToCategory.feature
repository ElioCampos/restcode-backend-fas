Feature: AddProductToCategory
	As owner I want to add a new product for my restaurant in a specific category

@mytag
Scenario: Add new product to category
	Given the owner wants to add on product endpoint 
	When owner add a new product
	| Name             | Price |
	| Pollo a la brasa |  20   |
	Then the product will be added succesfully
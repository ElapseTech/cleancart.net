Feature: Add items to the shop (SH3)
	As a shop administrator,
	I want to be able to add items to the shop,
	In order to make new items available to my customers

Scenario: Add an item
	Given A shop catalog
	When I add a new item 
	Then the item is added to the catalog

Scenario: Add multiple items
	Given A shop catalog
	When I add a new item
	Then I can add another item

Scenario: A title is mandatory
	Given A shop catalog
	When I add a new item with no title
	Then an error is reported

Scenario: An item code is mandatory
	Given A shop catalog
	When I add a new item with no item code
	Then an error is reported

Scenario: The item code must be unique
	Given A shop catalog
	And an item with code 'I1' in the catalog
	When I add a new item with the code 'I1'
Feature: Add items to the shop (SH3)
	As a shop administrator,
	I want to be able to add items to the shop,
	In order to make new items available to my customers

Scenario: Add an item
	Given A shop catalog
	When I add a new item with the title 'My new item'
	Then the item is added to the catalog

Scenario: Add multiple items
	Given A shop catalog
	When I add a new item
	Then I can add another item
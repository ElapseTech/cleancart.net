﻿Feature: Add items to the shop (SH3)
	As a shop administrator,
	I want to be able to add items to the shop,
	In order to make new items available to my customers

@Web
Scenario: Add an item
	Given The shop catalog page
	When I add a new item 
	Then the item is shown in the catalog

@Web
Scenario: Add multiple items
	Given The add item catalog form
	When I add a new item
	Then I can add another item

@Web
Scenario: Errors are shown
	Given The add item catalog form
	When I an error occurs
	Then the error is shown

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
	When I add a new item with same code 'I1'
	Then an error is reported
	
Scenario: The item title must be unique
	Given A shop catalog
	And an item with the title 'The Title' in the catalog
	When I add a new item with the same title 'The Title'
	Then an error is reported

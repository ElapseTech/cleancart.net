Feature: Display the shop catalog (SH1)
	As a customer
	I want to be able to browse the catalog
	In order to select which items I want to buy

Scenario: All items are present in the shop catalog
	Given A shop catalog
	And Some items in the catalog
	When I visit the catalog
	Then all items' title are present

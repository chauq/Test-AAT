@Test
Feature: Test
	Site functional testing

@Desktop
@Mobile
@TestScenario
Scenario: Test Scenario
	Given Homepage has launched
	Then Validate H2 page displayed 'Magic Mouse'

Scenario: Test All Product page
	Given Homepage has launched
	When Click on link 'All Product'
	Then Validate H1 page displayed 'Product Category'
	Then Validate H3 page displayed 'Meta'
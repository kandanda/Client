Feature: Participants
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
    Given I launch the application

@mytag
Scenario: Add new participant
	Given I switch to Particpants tab
	And I have added a new participant
	When I press save
	Then number of participants should increase

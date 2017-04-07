Feature: Participants
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
    Given The application is running
	And I switch to Particpants tab

Scenario: Add new participant
	And I have added a new participant
	When I press save
	Then number of participants should increase

Feature: Participants
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
    Given The application is running
	And The test database is loaded
	And I switch to Particpants tab

Scenario: See participants overview
	Then I see all participants

Scenario: Add new participant
	And I have added a new participant
	When I press save
	Then The number of participants should increase

Scenario: Delete a participant
	And I have selected the first participant in the list
	When I press delete
	Then The number of participants should decrease

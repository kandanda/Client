Feature: Participants
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
    Given The application is running
	And The test database is loaded
	And I switch to Particpants tab

Scenario: Add participants
	When I add add these participants
		| name            | captain        | phone            | email            |
		| FC Superkickers | Sven Hannewald | +41 55 123 45 67 | sven.h@hanne.ch  |
		| FC ShellShock   | Hackie McHack  | +41 44 876 65 55 | hacks@hackies.ch |  

Scenario: See participants overview
	Then I see all participants

Scenario: Search for "Real Madrid"
	And I enter "Real Madrid" into the participants search box
	Then I should see "Real Madrid" in the list of participants

Scenario: Add new participant
	And I have added a new participant
	When I press save
	Then The number of participants should increase

Scenario: Delete a participant
	And I have selected the first participant in the list
	When I press delete
	Then The number of participants should decrease

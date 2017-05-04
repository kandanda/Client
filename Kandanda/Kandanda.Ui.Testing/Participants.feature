Feature: Participants
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
    Given The application is running
	And The test database is loaded
	And I switch to Particpants tab

Scenario: Add participant
	When I add add this participant
		| Name            | Captain        | Phone            | Email            |
		| FC ShellShock   | Hackie McHack  | +41 44 876 65 55 | hacks@hackies.ch |  
	When I press save participants
	Then I should see "FC ShellShock" in the list of teams

Scenario: See participants overview
	Then I see all participants

Scenario: Search for "Real Madrid"
	And I enter "Real Madrid" into the participants search box
	Then I should see "FC Real Madrid" in the list of teams
	

Scenario: Delete a participant
	And I have selected the first participant in the list
	When I press delete
	Then The number of participants should decrease

Feature: Participants
	Teams are sort of a database of available teams 
	to participate in a tournament.

Background: 
    Given The application is running
	And The test database is loaded
	And I switch to Participants tab

Scenario: See existing teams
	Then I see 7 participants

Scenario: Add a new team
	When I have added this participant
		| Name          | Captain        | Phone            | Email            |
		| FC ShellShock | Hackie McHack  | +41 44 876 65 55 | hacks@hackies.ch |
		| FC Rabbits    | Bunny the hopp | +41 44 927 28 26 | bunny@rabbits.ch |  
	And I press save participants
	Then I see 9 participants

Scenario: Search for a team
	When I enter "Real Madrid" into the participants search box
	Then I should see "FC Real Madrid" in the list of teams
	

Scenario: Delete an existing team
	Given I have selected the first participant in the list
	When I press delete
	Then I see 6 participants

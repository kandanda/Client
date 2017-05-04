Feature: Tournaments
	It should be possible to create tournaments aswell as edit
	exisiting tournaments as desired. 

Background: 
    Given The application is running
	And The test database is loaded
	And I switch to Tournaments tab

Scenario: See existing tournaments
	Then I see some tournaments

Scenario: Add new tournament
	Given I press New Tournament
	And I enter the tournament information:
		| tournamentName     | NumberOfGroups | participantsPerGroup | GameType | KOType | DetermineThird |
		| My test tournament | 2              | 4                    |          |        | no             |
	When I press save
	Then I see My test tournament in tournament name

Scenario: Remove tournament
	And I select the first tournament
	When I press delete
	Then The number of tournaments should decrease

Scenario: Edit tournament name
	And I open the last tournament
	And I have entered changed tournament into the tournament Name
	When I press save
	Then The name of the last tournament shoud be changed tournament

Scenario: Add Real Madrid to the Tournament
	And I open the last tournament
	And I switch to Participants tab
	When I doubleclick on Real Madrid within the available participants grid
	Then Real Madrid is now in the list of participants

Scenario: Search for participants
	Given I open the last tournament
	And I switch to Participants tab
	When I enter "Real Madrid" into the search box
	Then I should see "Real Madrid" in the list of participants

Scenario: Setup a full tournament
	Given I add a new Tournament:
		| tournamentName     | NumberOfGroups | participantsPerGroup | GameType | KOType | DetermineThird |
		| My test tournament | 2              | 4                    |          |        | no             |
	And I switch to Shedules tab
	And I add these schedule information:
		| beginOn    | endOn      | startTime | endTime | duration | break    | lunchStart | lunchEnd | finalsBegin      | matchOn |
		| 01.07.2017 | 02.07.2017 | 10:00     | 17:00   | 00:15:00 | 00:05:00 | 12:00      | 13:00    | 02.07.2017 15:00 | 0000011 |
	And I switch to Participants tab
	And I double click Real Madrid in the data grid
	When I press publish
	Then I should see an URI of the tournament
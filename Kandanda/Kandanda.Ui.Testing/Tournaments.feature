Feature: Tournaments
	It should be possible to create tournaments aswell as edit
	exisiting tournaments as desired. 

Background: 
    Given The application is running
	And The test database is loaded
	And I switch to Tournaments tab

Scenario: See existing tournaments
	Then I should see 5 tournaments

Scenario: Add new tournament
	And I added this tournament
		| Key            | Value              |
		| Name           | My test tournament |
		| Participants   | 4                  |
		| GameType       |                    |
		| KOType         |                    |
		| DetermineThird | no                 |
	And I switch to Tournament schedule tab
	And I added this schedule information:
		| Key               | Value            |
		| From              | 01.07.2017       |
		| Until             | 02.07.2017       |
		| PlayTimeStart     | 10:00            |
		| PlayTimeEnd       | 17:00            |
		| GameDuration      | 00:15:00         |
		| BreakBetweenGames | 00:05:00         |
		| LunchBreakStart   | 12:00            |
		| LunchBreakEnd     | 13:00            |
		| finalsBegin       | 02.07.2017 15:00 |
		| Saturday          | true             |
		| Sunday            | true             |
	When I press close tournament
	Then I should see 6 tournaments
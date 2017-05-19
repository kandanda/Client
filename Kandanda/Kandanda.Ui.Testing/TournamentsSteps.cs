using System.Linq;
using Kandanda.Dal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.WindowItems;

namespace Kandanda.Ui.Testing
{
    [Binding]
    public class TournamentsSteps
    {
        private readonly Application _application;
        private readonly Window _window;

        public TournamentsSteps()
        {
            _application = FeatureContext.Current.Get<Application>("app");
            _window = _application.GetWindows().First();
        }

        [Given(@"I switch to Tournaments tab")]
        public void GivenISwitchToTournamentsTab()
        {
            var tabpage = _window.Get<TabPage>(AutomationIds.MainViewTournamentsTab);
            tabpage.Select();
        }

        [Given(@"I switch to Tournament information tab")]
        public void GivenISwitchToInformationTab()
        {
            var tabpage = _window.Get<TabPage>(AutomationIds.TournamentInformationTab);
            tabpage.Select();
        }

        [Given(@"I switch to Tournament schedule tab")]
        public void GivenISwitchToScheduleTab()
        {
            var tabpage = _window.Get<TabPage>(AutomationIds.TournamentScheduleTab);
            tabpage.Select();
        }

        [Given(@"I switch to Tournament participants tab")]
        public void GivenISwitchToParticipantsTab()
        {
            var tabpage = _window.Get<TabPage>(AutomationIds.TournamentParticipantsTab);
            tabpage.Select();
        }

        [Then(@"I should see (.*) tournaments")]
        public void ThenIShouldSeeTournaments(int p0)
        {
            var datagrid = _window.Get<ListView>(AutomationIds.TournamentsDataGrid);
            Assert.AreEqual(p0, datagrid.Rows.Count);
        }
        [Given(@"I created a new tournament")]
        public void GivenICreatedANewTournament()
        {
            _window.Get<Button>(AutomationIds.TournamentsNewTournamentButton).Click();
        }

        [Given(@"I entered this tournament information")]
        public void GivenIEnteredThisTournament(Table table)
        {
            var tournament = table.CreateInstance<Tournament>();
            var datepickeritems = _window.GetMultiple(SearchCriteria.ByAutomationId("AutoSelectTextBox"));


            _window.Get<TextBox>(AutomationIds.TournamentInfoName).Text = tournament.Name;

            var textBox = datepickeritems[0] as TextBox;
            if (textBox != null)
                textBox.BulkText = $"{tournament.GroupSize}";

            _window.Get<CheckBox>(AutomationIds.TournamentInfoDetermineThird).SetValue(tournament.DetermineThird);
        }

        [Given(@"I entered this schedule information")]
        public void GivenIEnteredThisScheduleInformation(Table table)
        {
            var tournament = table.CreateInstance<Tournament>();
            var datepickeritems = _window.GetMultiple(SearchCriteria.ByAutomationId("AutoSelectTextBox"));

            var textBox = datepickeritems[0] as TextBox;
            if (textBox != null)
                textBox.BulkText = tournament.From.ToString("dd.MM.yyyy HH:mm");
            
            textBox = datepickeritems[1] as TextBox;
            if (textBox != null)
                    textBox.BulkText = tournament.Until.ToString("dd.MM.yyyy HH:mm");

            textBox = datepickeritems[2] as TextBox;
            if (textBox != null)
                textBox.BulkText = $"{tournament.PlayTimeStart.Hours}:{tournament.PlayTimeStart.Minutes}";

            textBox = datepickeritems[3] as TextBox;
            if (textBox != null)
                textBox.BulkText = $"{tournament.PlayTimeEnd.Hours}:{tournament.PlayTimeEnd.Minutes}";

            textBox = datepickeritems[4] as TextBox;
            if (textBox != null)
                textBox.BulkText = $"{tournament.GameDuration.TotalMinutes}";

            textBox = datepickeritems[5] as TextBox;
            if (textBox != null)
                textBox.BulkText = $"{tournament.BreakBetweenGames.TotalMinutes}";

            textBox = datepickeritems[6] as TextBox;
            if (textBox != null)
                textBox.BulkText = $"{tournament.LunchBreakStart.Hours}:{tournament.LunchBreakStart.Minutes}";

            textBox = datepickeritems[7] as TextBox;
            if (textBox != null)
                textBox.BulkText = $"{tournament.LunchBreakEnd.Hours}:{tournament.LunchBreakEnd.Minutes}";

            textBox = datepickeritems[8] as TextBox;
            if (textBox != null)
                textBox.BulkText = tournament.FinalsFrom.ToString("dd.MM.yyyy HH:mm");

            _window.Get<CheckBox>(AutomationIds.TournamentScheduleOnMonday).SetValue(tournament.Monday);
            _window.Get<CheckBox>(AutomationIds.TournamentScheduleOnTuesday).SetValue(tournament.Tuesday);
            _window.Get<CheckBox>(AutomationIds.TournamentScheduleOnWednesday).SetValue(tournament.Wednesday);
            _window.Get<CheckBox>(AutomationIds.TournamentScheduleOnThursday).SetValue(tournament.Thursday);
            _window.Get<CheckBox>(AutomationIds.TournamentScheduleOnFriday).SetValue(tournament.Friday);
            _window.Get<CheckBox>(AutomationIds.TournamentScheduleOnSaturday).SetValue(tournament.Saturday);
            _window.Get<CheckBox>(AutomationIds.TournamentScheduleOnSunday).SetValue(tournament.Sunday);
        }

        [When(@"I press publish tournament")]
        public void WhenIPressPublishTournament()
        {
            _window.Get<Button>(AutomationIds.TournamentGenerateButton).Click();
        }

        [When(@"I press close tournament")]
        public void WhenIPressCloseTournament()
        {
            _window.Get<Button>(AutomationIds.TournamentCloseButton).Click();
            _application.GetWindows()[1].Get<Button>(SearchCriteria.ByText("OK")).Click();
        }

        [Given(@"I have selected the first tournament in the list")]
        public void GivenIHaveSelectedTheFirstTournamentInTheList()
        {
            _window.Get<ListView>(AutomationIds.TournamentsDataGrid).Focus();
            _window.Get<ListView>(AutomationIds.TournamentsDataGrid).Rows[1].Focus();
            _window.Get<ListView>(AutomationIds.TournamentsDataGrid).Rows[1].Select();
            _window.Get<ListView>(AutomationIds.TournamentsDataGrid).Rows[1].Cells[0].Focus();
        }

        [When(@"I press delete tournament")]
        public void WhenIPressDeleteTournament()
        {
            _window.Get<Button>(AutomationIds.TournamentsDeleteTournamentButton).Click();
        }



    }
}

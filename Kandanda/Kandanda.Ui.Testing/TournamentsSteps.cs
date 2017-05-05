using System.Linq;
using Kandanda.Dal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestStack.White;
using TestStack.White.UIItems;
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

        [Given(@"The application is running")]
        public void GivenTheApplicationIsRunning()
        {
            Assert.IsNotNull(_application);
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
            var tabpage = _window.Get<TabPage>(AutomationIds.TITab);
            tabpage.Select();
        }

        [Given(@"I switch to Tournament schedule tab")]
        public void GivenISwitchToScheduleTab()
        {
            var tabpage = _window.Get<TabPage>(AutomationIds.TSTab);
            tabpage.Select();
        }

        [Given(@"I switch to Tournament participants tab")]
        public void GivenISwitchToParticipantsTab()
        {
            var tabpage = _window.Get<TabPage>(AutomationIds.TPTab);
            tabpage.Select();
        }

        [Then(@"I should see (.*) tournaments")]
        public void ThenIShouldSeeTournaments(int p0)
        {
            var datagrid = _window.Get<ListView>(AutomationIds.TournamentsList);
            Assert.AreEqual(p0, datagrid.Rows.Count);
        }

        [Given(@"I added this tournament")]
        public void GivenIAddedThisTournament(Table table)
        {
            var addNewButton = _window.Get<Button>(AutomationIds.TournamentsNewTournamentButton);

            var tournament = table.CreateInstance<Tournament>();

            _window.Get<TextBox>(AutomationIds.TIName).Text = tournament.Name;
            _window.Get<TextBox>(AutomationIds.TINoPpg).Text = tournament.Name;
            _window.Get<TextBox>(AutomationIds.TIGt).Text = tournament.Name;
            _window.Get<TextBox>(AutomationIds.TIKoT).Text = tournament.Name;
            _window.Get<TextBox>(AutomationIds.TIDtrd).Text = tournament.Name;


        }

        [Given(@"I added this schedule information:")]
        public void GivenIAddedThisScheduleInformation(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I press publish tournament")]
        public void WhenIPressPublishTournament()
        {
            _window.Get<Button>(AutomationIds.TPublishButton).Click();
        }

        [When(@"I press close tournament")]
        public void WhenIPressCloseTournament()
        {
            _window.Get<Button>(AutomationIds.TCloseButton).Click();
        }


    }
}

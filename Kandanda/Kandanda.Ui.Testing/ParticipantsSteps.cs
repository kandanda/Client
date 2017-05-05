using System;
using System.Linq;
using Kandanda.Dal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.WindowsAPI;

namespace Kandanda.Ui.Testing
{
    [Binding]
    public class ParticipantsSteps
    {
        private readonly Application _application;
        private readonly Window _window;

        public ParticipantsSteps()
        {
            _application = FeatureContext.Current.Get<Application>("app");
            _window = _application.GetWindows().First();
        }

        [Given(@"The application is running")]
        public void GivenTheApplicationIsRunning()
        {
            Assert.IsNotNull(_application);
        }


        [Given(@"I switch to Participants tab")]
        public void GivenISwitchToParticpantsTab()
        {
            var tabpage = _window.Get<TabPage>(AutomationIds.MainViewParticipantsTab);
            tabpage.Select();
        }
        
        [When(@"I enter ""(.*)"" into the participants search box")]
        public void WhenIEnterIntoTheParticipantsSearchBox(string p0)
        {
            var searchbox = _window.Get<TextBox>(AutomationIds.ParticipantsSearchBox);
            searchbox.Text = p0;
        }


        [Then(@"I should see ""(.*)"" in the list of teams")]
        public void ThenIShouldSeeInTheListOfTeams(string p0)
        {
            var datagrid = _window.Get<ListView>(AutomationIds.ParticipantsDataGrid);
            Assert.AreEqual(p0, datagrid.Rows[0].Cells[0].Text);
        }
        [Given(@"I see (.*) participants")]
        public void GivenISeeParticipants(int p0)
        {
            var datagrid = _window.Get<ListView>(AutomationIds.ParticipantsDataGrid);
            Assert.AreEqual(p0, datagrid.Rows.Count);
        }

        [Then(@"I see (.*) participants")]
        public void ThenISeeParticipants(int p0)
        {
            var datagrid = _window.Get<ListView>(AutomationIds.ParticipantsDataGrid);
            Assert.AreEqual(p0, datagrid.Rows.Count);
        }

        [When(@"I have added this participant")]
        public void WhenIHaveAddedThisParticipant(Table table)
        {
            var addNewButton = _window.Get<Button>(AutomationIds.ParticipantsAddNewButton);
            var datagrid = _window.Get<ListView>(AutomationIds.ParticipantsDataGrid);

            var participants = table.CreateSet<Participant>();

            foreach (var participant in participants)
            {
                addNewButton.Click();
                var lastRow = datagrid.Rows[datagrid.Rows.Count - 1];

                lastRow.Cells[0].Click();
                lastRow.Cells[0].Enter(participant.Name);
                lastRow.Cells[0].Click();
                lastRow.Cells[1].Enter(participant.Captain);
                lastRow.Cells[1].Click();
                lastRow.Cells[2].Enter(participant.Phone);
                lastRow.Cells[2].Click();
                lastRow.Cells[3].Enter(participant.Email);
            }
        }

        [When(@"I press save participants")]
        public void WhenIPressSaveParticipants()
        {
            var savebutton = _window.Get<Button>(AutomationIds.ParticipantsSaveButton);
            savebutton.Click();
        }

        [Given(@"The test database is loaded")]
        public void GivenTheTestDatabaseIsLoaded()
        {
            _window.Get<Menu>(AutomationIds.MenuResetDatabase).Click();
        }

        [Given(@"I have selected the first participant in the list")]
        public void GivenIHaveSelectedTheFirstParticipantInTheList()
        {
            _window.Get<ListView>(AutomationIds.ParticipantsDataGrid).Rows[0].Select();
        }


        [When(@"I press delete")]
        public void WhenIPressDelete()
        {
            _window.Get<Button>(AutomationIds.ParticipantsDeleteButton).Click();
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Kandanda.Ui;
using Kandanda.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.WindowItems;
using Table = TestStack.White.UIItems.TableItems.Table;

namespace Kandanda.Specs
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
            if (_application == null)
            {
                ScenarioContext.Current.Pending();
            }

        }



        [Given(@"I switch to (.*) tab")]
        public void GivenISwitchToParticpantsTab(string tabName)
        {
            var tabpage = _window.Get<TabPage>(SearchCriteria.ByText(tabName));
            tabpage.Select();
        }

        [Given(@"I enter ""(.*)"" into the participants search box")]
        public void GivenIEnterIntoTheParticipantsSearchBox(string p0)
        {
            var searchbox = _window.Get<TextBox>(AutomationIds.ParticipantsSearchBox);
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have added a new participant")]
        public void GivenIHaveAddedANewParticipant()
        {
            /*
            Console.WriteLine("Add a new participant");
            var participantsGrid = _window.Get<Table>("ParticipantsGrid");
            var rows = participantsGrid.Rows;
            var numberofrows = participantsGrid.Rows.Count();

            
            var dataGrid = page.Get<ListView>(AutomationIds.MyDataGridId);
            distributionGrid.Rows[0].Cells[0].Value = firstName;
            var cell = dataGrid.Rows[0].Cells[1];
            cell.Click();
            cell.Enter("New Value");
            dataGrid.Select(1); // lose focus
            var datagrid = Window.Get<ListView>(AutomationIds.MyDataGridId);
            */
            ScenarioContext.Current.Pending();

        }

        [When(@"I press save")]
        public void WhenIPressSave()
        {
            var button = _window.Get<Button>("SaveButton");
            button.Click();
        }

        [Then(@"number of participants should increase")]
        public void ThenNumberOfParticipantsShouldIncrease()
        {
            Console.WriteLine("Test the result");
            //var participantsGrid = _window.Get<Table>("ParticipantsGrid");
            //var rows = participantsGrid.Rows;
            //var numberofrows = participantsGrid.Rows.Count();
            //ScenarioContext.Current.Pending();
        }

        [Given(@"The test database is loaded")]
        public void GivenTheTestDatabaseIsLoaded()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I see all participants")]
        public void ThenISeeAllParticipants()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The number of participants should increase")]
        public void ThenTheNumberOfParticipantsShouldIncrease()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have selected the first participant in the list")]
        public void GivenIHaveSelectedTheFirstParticipantInTheList()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I press delete")]
        public void WhenIPressDelete()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The number of participants should decrease")]
        public void ThenTheNumberOfParticipantsShouldDecrease()
        {
            ScenarioContext.Current.Pending();
        }
        [When(@"I add add these participants")]
        public void WhenIAddAddTheseParticipants(Table table)
        {
            ScenarioContext.Current.Pending();
        }





    }
}

﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.WindowItems;
using Table = TestStack.White.UIItems.TableItems.Table;

namespace Kandanda.Integration.Testing
{
    [Binding]
    public class ParticipantsSteps
    {
        public string BaseDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //TODO: get assembly name by $"{nameof(Kandanda.Ui)}.exe" or similar
        public string SutPath => Path.Combine(BaseDir, "Kandanda.Ui.exe");
        private Application _app;
        private Window _window;

        [Given(@"I launch the application")]
        public void GivenILaunchTheApplication()
        {
            _app = Application.Launch(SutPath);
            _window = _app.GetWindows().First();
        }


        [Given(@"I switch to Particpants tab")]
        public void GivenISwitchToParticpantsTab()
        {
            var tabpage = _window.Get<TabPage>("Participants");
            tabpage.Select();
            //ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have added a new participant")]
        public void GivenIHaveAddedANewParticipant()
        {
            var participantsGrid = _window.Get<Table>("ParticipantsGrid");
            var rows = participantsGrid.Rows;
            var numberofrows = participantsGrid.Rows.Count();

            /*
            var dataGrid = page.Get<ListView>(AutomationIds.MyDataGridId);
            distributionGrid.Rows[0].Cells[0].Value = firstName;
            var cell = dataGrid.Rows[0].Cells[1];
            cell.Click();
            cell.Enter("New Value");
            dataGrid.Select(1); // lose focus
            var datagrid = Window.Get<ListView>(AutomationIds.MyDataGridId)
            ScenarioContext.Current.Pending();
            */
        }
        
        [When(@"I press save")]
        public void WhenIPressSave()
        {
            var button = _window.Get<Button>("SaveButton");
            button.Click();
            //ScenarioContext.Current.Pending();
        }
        
        [Then(@"number of participants should increase")]
        public void ThenNumberOfParticipantsShouldIncrease()
        {
            var participantsGrid = _window.Get<Table>("ParticipantsGrid");
            var rows = participantsGrid.Rows;
            var numberofrows = participantsGrid.Rows.Count();
            //ScenarioContext.Current.Pending();
        }
    }
}

using MedicalRecordAutomation.Hooks;
using Microsoft.Data.SqlClient;
using Microsoft.Playwright;
using Reqnroll;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MedicalRecordAutomation.StepDefinitions
{
    [Binding]
    public class PatientStepDefinitions
    {
        private IPage _page;
        private AutomationHooks _hooks;
        private static string actualAlertText;
        public PatientStepDefinitions(AutomationHooks hooks)
        {
            _page = hooks.PageInstance;
            _hooks= hooks;
        }

        [When("I click on patient menu")]
        public async Task WhenIClickOnPatientMenu()
        {
            await _page.Locator("xpath=//div[text()='Patient']").ClickAsync();
        }

        [When("I click on New-search menu")]
        public async Task WhenIClickOnNew_SearchMenuAsync()
        {
            await _page.Locator("xpath=//div[text()='New/Search']").ClickAsync();
        }

        [When("I fill the new patient form")]
        public async Task WhenIFillTheNewPatientForm(DataTable dataTable)
        {
            _hooks.ScenarioContextInstance.Add("empDataTable", dataTable);

            Console.WriteLine(dataTable.Rows[0][0]);
            Console.WriteLine(dataTable.Rows[0]["firstname"]);
            Console.WriteLine(dataTable.Rows[0]["middlename"]);
            Console.WriteLine(dataTable.Rows[0]["lastname"]);
            Console.WriteLine(dataTable.Rows[0]["gender"]);
            Console.WriteLine(dataTable.Rows[0]["DOB"]);


            var frame = _page.FrameLocator("xpath=//iframe[@name='pat']");
            await frame.Locator("xpath=//input[@id='form_fname']").FillAsync(dataTable.Rows[0]["firstname"]);

        }

        [When("I click on create new patient")]
        public void WhenIClickOnCreateNewPatient()
        {
            
        }

        [When("I click on confirm create new patient")]
        public void WhenIClickOnConfirmCreateNewPatient()
        {
            
        }

        [When("I store message and handle the alert box")]
        public void WhenIStoreMessageAndHandleTheAlertBox()
        {
            actualAlertText = "alert is tobacco";
        }

        [When("I close the birthday popup if available")]
        public void WhenICloseTheBirthdayPopupIfAvailable()
        {
            
        }

        [Then("I should verify the added name as {string}")]
        public void ThenIShouldVerifyTheAddedNameAs(string p0)
        {
            
        }

        [Then("I also the alert message should contains {string}")]
        public void ThenIAlsoTheAlertMessageShouldContains(string tobacco)
        {
            _hooks.ScenarioContextInstance.TryGetValue("username",out string username);


            _hooks.ScenarioContextInstance.TryGetValue("empDataTable", out DataTable dataTable);

            if(dataTable != null)
            {
                Console.WriteLine(dataTable.Rows[0][0]);
                Console.WriteLine(dataTable.Rows[0]["firstname"]);
                Console.WriteLine(dataTable.Rows[0]["middlename"]);
                Console.WriteLine(dataTable.Rows[0]["lastname"]);
                Console.WriteLine(dataTable.Rows[0]["gender"]);
                Console.WriteLine(dataTable.Rows[0]["DOB"]);
            }
            


            Console.WriteLine(username);

            Console.WriteLine(actualAlertText);
        }
    }
}

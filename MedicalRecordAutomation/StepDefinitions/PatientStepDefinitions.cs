using System;
using System.Threading.Tasks;
using MedicalRecordAutomation.Hooks;
using Reqnroll;

namespace MedicalRecordAutomation.StepDefinitions
{
    [Binding]
    public class PatientStepDefinitions
    {
        [When("I click on patient menu")]
        public async Task WhenIClickOnPatientMenu()
        {
            await AutomationHooks.PageInstance.Locator("xpath=//div[text()='Patient']").ClickAsync();
        }

        [When("I click on New-search menu")]
        public async Task WhenIClickOnNew_SearchMenuAsync()
        {
            await AutomationHooks.PageInstance.Locator("xpath=//div[text()='New/Search']").ClickAsync();
        }

        [When("I fill the new patient form")]
        public async Task WhenIFillTheNewPatientForm(DataTable dataTable)
        {
            Console.WriteLine(dataTable.Rows[0][0]);
            Console.WriteLine(dataTable.Rows[0]["firstname"]);
            Console.WriteLine(dataTable.Rows[0]["middlename"]);
            Console.WriteLine(dataTable.Rows[0]["lastname"]);
            Console.WriteLine(dataTable.Rows[0]["gender"]);
            Console.WriteLine(dataTable.Rows[0]["DOB"]);


            var frame = AutomationHooks.PageInstance.FrameLocator("xpath=//iframe[@name='pat']");
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
            
        }
    }
}

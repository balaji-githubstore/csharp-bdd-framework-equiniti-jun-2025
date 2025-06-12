using MedicalRecordAutomation.Hooks;
using Microsoft.Playwright;
using Reqnroll;
using System;

namespace MedicalRecordAutomation.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        [Given("I have a browser with openemr application")]
        public async Task GivenIHaveABrowserWithOpenemrApplicationAsync()
        {
            await AutomationHooks.PageInstance.GotoAsync("https://demo.openemr.io/b/openemr");
        }

        [When("I enter username as {string}")]
        public void WhenIEnterUsernameAs(string username)
        {
            
        }

        [When("I enter password as {string}")]
        public void WhenIEnterPasswordAs(string password)
        {
            
        }

        [When("I select language as {string}")]
        public void WhenISelectLanguageAs(string language)
        {
           
        }

        [When("I click on login")]
        public void WhenIClickOnLogin()
        {
            
        }

        [Then("I should get access to portal with title as {string}")]
        public void ThenIShouldGetAccessToPortalWithTitleAs(string expectedValue)
        {
           
        }

        [Then("I should get not get access to portal with error {string}")]
        public void ThenIShouldGetNotGetAccessToPortalWithError(string expectedValue)
        {
            
        }
    }
}

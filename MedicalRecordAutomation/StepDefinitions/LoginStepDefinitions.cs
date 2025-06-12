using MedicalRecordAutomation.Hooks;
using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll;
using System;
using System.Threading.Tasks;

namespace MedicalRecordAutomation.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {

        [Given("I have a browser with openemr application")]
        public async Task GivenIHaveABrowserWithOpenemrApplicationAsync()
        {
            await AutomationHooks.PageInstance.GotoAsync("https://demo.openemr.io/a/openemr");
        }

        [When("I enter username as {string}")]
        public async Task WhenIEnterUsernameAsAsync(string username)
        {
            await AutomationHooks.PageInstance.Locator("css=#authUser").FillAsync(username);
        }

        [When("I enter password as {string}")]
        public async Task WhenIEnterPasswordAsAsync(string password)
        {
            await AutomationHooks.PageInstance.Locator("css=#clearPass").FillAsync(password);
        }

        [When("I select language as {string}")]
        public void WhenISelectLanguageAs(string language)
        {

        }

        [When("I click on login")]
        public async Task WhenIClickOnLoginAsync()
        {
            await AutomationHooks.PageInstance.Locator("xpath=//button[@id='login-button']").ClickAsync();
        }

        [Then("I should get access to portal with title as {string}")]
        public async Task ThenIShouldGetAccessToPortalWithTitleAs(string expectedValue)
        {
            Assert.That(await AutomationHooks.PageInstance.TitleAsync(), Is.EqualTo(expectedValue));
        }

        [Then("I should get not get access to portal with error {string}")]
        public async Task ThenIShouldGetNotGetAccessToPortalWithErrorAsync(string expectedValue)
        {
            var actualValue = await AutomationHooks.PageInstance.Locator("xpath=//p[contains(text(),'Invalid')]").InnerTextAsync();      
            Assert.That(actualValue,Is.EqualTo(expectedValue));
        }
    }
}

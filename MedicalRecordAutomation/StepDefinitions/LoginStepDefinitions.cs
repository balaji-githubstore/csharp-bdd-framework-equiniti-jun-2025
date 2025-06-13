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
        private IPage _page;
        private AutomationHooks _hooks;
        public LoginStepDefinitions(AutomationHooks hooks)
        {
            _page = hooks.PageInstance;
            _hooks = hooks;
        }

        [Given("I have a browser with openemr application")]
        public async Task GivenIHaveABrowserWithOpenemrApplicationAsync()
        {
            await _page.GotoAsync("https://demo.openemr.io/a/openemr");
        }

        [When("I enter username as {string}")]
        public async Task WhenIEnterUsernameAsAsync(string username)
        {
            _hooks.ScenarioContextInstance.Add("username", username);
            await _page.Locator("css=#authUser").FillAsync(username);
        }

        [When("I enter password as {string}")]
        public async Task WhenIEnterPasswordAsAsync(string password)
        {
            await _page.Locator("css=#clearPass").FillAsync(password);
        }

        [When("I select language as {string}")]
        public void WhenISelectLanguageAs(string language)
        {
            //actualalerttext
        }

        [When("I click on login")]
        public async Task WhenIClickOnLoginAsync()
        {
            await _page.Locator("xpath=//button[@id='login-button']").ClickAsync();
        }

        [Then("I should get access to portal with title as {string}")]
        public async Task ThenIShouldGetAccessToPortalWithTitleAs(string expectedValue)
        {
            Assert.That(await _page.TitleAsync(), Is.EqualTo(expectedValue));
        }

        [Then("I should get not get access to portal with error {string}")]
        public async Task ThenIShouldGetNotGetAccessToPortalWithErrorAsync(string expectedValue)
        {
            var actualValue = await _page.Locator("xpath=//p[contains(text(),'Invalid')]").InnerTextAsync();
            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Then("I should get username placeholder as {string}")]
        public async Task ThenIShouldGetUsernamePlaceholderAsAsync(string expectedValue)
        {
            var actualValue = await _page.Locator("css=#authUser").GetAttributeAsync("placeholder");
            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Then("I should get language as {string}")]
        public async Task ThenIShouldGetLanguageAsAsync(string p0)
        {
            var actualValue = await _page.Locator("css=select[name='languageChoice'] > option:checked").InnerTextAsync();
        }



    }
}

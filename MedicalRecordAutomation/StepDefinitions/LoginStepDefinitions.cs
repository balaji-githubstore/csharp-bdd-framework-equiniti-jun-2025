using MedicalRecordAutomation.Hooks;
using MedicalRecordAutomation.Pages;
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
        private readonly LoginPage _loginPage;
        private readonly MainPage _mainPage;
        public LoginStepDefinitions(LoginPage loginPage,MainPage mainPage)
        {
            _loginPage = loginPage;
            _mainPage = mainPage;
        }


        [Given("I have a browser with openemr application")]
        public async Task GivenIHaveABrowserWithOpenemrApplicationAsync()
        {
            await _loginPage.NavigateToBaseUrlAsync();
        }

        [When("I enter username as {string}")]
        public async Task WhenIEnterUsernameAsAsync(string username)
        {
          await _loginPage.EnterUsernameAsync(username);
        }

        [When("I enter password as {string}")]
        public async Task WhenIEnterPasswordAsAsync(string password)
        {
            //await AutomationHooks.PageInstance.Locator("css=#clearPass").FillAsync(password);
        }

        [When("I select language as {string}")]
        public void WhenISelectLanguageAs(string language)
        {

        }

        [When("I click on login")]
        public async Task WhenIClickOnLoginAsync()
        {
            //await AutomationHooks.PageInstance.Locator("xpath=//button[@id='login-button']").ClickAsync();
        }

        [Then("I should get access to portal with title as {string}")]
        public async Task ThenIShouldGetAccessToPortalWithTitleAs(string expectedValue)
        {
           // Assert.That(await AutomationHooks.PageInstance.TitleAsync(), Is.EqualTo(expectedValue));
        }

        [Then("I should get not get access to portal with error {string}")]
        public async Task ThenIShouldGetNotGetAccessToPortalWithErrorAsync(string expectedValue)
        {
            //var actualValue = await AutomationHooks.PageInstance.Locator("xpath=//p[contains(text(),'Invalid')]").InnerTextAsync();
            //Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Then("I should get username placeholder as {string}")]
        public async Task ThenIShouldGetUsernamePlaceholderAsAsync(string expectedValue)
        {
            //var actualValue = await AutomationHooks.PageInstance.Locator("css=#authUser").GetAttributeAsync("placeholder");
            //Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Then("I should get language as {string}")]
        public async Task ThenIShouldGetLanguageAsAsync(string p0)
        {
            //var actualValue = await AutomationHooks.PageInstance.Locator("css=select[name='languageChoice'] > option.checked").InnerTextAsync();
        }



    }
}

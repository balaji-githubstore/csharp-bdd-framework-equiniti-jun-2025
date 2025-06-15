using MedicalRecordAutomation.Hooks;
using MedicalRecordAutomation.Pages;
using MedicalRecordAutomation.Support;
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
        public LoginStepDefinitions(LoginPage loginPage, MainPage mainPage)
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
            await _loginPage.EnterPasswordAsync(password);
        }

        [When("I select language as {string}")]
        public void WhenISelectLanguageAs(string language)
        {

        }

        [When("I click on login")]
        public async Task WhenIClickOnLoginAsync()
        {
            await _loginPage.ClickOnLogin();
        }

        [Then("I should get access to portal with title as {string}")]
        public async Task ThenIShouldGetAccessToPortalWithTitleAs(string expectedValue)
        {
            Assert.That(await _mainPage.GetMainPageTitleAsync(), Is.EqualTo(expectedValue));
        }

        [Then("I should get not get access to portal with error {string}")]
        public async Task ThenIShouldGetNotGetAccessToPortalWithErrorAsync(string expectedValue)
        {
            var actualValue = await _loginPage.GetInvalidErrorMessageAsync();
            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Then("I should get username placeholder as {string}")]
        public async Task ThenIShouldGetUsernamePlaceholderAsAsync(string expectedValue)
        {
            var actualValue = await _loginPage.GetUsernamePlaceholderAsync();
            Assert.That(actualValue, Does.Contain(expectedValue));
        }

        [Then("I should get language as {string}")]
        public async Task ThenIShouldGetLanguageAsAsync(string expectedValue)
        {
            var actualValue = await _loginPage.GetSelectedLanguagedAsync();
            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Then("I verify the data from excel {string} sheet {string}")]
        public void ThenIVerifyTheDataFromExcelSheet(string file, string sheetname)
        {
            DataTable dataTable= ExcelUtisl.GetSheetIntoDataTable(file, sheetname);


        }



    }
}

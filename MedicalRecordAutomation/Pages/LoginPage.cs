using MedicalRecordAutomation.Hooks;
using MedicalRecordAutomation.StepDefinitions;
using MedicalRecordAutomation.Support;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordAutomation.Pages
{
    public class LoginPage : BasePage
    {
        private string _usernameLocator = "css=#authUser";
        private string _passwordLocator = "css=#clearPass";
        private string _loginLocator = "css=#login-button";
        private string _errorLocator = "xpath=//p[contains(text(),'Invalid')]";


        private AutomationHooks _hooks;
       // private BasePage _basePage;
       //,BasePage basePage -- can add in constructor and get it as well
        public LoginPage(AutomationHooks hooks):base(hooks)
        {
            _hooks = hooks;
            //_basePage=basePage;
        }
        public async Task NavigateToBaseUrlAsync()
        {
            await _hooks.PageInstance.GotoAsync("https://demo.openemr.io/a/openemr");
        }

        public async Task EnterUsernameAsync(string username)
        {
            //await _hooks.PageInstance.Locator(_usernameLocator).FillAsync("jack");
            await base.SetTextToInputBoxAsync(_usernameLocator, username);
            //await _basePage.SetTextToInputBoxAsync(_usernameLocator, username);
        }

        public async Task EnterPasswordAsync(string password)
        {
            //await _hooks.PageInstance.Locator(_passwordLocator).FillAsync(password);
            await base.SetTextToInputBoxAsync(_passwordLocator, password);
        }

        public async Task ClickOnLogin()
        {
            await _hooks.PageInstance.Locator(_loginLocator).ClickAsync();
        }

        public async Task<string> GetInvalidErrorMessageAsync()
        {
            return await _hooks.PageInstance.Locator(_errorLocator).InnerTextAsync();
        }

        public async Task<string> GetUsernamePlaceholderAsync()
        {
            return await _hooks.PageInstance.Locator(_usernameLocator).GetAttributeAsync("placeholder");
        }


    }
}

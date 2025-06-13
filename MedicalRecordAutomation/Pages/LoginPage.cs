using MedicalRecordAutomation.Hooks;
using MedicalRecordAutomation.StepDefinitions;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordAutomation.Pages
{
    public class LoginPage
    {
        private string _usernameLocator = "css=#authUser";
        private string _passwordLocator = "css=#clearPass";
        private string _loginLocator = "css=#login-button";
        private string _errorLocator = "xpath=//p[contains(text(),'Invalid')]";


        private AutomationHooks _hooks;
        public LoginPage(AutomationHooks hooks)
        {
            _hooks = hooks;

        }
        public async Task NavigateToBaseUrlAsync()
        {
            await _hooks.PageInstance.GotoAsync("https://demo.openemr.io/a/openemr");
        }

        public async Task EnterUsernameAsync(string username)
        {
            await _hooks.PageInstance.Locator(_usernameLocator).FillAsync("jack");
        }

        public async Task EnterPasswordAsync(string password)
        {
            await _hooks.PageInstance.Locator(_passwordLocator).FillAsync(password);
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

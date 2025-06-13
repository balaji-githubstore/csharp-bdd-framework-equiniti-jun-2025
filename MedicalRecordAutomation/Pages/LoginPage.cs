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
        private AutomationHooks _hooks;
        public LoginPage(AutomationHooks hooks)
        {
            _hooks=hooks;
            
        }

        public async Task NavigateToBaseUrlAsync()
        {
            await _hooks.PageInstance.GotoAsync("https://demo.openemr.io/a/openemr");
        }
        
        public async Task EnterUsernameAsync(string username)
        {
            await _hooks.PageInstance.Locator("css=#authUser").FillAsync("jack");
        }
    }
}

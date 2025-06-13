using MedicalRecordAutomation.Hooks;
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
        private IPage _page;

        public LoginPage(AutomationHooks hooks) 
        {
            _page = hooks.PageInstance;
        }


        public async Task EnterUsername(string username)
        {
            await _page.Locator("css=#authUser").FillAsync(username);
        }
    }
}

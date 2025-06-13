using MedicalRecordAutomation.Hooks;
using MedicalRecordAutomation.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordAutomation.Pages
{
    public class MainPage : BasePage
    {
        private AutomationHooks _hooks;
        public MainPage(AutomationHooks hooks):base(hooks)
        {
            _hooks = hooks;
        }

        public async Task<string> GetMainPageTitleAsync()
        {
            return await _hooks.PageInstance.TitleAsync();
        }

        //clickonPatientMenu
        //ClickonNewSearchMenu
    }
}

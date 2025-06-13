using MedicalRecordAutomation.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordAutomation.Pages
{
    public class MainPage
    {
        private AutomationHooks _hooks;
        public MainPage(AutomationHooks hooks)
        {
            _hooks = hooks;
        }
    }
}

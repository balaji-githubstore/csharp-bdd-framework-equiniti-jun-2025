using Microsoft.Playwright;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordAutomation.Hooks
{
    /// <summary>
    /// This class handles the browser and report configuration. 
    /// </summary>
    [Binding]
    public class AutomationHooks
    {
        private IPlaywright PlaywrightInstance { get; set; }
        private IBrowser BrowserInstance { get; set; }
        public IBrowserContext BrowserContextInstance { get; private set; }
        public IPage PageInstance { get; private set; }


        [BeforeScenario]
        public async Task InitScriptAsyc()
        {
            PlaywrightInstance = await Playwright.CreateAsync();
            BrowserInstance = await PlaywrightInstance.Chromium.LaunchAsync(new() { Headless = false });
            BrowserContextInstance = await BrowserInstance.NewContextAsync();
            PageInstance = await BrowserContextInstance.NewPageAsync();
        }

        [AfterScenario]
        public async Task EndScriptAsyc()
        {
            if (PlaywrightInstance != null)
            {
                await PageInstance.CloseAsync();
                await BrowserContextInstance.CloseAsync();
                PlaywrightInstance.Dispose();
            }

        }
    }
}

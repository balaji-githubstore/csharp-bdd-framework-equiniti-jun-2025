using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Parallelizable(ParallelScope.Children)]
[assembly: LevelOfParallelism(2)]
namespace MedicalRecordAutomation.Hooks
{
    /// <summary>
    /// This class handles the browser and report configuration. 
    /// </summary>
    [Binding]
    public class AutomationHooks
    {
        public IPlaywright? PlaywrightInstance { get; private set; }
        public IBrowser? BrowserInstance { get; private set; }
        public IBrowserContext? BrowserContextInstance { get; private set; }
        public IPage? PageInstance { get; private set; }

        public FeatureContext FeatureContextInstance {  get; private set; }
        public ScenarioContext ScenarioContextInstance { get; private set; }

        public AutomationHooks(FeatureContext featureContext,ScenarioContext scenarioContext)
        {
            FeatureContextInstance = featureContext;
            ScenarioContextInstance = scenarioContext;
        }


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

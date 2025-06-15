using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll;
using ReqnrollProjectBDD.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: Parallelizable(ParallelScope.Children)]
[assembly:LevelOfParallelism(4)]
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

        public BrowserSettings BrowserSettingsInstance { get; private set; }
        public FeatureContext FeatureContextInstance { get; private set; }
        public ScenarioContext ScenarioContextInstance { get; private set; }

        public AutomationHooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            FeatureContextInstance = featureContext;
            ScenarioContextInstance = scenarioContext;
        }

        [BeforeScenario]
        public async Task InitScriptAsyc()
        {

            var config = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

            BrowserSettingsInstance = config.GetSection("BrowserSettings").Get<BrowserSettings>();


            PlaywrightInstance = await Playwright.CreateAsync();
            BrowserInstance = await PlaywrightInstance.Chromium.LaunchAsync(new() { Headless = false,Channel=BrowserSettingsInstance.BrowserType });
            BrowserContextInstance = await BrowserInstance.NewContextAsync();
            PageInstance = await BrowserContextInstance.NewPageAsync();
        }

        [AfterScenario]
        public async Task EndScriptAsyc()
        {
            if(ScenarioContextInstance.ScenarioExecutionStatus==ScenarioExecutionStatus.TestError)
            {
                await PageInstance.ScreenshotAsync(new PageScreenshotOptions() { Path= "error" + DateTime.Now.ToString() + ".png" });
            }
            if (PlaywrightInstance != null)
            {
                await PageInstance.CloseAsync();
                await BrowserContextInstance.CloseAsync();
                PlaywrightInstance.Dispose();
            }

        }
    }
}

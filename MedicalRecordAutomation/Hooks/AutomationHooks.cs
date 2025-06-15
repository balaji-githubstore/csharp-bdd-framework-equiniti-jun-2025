using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll;
using ReqnrollProjectBDD.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Parallelizable(ParallelScope.Children)]
[assembly: LevelOfParallelism(4)]

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

        private static readonly string ProjectDirectory = GetProjectDirectory();
        private static readonly string ScreenshotDirectory = Path.Combine(ProjectDirectory, "Screenshots");
        private static readonly string VideoDirectory = Path.Combine(ProjectDirectory, "Videos");
        private static readonly string TraceDirectory = Path.Combine(ProjectDirectory, "Traces");
        public AutomationHooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            FeatureContextInstance = featureContext;
            ScenarioContextInstance = scenarioContext;
        }
        private static string GetProjectDirectory()
        {
            // Get the directory where the assembly is located (bin folder)
            var assemblyLocation = AppContext.BaseDirectory;

            // Navigate up to find the project root (usually 3-4 levels up from bin/Debug/net6.0)
            var directory = new DirectoryInfo(assemblyLocation);
            while (directory != null && !File.Exists(Path.Combine(directory.FullName, "*.csproj")) &&
                   !directory.GetFiles("*.csproj").Any())
            {
                directory = directory.Parent;
            }

            return directory?.FullName ?? assemblyLocation;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Create directories in project root if they don't exist
            Directory.CreateDirectory(ScreenshotDirectory);
            Directory.CreateDirectory(VideoDirectory);
            Directory.CreateDirectory(TraceDirectory);

            // Install Playwright browsers if needed (optional - can be done via CI/CD)
            // Microsoft.Playwright.Program.Main(new[] { "install" });
        }

        [BeforeScenario]
        public async Task InitScriptAsync()
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                BrowserSettingsInstance = config.GetSection("BrowserSettings").Get<BrowserSettings>();

                // Validate browser settings
                if (BrowserSettingsInstance == null)
                {
                    throw new InvalidOperationException("BrowserSettings configuration is missing or invalid");
                }

                PlaywrightInstance = await Playwright.CreateAsync();

                // Configure browser launch options
                var launchOptions = new BrowserTypeLaunchOptions
                {
                    Headless = BrowserSettingsInstance.Headless ?? false,
                    Channel = BrowserSettingsInstance.BrowserType,
                    SlowMo = BrowserSettingsInstance.SlowMo ?? 0,
                    Timeout = BrowserSettingsInstance.Timeout ?? 30000,
                    Args = BrowserSettingsInstance.Args ?? new string[] { }
                };

                // Support different browser types
                BrowserInstance = BrowserSettingsInstance.BrowserName?.ToLower() switch
                {
                    "firefox" => await PlaywrightInstance.Firefox.LaunchAsync(launchOptions),
                    "webkit" or "safari" => await PlaywrightInstance.Webkit.LaunchAsync(launchOptions),
                    _ => await PlaywrightInstance.Chromium.LaunchAsync(launchOptions)
                };

                // Configure browser context options
                var contextOptions = new BrowserNewContextOptions
                {
                    ViewportSize = new ViewportSize
                    {
                        Width = BrowserSettingsInstance.ViewportWidth ?? 1920,
                        Height = BrowserSettingsInstance.ViewportHeight ?? 1080
                    },
                    UserAgent = BrowserSettingsInstance.UserAgent,
                    Locale = BrowserSettingsInstance.Locale ?? "en-US",
                    TimezoneId = BrowserSettingsInstance.TimezoneId,
                    RecordVideoDir = BrowserSettingsInstance.RecordVideo ? VideoDirectory : null,
                    RecordVideoSize = BrowserSettingsInstance.RecordVideo ? new RecordVideoSize { Width = 1920, Height = 1080 } : null
                };

                BrowserContextInstance = await BrowserInstance.NewContextAsync(contextOptions);

                // Enable tracing if configured
                if (BrowserSettingsInstance.EnableTracing)
                {
                    await BrowserContextInstance.Tracing.StartAsync(new TracingStartOptions
                    {
                        Screenshots = true,
                        Snapshots = true,
                        Sources = true
                    });
                }

                PageInstance = await BrowserContextInstance.NewPageAsync();

                // Set default timeouts
                PageInstance.SetDefaultTimeout(BrowserSettingsInstance.DefaultTimeout ?? 30000);
                PageInstance.SetDefaultNavigationTimeout(BrowserSettingsInstance.NavigationTimeout ?? 30000);

                // Store instances in context for access in step definitions
                ScenarioContextInstance.Set(PageInstance, "Page");
                ScenarioContextInstance.Set(BrowserContextInstance, "BrowserContext");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Failed to initialize browser: {ex.Message}");
                throw;
            }
        }

        [AfterScenario]
        public async Task EndScriptAsync()
        {
            try
            {
                var scenarioTitle = ScenarioContextInstance.ScenarioInfo.Title;
                var featureTitle = FeatureContextInstance.FeatureInfo.Title;
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

                // Take screenshot on failure or if configured to always take screenshots
                if (ScenarioContextInstance.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError ||
                    BrowserSettingsInstance?.AlwaysTakeScreenshots == true)
                {
                    var screenshotFileName = $"{featureTitle}_{scenarioTitle}_{timestamp}.png"
                        .Replace(" ", "_")
                        .Replace("/", "_")
                        .Replace("\\", "_");

                    var screenshotPath = Path.Combine(ScreenshotDirectory, screenshotFileName);

                    if (PageInstance != null)
                    {
                        await PageInstance.ScreenshotAsync(new PageScreenshotOptions
                        {
                            Path = screenshotPath,
                            FullPage = true
                        });

                        TestContext.AddTestAttachment(screenshotPath, "Screenshot");
                        TestContext.WriteLine($"Screenshot saved: {screenshotPath}");
                    }
                }

                // Save trace on failure if tracing is enabled
                if (BrowserSettingsInstance?.EnableTracing == true &&
                    ScenarioContextInstance.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
                {
                    var traceFileName = $"{featureTitle}_{scenarioTitle}_{timestamp}.zip"
                        .Replace(" ", "_")
                        .Replace("/", "_")
                        .Replace("\\", "_");

                    var tracePath = Path.Combine(TraceDirectory, traceFileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(tracePath));

                    await BrowserContextInstance.Tracing.StopAsync(new TracingStopOptions
                    {
                        Path = tracePath
                    });

                    TestContext.AddTestAttachment(tracePath, "Trace");
                    TestContext.WriteLine($"Trace saved: {tracePath}");
                }
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error during cleanup: {ex.Message}");
            }
            finally
            {
                // Ensure cleanup happens even if screenshot/trace fails
                await CleanupBrowser();
            }
        }

        private async Task CleanupBrowser()
        {
            try
            {
                if (PageInstance != null)
                {
                    await PageInstance.CloseAsync();
                }

                if (BrowserContextInstance != null)
                {
                    await BrowserContextInstance.CloseAsync();
                }

                if (BrowserInstance != null)
                {
                    await BrowserInstance.CloseAsync();
                }

                PlaywrightInstance?.Dispose();
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error during browser cleanup: {ex.Message}");
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            // Optional: Clean up old screenshots/traces
            // CleanupOldFiles();
        }
    }
}
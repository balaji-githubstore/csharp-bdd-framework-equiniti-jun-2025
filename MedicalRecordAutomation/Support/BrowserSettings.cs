namespace ReqnrollProjectBDD.Support
{
    public class BrowserSettings
    {
        public string BrowserName { get; set; } = "chromium"; // chromium, firefox, webkit
        public string BrowserType { get; set; } // chrome, msedge, chrome-beta, etc.
        public bool? Headless { get; set; } = false;
        public int? SlowMo { get; set; } = 0;
        public int? Timeout { get; set; } = 30000;
        public int? DefaultTimeout { get; set; } = 30000;
        public int? NavigationTimeout { get; set; } = 30000;
        public string[] Args { get; set; } = new string[] { };

        // Viewport settings
        public int? ViewportWidth { get; set; } = 1920;
        public int? ViewportHeight { get; set; } = 1080;

        // Context settings
        public string UserAgent { get; set; }
        public string Locale { get; set; } = "en-US";
        public string TimezoneId { get; set; }

        // Recording and debugging
        public bool RecordVideo { get; set; } = false;
        public bool EnableTracing { get; set; } = false;
        public bool AlwaysTakeScreenshots { get; set; } = false;
    }
}
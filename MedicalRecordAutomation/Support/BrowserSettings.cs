using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollProjectBDD.Support
{
    public class BrowserSettings
    {
        public string BrowserType { get; set; } = "chromium";
        public bool Headless { get; set; } = true;
        public string BaseUrl { get; set; } = string.Empty;
    }
}

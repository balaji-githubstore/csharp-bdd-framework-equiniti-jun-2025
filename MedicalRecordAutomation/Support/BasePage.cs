using MedicalRecordAutomation.Hooks;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordAutomation.Support
{
    public class BasePage
    {

        private AutomationHooks _hooks;
        public BasePage(AutomationHooks hooks)
        {
            _hooks = hooks;
            _hooks.PageInstance.SetDefaultTimeout(40000);
        }


        public async Task SetTextToInputBoxAsync(string locator, string text)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).FillAsync(text);
        }

        public async Task<string> GetInnerTextAsycn(string locator, string text)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            return await _hooks.PageInstance.Locator(locator).InnerTextAsync();
        }

        public async Task<IPage> GetNewTabUsingTitle(string title)
        {
            var pages = _hooks.PageInstance.Context.Pages;
            IPage newTab = null;
            foreach (var page in pages)
            {
                string actualTitle = await page.TitleAsync();
                if (actualTitle.Equals(title))
                {
                    newTab = page;
                    break;
                }
            }
            return newTab;
        }
    }
}

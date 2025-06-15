using MedicalRecordAutomation.Hooks;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordAutomation.Support
{
    /// <summary>
    /// This class contains all reuseable code of playwright. Added some the reusable code. Please add code based on your work
    /// </summary>
    public class BasePage
    {
        private AutomationHooks _hooks;

        public BasePage(AutomationHooks hooks)
        {
            _hooks = hooks;
            _hooks.PageInstance.SetDefaultTimeout(40000);
        }

        #region Text Input Methods
        public async Task SetTextToInputBoxAsync(string locator, string text)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).FillAsync(text);
        }

        public async Task ClearAndSetTextAsync(string locator, string text)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).ClearAsync();
            await _hooks.PageInstance.Locator(locator).FillAsync(text);
        }

        public async Task TypeTextSlowlyAsync(string locator, string text, int delayMs = 100)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).TypeAsync(text, new() { Delay = delayMs });
        }
        #endregion

        #region Click Methods
        public async Task ClickElementAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).ClickAsync();
        }

        public async Task DoubleClickElementAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).DblClickAsync();
        }

        public async Task RightClickElementAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).ClickAsync(new() { Button = MouseButton.Right });
        }

        public async Task ClickByTextAsync(string text)
        {
            await _hooks.PageInstance.GetByText(text).ClickAsync();
        }

        public async Task ClickByRoleAsync(AriaRole role, string name)
        {
            await _hooks.PageInstance.GetByRole(role, new() { Name = name }).ClickAsync();
        }
        #endregion

        #region Text Retrieval Methods
        public async Task<string> GetInnerTextAsync(string locator) // Fixed typo in method name
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            return await _hooks.PageInstance.Locator(locator).InnerTextAsync();
        }

        public async Task<string> GetAttributeValueAsync(string locator, string attributeName)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            return await _hooks.PageInstance.Locator(locator).GetAttributeAsync(attributeName);
        }

        public async Task<string> GetInputValueAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            return await _hooks.PageInstance.Locator(locator).InputValueAsync();
        }

        public async Task<List<string>> GetAllInnerTextsAsync(string locator)
        {
            var elements = await _hooks.PageInstance.Locator(locator).AllAsync();
            var texts = new List<string>();

            foreach (var element in elements)
            {
                texts.Add(await element.InnerTextAsync());
            }

            return texts;
        }
        #endregion

        #region Wait Methods
        public async Task WaitForElementVisibleAsync(string locator, int timeoutMs = 30000)
        {
            await _hooks.PageInstance.Locator(locator).WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeoutMs
            });
        }

        public async Task WaitForElementHiddenAsync(string locator, int timeoutMs = 30000)
        {
            await _hooks.PageInstance.Locator(locator).WaitForAsync(new()
            {
                State = WaitForSelectorState.Hidden,
                Timeout = timeoutMs
            });
        }

        public async Task WaitForUrlAsync(string url, int timeoutMs = 30000)
        {
            await _hooks.PageInstance.WaitForURLAsync(url, new() { Timeout = timeoutMs });
        }

        public async Task WaitForLoadStateAsync(LoadState state = LoadState.Load)
        {
            await _hooks.PageInstance.WaitForLoadStateAsync(state);
        }

        public async Task WaitAsync(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
        #endregion

        #region Element State Methods
        public async Task<bool> IsElementVisibleAsync(string locator)
        {
            try
            {
                return await _hooks.PageInstance.Locator(locator).IsVisibleAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsElementEnabledAsync(string locator)
        {
            try
            {
                return await _hooks.PageInstance.Locator(locator).IsEnabledAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsElementCheckedAsync(string locator)
        {
            try
            {
                return await _hooks.PageInstance.Locator(locator).IsCheckedAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetElementCountAsync(string locator)
        {
            return await _hooks.PageInstance.Locator(locator).CountAsync();
        }
        #endregion

        #region Dropdown/Select Methods
        public async Task SelectDropdownByValueAsync(string locator, string value)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).SelectOptionAsync(value);
        }

        public async Task SelectDropdownByTextAsync(string locator, string text)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).SelectOptionAsync(new SelectOptionValue { Label = text });
        }

        public async Task SelectDropdownByIndexAsync(string locator, int index)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).SelectOptionAsync(new SelectOptionValue { Index = index });
        }
        #endregion

        #region Checkbox/Radio Methods
        public async Task CheckElementAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).CheckAsync();
        }

        public async Task UncheckElementAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).UncheckAsync();
        }

        public async Task SetCheckboxStateAsync(string locator, bool isChecked)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
            await _hooks.PageInstance.Locator(locator).SetCheckedAsync(isChecked);
        }
        #endregion

        #region Navigation Methods
        public async Task NavigateToUrlAsync(string url)
        {
            await _hooks.PageInstance.GotoAsync(url);
        }

        public async Task RefreshPageAsync()
        {
            await _hooks.PageInstance.ReloadAsync();
        }

        public async Task GoBackAsync()
        {
            await _hooks.PageInstance.GoBackAsync();
        }

        public async Task GoForwardAsync()
        {
            await _hooks.PageInstance.GoForwardAsync();
        }

        public string GetCurrentUrl()
        {
            return _hooks.PageInstance.Url;
        }

        public async Task<string> GetPageTitleAsync()
        {
            return await _hooks.PageInstance.TitleAsync();
        }
        #endregion

        #region Tab/Window Methods
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

        public IPage GetNewTabUsingUrlAsync(string url)
        {
            var pages = _hooks.PageInstance.Context.Pages;
            IPage newTab = null;
            foreach (var page in pages)
            {
                if (page.Url.Contains(url))
                {
                    newTab = page;
                    break;
                }
            }
            return newTab;
        }

        public async Task SwitchToTabAsync(IPage page)
        {
            await page.BringToFrontAsync();
        }

        public async Task CloseTabAsync(IPage page)
        {
            await page.CloseAsync();
        }
        #endregion

        #region File Upload/Download Methods
        public async Task UploadFileAsync(string locator, string filePath)
        {
            await _hooks.PageInstance.Locator(locator).SetInputFilesAsync(filePath);
        }

        public async Task UploadMultipleFilesAsync(string locator, string[] filePaths)
        {
            await _hooks.PageInstance.Locator(locator).SetInputFilesAsync(filePaths);
        }
        #endregion

        #region Keyboard/Mouse Actions
        public async Task PressKeyAsync(string key)
        {
            await _hooks.PageInstance.Keyboard.PressAsync(key);
        }

        public async Task PressKeyOnElementAsync(string locator, string key)
        {
            await _hooks.PageInstance.Locator(locator).PressAsync(key);
        }

        public async Task ScrollToElementAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).ScrollIntoViewIfNeededAsync();
        }

        public async Task HoverElementAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).HoverAsync();
        }

        public async Task DragAndDropAsync(string sourceLocator, string targetLocator)
        {
            await _hooks.PageInstance.Locator(sourceLocator).DragToAsync(_hooks.PageInstance.Locator(targetLocator));
        }
        #endregion

        #region Screenshot Methods
        public async Task<byte[]> TakeScreenshotAsync()
        {
            return await _hooks.PageInstance.ScreenshotAsync();
        }

        public async Task<byte[]> TakeElementScreenshotAsync(string locator)
        {
            return await _hooks.PageInstance.Locator(locator).ScreenshotAsync();
        }

        public async Task SaveScreenshotAsync(string path)
        {
            await _hooks.PageInstance.ScreenshotAsync(new() { Path = path });
        }
        #endregion

        #region Alert/Dialog Methods
        private string _lastDialogMessage = string.Empty;
        private string _lastDialogType = string.Empty;
        private TaskCompletionSource<bool> _dialogHandled;

        public async Task<string> HandleDialogAndGetTextAsync(bool accept = true, string inputText = null)
        {
            _dialogHandled = new TaskCompletionSource<bool>();

            void DialogHandler(object sender, IDialog dialog)
            {
                _lastDialogMessage = dialog.Message;
                _lastDialogType = dialog.Type;

                Task.Run(async () =>
                {
                    try
                    {
                        if (accept)
                        {
                            if (!string.IsNullOrEmpty(inputText))
                                await dialog.AcceptAsync(inputText);
                            else
                                await dialog.AcceptAsync();
                        }
                        else
                        {
                            await dialog.DismissAsync();
                        }
                        _dialogHandled.SetResult(true);
                    }
                    catch (Exception ex)
                    {
                        _dialogHandled.SetException(ex);
                    }
                });
            }

            _hooks.PageInstance.Dialog += DialogHandler;

            try
            {
                // Wait for dialog to be handled (with timeout)
                await _dialogHandled.Task.WaitAsync(TimeSpan.FromSeconds(30));
                return _lastDialogMessage;
            }
            finally
            {
                _hooks.PageInstance.Dialog -= DialogHandler;
            }
        }

        public async Task AcceptDialogAsync()
        {
            await HandleDialogAndGetTextAsync(accept: true);
        }

        public async Task DismissDialogAsync()
        {
            await HandleDialogAndGetTextAsync(accept: false);
        }

        public async Task AcceptDialogWithTextAsync(string text)
        {
            await HandleDialogAndGetTextAsync(accept: true, inputText: text);
        }

        public async Task<string> AcceptDialogAndGetTextAsync()
        {
            return await HandleDialogAndGetTextAsync(accept: true);
        }

        public async Task<string> DismissDialogAndGetTextAsync()
        {
            return await HandleDialogAndGetTextAsync(accept: false);
        }

        public string GetLastDialogMessage()
        {
            return _lastDialogMessage;
        }

        public string GetLastDialogType()
        {
            return _lastDialogType;
        }

        public async Task<bool> WaitForDialogAsync(int timeoutMs = 10000)
        {
            var dialogAppeared = false;
            var tcs = new TaskCompletionSource<bool>();

            void DialogHandler(object sender, IDialog dialog)
            {
                dialogAppeared = true;
                _lastDialogMessage = dialog.Message;
                _lastDialogType = dialog.Type;
                tcs.SetResult(true);
            }

            _hooks.PageInstance.Dialog += DialogHandler;

            try
            {
                using var cts = new CancellationTokenSource(timeoutMs);
                cts.Token.Register(() => tcs.TrySetResult(false));

                return await tcs.Task;
            }
            finally
            {
                _hooks.PageInstance.Dialog -= DialogHandler;
            }
        }

        public void SetupDialogAutoHandler(bool autoAccept = true, string defaultText = null)
        {
            _hooks.PageInstance.Dialog += async (_, dialog) =>
            {
                _lastDialogMessage = dialog.Message;
                _lastDialogType = dialog.Type;

                if (autoAccept)
                {
                    if (dialog.Type == "prompt" && !string.IsNullOrEmpty(defaultText))
                        await dialog.AcceptAsync(defaultText);
                    else
                        await dialog.AcceptAsync();
                }
                else
                {
                    await dialog.DismissAsync();
                }
            };
        }
        #endregion

        #region Frame Methods
        public IFrame GetFrameByNameAsync(string name)
        {
            return _hooks.PageInstance.Frame(name);
        }

        public IFrame GetFrameByUrlAsync(string url)
        {
            return _hooks.PageInstance.FrameByUrl(url);
        }
        #endregion

        #region Utility Methods
        public async Task<string> EvaluateJavaScriptAsync(string script)
        {
            var result = await _hooks.PageInstance.EvaluateAsync(script);
            return result?.ToString();
        }

        public async Task<T> EvaluateJavaScriptAsync<T>(string script)
        {
            return await _hooks.PageInstance.EvaluateAsync<T>(script);
        }

        public async Task FocusElementAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).FocusAsync();
        }

        public async Task ClearInputAsync(string locator)
        {
            await _hooks.PageInstance.Locator(locator).ClearAsync();
        }
        #endregion
    }
}
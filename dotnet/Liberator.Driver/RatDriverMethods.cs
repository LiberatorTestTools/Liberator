
using Liberator.Driver.Enums;
using Liberator.Driver.Performance;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Liberator.Driver
{
    public partial class RatDriver<TWebDriver> : IRatDriver<TWebDriver>
        where TWebDriver : IWebDriver, new()
    {
        #region Public Methods

        /// <summary>
        /// Clicks on a WebElement
        /// </summary>
        /// <param name="element">The WebElement on which to click</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickLink(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && DriverName.ToLower().Contains("safari"))
                {
                    IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)ReturnEncapsulatedDriver();
                    javaScriptExecutor.ExecuteScript("arguments[0].click();", Element);
                }
                else
                {
                    element.Click();
                }
            }
            catch (Exception)
            {
                TryClickWithEnter(element);
            }
        }

        /// <summary>
        /// Clicks on a WebElement
        /// </summary>
        /// <param name="element">The WebElement on which to click</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickLink(IWebElement element, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && DriverName.ToLower().Contains("safari"))
                {
                    IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)ReturnEncapsulatedDriver();
                    javaScriptExecutor.ExecuteScript("arguments[0].click();", Element);
                }
                else
                {
                    element.Click();
                }
            }
            catch (Exception)
            {
                TryClickWithEnter(element);
            }
        }

        /// <summary>
        /// Clicks on a WebElement
        /// </summary>
        /// <param name="locator">The locator for the WebElement on which to click</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickLink(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && DriverName.ToLower().Contains("safari"))
                {
                    IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)ReturnEncapsulatedDriver();
                    javaScriptExecutor.ExecuteScript("arguments[0].click();", Element);
                }
                else
                {
                    Element.Click();
                }
            }
            catch (Exception)
            {
                TryClickWithEnter(locator);
            }
        }

        /// <summary>
        /// Clicks on a WebElement
        /// </summary>
        /// <param name="locator">The locator for the WebElement on which to click</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickLink(By locator, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                if (typeof(TWebDriver) == typeof(OperaDriver))
                {
                    Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
                    Element.SendKeys(Keys.Enter);
                }
                if (typeof(TWebDriver) != typeof(OperaDriver))
                {
                    Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && DriverName.ToLower().Contains("safari"))
                    {
                        IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)ReturnEncapsulatedDriver();
                        javaScriptExecutor.ExecuteScript("arguments[0].click();", Element);
                    }
                    else
                    {
                        Element.Click();
                    }
                }
            }
            catch (Exception)
            {
                TryClickWithEnter(locator);
            }
        }

        /// <summary>
        /// Clicks on a link and waits for a new page to be loaded
        /// </summary>
        /// <param name="element">The element on which to click</param>
        public void ClickLinkAndWait(IWebElement element)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }

                LastPage = Driver.FindElement(By.TagName("html"));
                Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
                ClickLink(element, true);
                if (typeof(TWebDriver) != typeof(OperaDriver) && typeof(TWebDriver) != typeof(InternetExplorerDriver))
                {
                    WaitForPageToLoad(LastPage);
                }

                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.PageLoad); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failure during attempt to click a link which opens a page."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on a link and waits for a new page to be loaded
        /// </summary>
        /// <param name="locator">The element on which to click</param>
        public void ClickLinkAndWait(By locator)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }

                LastPage = Driver.FindElement(By.TagName("html"));
                Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
                ClickLink(locator, true);
                WaitForPageToLoad(LastPage);

                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.PageLoad); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failure during attempt to click a link which opens a page."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on a link and waits for a new page to be loaded that contains a specified URL or part URL
        /// </summary>
        /// <param name="element">The element on which to click</param>
        /// <param name="url">The partial URL to be waited for</param>
        public void ClickLinkAndWaitForUrl(IWebElement element, string url)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                LastPage = Driver.FindElement(By.TagName("html"));
                Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
                ClickLink(element, true);
                WaitForUrlToContain(url);
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.PageLoad); }
                Console.Out.WriteLine("URL: {0} has been loaded.", url);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failure during attempt to click a link which opens a page."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on a link and waits for a new page to be loaded that contains a specified URL or part URL
        /// </summary>
        /// <param name="locator">The locator for the element on which to click</param>
        /// <param name="url">The partial URL to be waited for</param>
        public void ClickLinkAndWaitForUrl(By locator, string url)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                LastPage = Driver.FindElement(By.TagName("html"));
                Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
                ClickLink(locator, true);
                WaitForUrlToContain(url);
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.PageLoad); }
                Console.Out.WriteLine("URL: {0} has been loaded.", url);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failure during attempt to click a link which opens a page."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Gets the text of a WebElement
        /// </summary>
        /// <param name="element">The WebElement from which to retrieve text</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetElementText(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                return element.GetAttribute("innerText");
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could not get the text of the specified element."); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets the text of a WebElement
        /// </summary>
        /// <param name="element">The WebElement from which to retrieve text</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetElementText(IWebElement element, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                return element.GetAttribute("innerText");
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could not get the text of the specified element."); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets the text of a WebElement
        /// </summary>
        /// <param name="locator">The WebElement from which to retrieve text</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetElementText(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToExist(locator); }
                return Element.GetAttribute("innerText");
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could not get the text of the specified element."); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets the text of a WebElement
        /// </summary>
        /// <param name="locator">The WebElement from which to retrieve text</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetElementText(By locator, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToExist(locator); }
                return Element.GetAttribute("innerText");
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could not get the text of the specified element."); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Retrieves the text value from the selected option in a dropdown menu.
        /// </summary>
        /// <param name="locator">The locator for the element that represents the dropdown menu.</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetSelectedTextFromDropdown(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (wait) { WaitForElementToBeClickable(locator); };
                Element = Driver.FindElement(locator);
                SelectElement se = new SelectElement(Element);
                return se.SelectedOption.Text;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to get the value of the specified field."); }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves the text value from the selected option in a dropdown menu.
        /// </summary>
        /// <param name="element">The element that represents the dropdown menu.</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetSelectedTextFromDropdown(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); };
                SelectElement se = new SelectElement(element);
                return se.SelectedOption.Text;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to get the value of the specified field."); }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks the browser for the presence of a particular WebElement
        /// </summary>
        /// <param name="element">The WebElement whose presence is tested</param>
        /// <returns>True if the WebElement is present; false if the WebElement is not present</returns>
        public bool ElementExists(IWebElement element)
        {
            Element = element;
            try
            {
                var exists = element.Displayed;
                return exists;
            }
            catch (Exception)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could not confirm whether the specified element exists."); }
                return false;
            }
        }

        /// <summary>
        /// Checks the browser for the presence of a particular WebElement
        /// </summary>
        /// <param name="locator">The locator for the WebElement whose presence is tested</param>
        /// <returns>True if the WebElement is present; false if the WebElement is not present</returns>
        public bool ElementExists(By locator)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                return true;
            }
            catch (Exception)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could not confirm whether the specified element exists."); }
                return false;
            }
        }

        /// <summary>
        /// Gets an attribute of a WebElement and returns it as text
        /// </summary>
        /// <param name="element">The WebElement whose attributes are to be tested</param>
        /// <param name="attribute">The attribute value to retrieve</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>True if the WebElement is present; false if the WebElement is not present</returns>
        public string GetElementAttribute(IWebElement element, string attribute, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                return element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Failed to get the {0} attribute from the specified element.", attribute); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets an attribute of a WebElement and returns it as text
        /// </summary>
        /// <param name="element">The WebElement whose attributes are to be tested</param>
        /// <param name="attribute">The attribute value to retrieve</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>The attribute to retrieve</returns>
        public string GetElementAttribute(IWebElement element, string attribute, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                return element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Failed to get the {0} attribute from the specified element.", attribute); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets an attribute of a WebElement and returns it as text
        /// </summary>
        /// <param name="locator">The locator for the WebElement whose attributes are to be tested</param>
        /// <param name="attribute">The attribute value to retrieve</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>The attribute to retrieve</returns>
        public string GetElementAttribute(By locator, string attribute, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                return Element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Failed to get the {0} attribute from the specified element.", attribute); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets an attribute of a WebElement and returns it as text
        /// </summary>
        /// <param name="locator">The locator for the WebElement whose attributes are to be tested</param>
        /// <param name="attribute">The attribute value to retrieve</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>The attribute to retrieve</returns>
        public string GetElementAttribute(By locator, string attribute, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                return Element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Failed to get the {0} attribute from the specified element.", attribute); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds an element with a unique CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElements that has the CSS Selector</returns>
        public IWebElement FindElementByCssSelector(string cssSelector, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                IWebElement element = Driver.FindElement(By.CssSelector(cssSelector));
                Element = element;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the <{0}> element with the CSS Selector: {0}.", Element.TagName, cssSelector);
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share a CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of WebElements that share the CSS Selector</returns>
        public IEnumerable<IWebElement> FindElementsByCssSelector(string cssSelector, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                IEnumerable<IWebElement> collection = Driver.FindElements(By.CssSelector(cssSelector));
                Elements = collection.ToList();
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the elements with the CSS Selector: {0}.", cssSelector);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByCssSelector(string cssSelector, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.CssSelector(cssSelector));
                Elements = collection.ToList();
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the CSS Selector: {0}.", cssSelector);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find subelements using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child element of a given WebElement by CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A child WebElement with the given CSS Selector</returns>
        public IWebElement FindSubElementByCssSelector(string cssSelector, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.CssSelector(cssSelector));
                Elements = collection.ToList();
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the CSS Selector: {0}.", cssSelector);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find subelements using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByCssSelector(string cssSelector, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.CssSelector(cssSelector));
                Elements = collection.ToList();
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the CSS Selector: {0}.", cssSelector);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child element of a given WebElement by CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A child WebElement with the given CSS Selector</returns>
        public IWebElement FindSubElementByCssSelector(string cssSelector, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.CssSelector(cssSelector));
                Elements = collection.ToList();
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the CSS Selector: {0}.", cssSelector);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement that has a Class Name
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement described by the Class Name</returns>
        public IWebElement FindElementByClassName(string className, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.ClassName(className)); }
                IWebElement element = Driver.FindElement(By.ClassName(className));
                Element = element;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the element with the Class Name: {0}.", className);
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share a Class Name
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByClassName(string className, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.ClassName(className)); }
                IEnumerable<IWebElement> collection = Driver.FindElements(By.ClassName(className));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the elements with the Class Name: {0}.", className);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a Class Name
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByClassName(string className, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.ClassName(className)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.ClassName(className));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the Class Name: {0}.", className);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a child element of a given WebElement that has a specific Class Name.
        /// <para>Will return the first item if a collection is found.</para>
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement described by the Class Name</returns>
        public IWebElement FindSubElementByClassName(string className, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.ClassName(className)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.ClassName(className));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the Class Name: {0}.", className);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a Class Name
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByClassName(string className, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.ClassName(className)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.ClassName(className));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the Class Name: {0}.", className);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a child element of a given WebElement that has a specific Class Name.
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement described by the Class Name</returns>
        public IWebElement FindSubElementByClassName(string className, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.ClassName(className)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.ClassName(className));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the Class Name: {0}.", className);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement whose id is as specified
        /// </summary>
        /// <param name="id">The id to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with the specified id</returns>
        public IWebElement FindElementById(string id, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.Id(id)); }
                IWebElement element = Driver.FindElement(By.Id(id));
                Element = element;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the element with the ID: {0}.", id);
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the ID: {0}.", id); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement whose id is as specified
        /// </summary>
        /// <param name="id">The id to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with the specified id</returns>
        public IWebElement FindSubElementElementById(string id, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                IWebElement element = Driver.FindElement(locator);
                Element = element;
                IEnumerable<IWebElement> collection = Element.FindElements(By.Id(id));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the ID: {0}.", id);
                return Elements.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the ID: {0}.", id); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement whose id is as specified
        /// </summary>
        /// <param name="id">The id to search for</param>
        /// <param name="parent">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with the specified id</returns>
        public IWebElement FindSubElementElementById(string id, IWebElement parent, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(parent); }
                Element = parent;
                IEnumerable<IWebElement> collection = Element.FindElements(By.Id(id));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the ID: {0}.", id);
                return Elements.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the ID: {0}.", id); }
                HandleErrors(ex);
            }
            return null;
        }


        /// <summary>
        /// Finds a WebElement whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with the specified Link Text</returns>
        public IWebElement FindElementByLinkText(string linkText, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                IWebElement element = Driver.FindElement(By.LinkText(linkText));
                Element = element;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the element with the link text: {0}.", linkText);
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByLinkText(string linkText, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                IEnumerable<IWebElement> collection = Driver.FindElements(By.LinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the elements with the link text: {0}.", linkText);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByLinkText(string linkText, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.LinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the link text: {0}.", linkText);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a child element of a given WebElement whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with the specified Link Text</returns>
        public IWebElement FindSubElementByLinkText(string linkText, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.LinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the link text: {0}.", linkText);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByLinkText(string linkText, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.LinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the link text: {0}.", linkText);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }



        /// <summary>
        /// Finds a child element of a given WebElement whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with the specified Link Text</returns>
        public IWebElement FindSubElementByLinkText(string linkText, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.LinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the link text: {0}.", linkText);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement by name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement</returns>
        public IWebElement FindElementByName(string name, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                IWebElement element = Driver.FindElement(By.Name(name));
                Element = element;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the element with the name: {0}.", name);
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share a name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByName(string name, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                IEnumerable<IWebElement> collection = Driver.FindElements(By.Name(name));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the elements with the name: {0}.", name);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByName(string name, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                IEnumerable<IWebElement> collection = Element.FindElements(By.Name(name));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the name: {0}.", name);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a child element of a given WebElement by name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with the specified name</returns>
        public IWebElement FindSubElementByName(string name, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                IEnumerable<IWebElement> collection = Element.FindElements(By.Name(name));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the name: {0}.", name);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByName(string name, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.Name(name));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the name: {0}.", name);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a child element of a given WebElement with a specified name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with the specified name</returns>
        public IWebElement FindSubElementByName(string name, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.Name(name));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the name: {0}.", name);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElements whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement</returns>
        public IWebElement FindElementByPartialLinkText(string linkText, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                IWebElement element = Driver.FindElement(By.PartialLinkText(linkText));
                Element = element;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the element with the partial link text: {0}.", linkText);
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByPartialLinkText(string linkText, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                IEnumerable<IWebElement> collection = Driver.FindElements(By.PartialLinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the elements with the partial link text: {0}.", linkText);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByPartialLinkText(string linkText, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.PartialLinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the partial link text: {0}.", linkText);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child element of a given WebElement whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with matching partial link text</returns>
        public IWebElement FindSubElementByPartialLinkText(string linkText, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.PartialLinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the partial link text: {0}.", linkText);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByPartialLinkText(string linkText, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.PartialLinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the partial link text: {0}.", linkText);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child element of a given WebElement whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A WebElement with matching partial link text</returns>
        public IWebElement FindSubElementByPartialLinkText(string linkText, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.PartialLinkText(linkText));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelement with the partial link text: {0}.", linkText);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share a tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IWebElement FindElementByTag(string tagName, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.TagName(tagName)); }
                IWebElement element = Driver.FindElement(By.TagName(tagName));
                Element = element;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the element with the tag: {0}.", tagName);
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share a tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByTag(string tagName, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.TagName(tagName)); }
                IEnumerable<IWebElement> collection = Driver.FindElements(By.TagName(tagName));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the elements with the tag: {0}.", tagName);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByTag(string tagName, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                IEnumerable<IWebElement> collection = element.FindElements(By.TagName(tagName));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the tag: {0}.", tagName);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a child element of a given WebElement by tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A child WebElement with the given tag</returns>
        public IWebElement FindSubElementByTag(string tagName, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                IEnumerable<IWebElement> collection = element.FindElements(By.TagName(tagName));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the tag: {0}.", tagName);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByTag(string tagName, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.TagName(tagName));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the tag: {0}.", tagName);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }



        /// <summary>
        /// Finds a child element of a given WebElement by tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A child WebElement with the given tag</returns>
        public IWebElement FindSubElementByTag(string tagName, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.TagName(tagName));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the tag: {0}.", tagName);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find a subelement using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IWebElement FindElementByXPath(string xpath, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                IWebElement element = Driver.FindElement(By.XPath(xpath));
                Element = element;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the element with the XPath: {0}.", xpath);
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the xpath: {0}.", xpath); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share an xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByXPath(string xpath, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                IEnumerable<IWebElement> collection = Driver.FindElements(By.XPath(xpath));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the elements with the XPath: {0}.", xpath);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the xpath: {0}.", xpath); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share an xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByXPath(string xpath, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.XPath(xpath));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the XPath: {0}.", xpath);
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the xpath: {0}.", xpath); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a child elements of a given WebElement by xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A child WebElement with the specified XPath</returns>
        public IWebElement FindSubElementByXPath(string xpath, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.XPath(xpath));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the XPath: {0}.", xpath);
                return collection.First();
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.Out.WriteLine("Could not find an element using the xpath: {0}.", xpath); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share an xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByXPath(string xpath, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.XPath(xpath));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the XPath: {0}.", xpath);
                return collection;
            }
            catch (Exception ex)
            {
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share an xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A child WebElements with the given XPath</returns>
        public IWebElement FindSubElementByXPath(string xpath, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                Element = Driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.XPath(xpath));
                Elements = collection;
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the subelements with the XPath: {0}.", xpath);
                return collection.First();
            }
            catch (Exception ex)
            {
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement identified by an attribite value from a collection of WebElements sharing a parent
        /// </summary>
        /// <param name="parentElement">The parent element for the process</param>
        /// <param name="type">The type of locator to use to fetch the collection</param>
        /// <param name="locator">The locator value for items in the collection</param>
        /// <param name="attribute">The HTML attribute to use to find the unique item</param>
        /// <param name="value">The value of the attribute of the unique item</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A web element identified by a locator type and an attribute value</returns>
        public IWebElement ExtractElementFromCollectionByAttribute(IWebElement parentElement, EnumLocatorType type, string locator, string attribute, string value, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = parentElement;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                if (wait) { WaitForElementToBeClickable(parentElement); }
                GetCollectionOfElements(type, locator);
                Element = Elements.Where(e => e.GetAttribute(attribute).Contains(value)).FirstOrDefault();
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the element with the locator value: {0}, which has a {1} attribute of {2}.", locator, attribute, value);
                return Element;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Unable to extract the element required with defined parameters.");
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement identified by an attribite value from a collection of WebElements sharing a parent
        /// </summary>
        /// <param name="parentLocator">The locator for the parent element for the process</param>
        /// <param name="type">The type of locator to use to fetch the collection</param>
        /// <param name="locator">The locator value for items in the collection</param>
        /// <param name="attribute">The HTML attribute to use to find the unique item</param>
        /// <param name="value">The value of the attribute of the unique item</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        /// <returns>A web element identified by a locator type and an attribute value</returns>
        public IWebElement ExtractElementFromCollectionByAttribute(By parentLocator, EnumLocatorType type, string locator, string attribute, string value, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = parentLocator;
            try
            {
                if (RecordPerformance) { RatTimerCollection.StartTimer(); }
                Element = Driver.FindElement(parentLocator);
                if (wait) { WaitForElementToBeVisible(parentLocator); }
                GetCollectionOfElements(type, locator);
                Element = Elements.Where(e => e.GetAttribute(attribute).Contains(value)).FirstOrDefault();
                if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.ElementFindTime); }
                Console.Out.WriteLine("Found the element with the locator value: {0}, which has a {1} attribute of {2}.", locator, attribute, value);
                return Element;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Unable to extract the element required with defined parameters.");
                HandleErrors(ex);
            }
            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Provides a hover action for Hover based methods
        /// </summary>
        /// <param name="locator">The locator for the WebElement to receive the Hover action</param>
        /// <returns>An action</returns>
        private Actions HoverAction(By locator)
        {
            WebDriverWait wait = new WebDriverWait(Driver, Preferences.BaseSettings.MenuHoverTime);
            Element = wait.Until(ExpectedConditions
                .ElementIsVisible(locator));
            return new Actions(Driver);
        }

        /// <summary>
        /// Provides a hover action for Hover based methods
        /// </summary>
        /// <param name="element">The WebElement to receive the Hover action</param>
        /// <returns>An action</returns>
        private Actions HoverAction(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(Driver, Preferences.BaseSettings.MenuHoverTime);
            Element = wait.Until(ExpectedConditions.ElementToBeClickable(element));
            return new Actions(Driver);
        }

        private void GetCollectionOfElements(EnumLocatorType type, string locator)
        {
            IEnumerable<IWebElement> collection = new List<IWebElement>();
            switch (type)
            {
                case EnumLocatorType.ClassName:
                    collection = Element.FindElements(By.ClassName(locator));
                    break;
                case EnumLocatorType.CssSelector:
                    collection = Element.FindElements(By.CssSelector(locator));
                    break;
                case EnumLocatorType.Id:
                    collection = Element.FindElements(By.Id(locator));
                    break;
                case EnumLocatorType.LinkText:
                    collection = Element.FindElements(By.LinkText(locator));
                    break;
                case EnumLocatorType.Name:
                    collection = Element.FindElements(By.Name(locator));
                    break;
                case EnumLocatorType.NotSpecified:
                case EnumLocatorType.PartialLinkText:
                    collection = Element.FindElements(By.PartialLinkText(locator));
                    break;
                case EnumLocatorType.TagName:
                    collection = Element.FindElements(By.TagName(locator));
                    break;
                case EnumLocatorType.XPath:
                    collection = Element.FindElements(By.XPath(locator));
                    break;
            }
            Elements = collection;
        }

        private IWebElement FindSubElement(IWebElement parentElement, By locator)
        {
            try
            {
                return parentElement.FindElement(locator);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Unable to find a subelement required with defined parameters.");
                HandleErrors(ex);
                return null;
            }
        }

        private IEnumerable<IWebElement> FindSubElements(IWebElement parentElement, By locator)
        {
            try
            {
                return parentElement.FindElements(locator);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Unable to find subelements required with defined parameters.");
                HandleErrors(ex);
                return null;
            }
        }

        #endregion

        #region CSS Retrievers

        /// <summary>
        /// Gets a css value from an element
        /// </summary>
        /// <param name="element">The element to query</param>
        /// <param name="attribute">The attribute to query</param>
        /// <returns>The CSS Value</returns>
        public string GetCssValue(IWebElement element, string attribute)
        {
            Element = element;
            try
            {
                string value = element.GetCssValue(attribute);
                return value;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Unable to retrieve the css value required with defined parameters.");
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets a css value from an element
        /// </summary>
        /// <param name="locator">The locator for the element to query</param>
        /// <param name="attribute">The attribute to query</param>
        /// <returns>The CSS Value</returns>
        public string GetCssValue(By locator, string attribute)
        {
            Locator = locator;
            try
            {
                IWebElement element = Driver.FindElement(locator);
                string value = element.GetCssValue(attribute);
                return value;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Unable to retrieve the css value required with defined parameters.");
                HandleErrors(ex);
                return null;
            }
        }

        #endregion

        #region Private Methods

        internal void TryClickWithEnter(IWebElement element)
        {
            try
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human)
                { Console.Out.WriteLine("Could not use the click method. Atempting to send Enter key instead."); }
                element.SendKeys(Keys.Enter);
                Console.Out.WriteLine("Clicked on the <{0}> element passed.", Element.TagName);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ElementNotVisibleException))
                {
                    Console.Out.WriteLine("The element passed by {0} is not visible on the page {1}", ex.Source, Driver.Url);
                    HandleErrors(ex);
                }
                if (_debugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Cannot send enter to the element requested. Attempting JavaScript.");
                    ClickUsingJava(element);
                }
            }
        }

        private void TryClickWithEnter(By locator)
        {
            try
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could not use the click method. Atempting to send Enter key instead."); }
                Element.SendKeys(Keys.Enter);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ElementNotVisibleException))
                {
                    Console.Out.WriteLine("The element passed by {0} is not visible on the page {1}", ex.Source, Driver.Url);
                    HandleErrors(ex);
                }
                if (_debugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Cannot click on the element requested. Attempting JavaScript.");
                    ClickUsingJava(locator);
                }
            }
        }

        #endregion

    }

}
﻿using Liberator.Driver.Enums;
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
        /// Waits for an element to be loaded
        /// </summary>
        /// <param name="element">The element for which to wait</param>
        public void WaitForElementToLoad(IWebElement element)
        {
            Element = element;
            try
            {
                var wait = new WebDriverWait(_driver, _timeout)
                    .Until(ExpectedConditions
                    .ElementToBeClickable(element));
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.WriteLine("Unable to complete wait for element correctly.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Waits for an element to be loaded
        /// </summary>
        /// <param name="locator">The locator for the element for which to wait</param>
        public void WaitForElementToLoad(By locator)
        {
            Locator = locator;
            try
            {
                var wait = new WebDriverWait(_driver, _timeout).Until(ExpectedConditions.ElementToBeClickable(_driver.FindElement(locator)));
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.WriteLine("Unable to complete wait for element correctly.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Waits for an element to be loaded
        /// </summary>
        /// <param name="element">The element for which to wait</param>
        /// <param name="seconds">Maximum number of seconds to wait</param>
        public void WaitForElementToLoad(IWebElement element, int seconds)
        {
            Element = element;
            try
            {
                TimeSpan timeSpan = new TimeSpan(0, 0, 0, seconds, 0);
                var wait = new WebDriverWait(_driver, timeSpan)
                    .Until(ExpectedConditions
                    .ElementToBeClickable(element));
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.WriteLine("Unable to complete wait for element correctly.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Waits for an element to be loaded
        /// </summary>
        /// <param name="locator">The locator for the element for which to wait</param>
        /// <param name="seconds">Maximum number of seconds to wait</param>
        public void WaitForElementToLoad(By locator, int seconds)
        {
            Locator = locator;
            try
            {
                TimeSpan timeSpan = new TimeSpan(0, 0, 0, seconds, 0);
                var wait = new WebDriverWait(this._driver, timeSpan)
                    .Until(ExpectedConditions
                    .ElementToBeClickable(_driver.FindElement(locator)));
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.WriteLine("Unable to complete wait for element correctly.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Waits for an element to disappear from the DOM
        /// </summary>
        /// <param name="locator">The locator for the element for which to wait</param>
        public void WaitForInvisibilityOfElement(By locator)
        {
            Locator = locator;
            try
            {
                bool wait = new WebDriverWait(_driver, _timeout)
                    .Until(ExpectedConditions
                    .InvisibilityOfElementLocated(locator));
                if (wait){ throw new TimeoutException("Item has not disappeared as required by the test code."); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.WriteLine("Unable to complete wait for element correctly.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Waits for an element containing text to disappear from the DOM
        /// </summary>
        /// <param name="locator">The locator for the element for which to wait</param>
        /// <param name="text">The text that should be found in the element</param>
        public void WaitForInvisibilityOfElementWithText(By locator, string text)
        {
            Locator = locator;
            try
            {
                bool wait = new WebDriverWait(_driver, _timeout)
                    .Until(ExpectedConditions
                    .InvisibilityOfElementWithText(locator, text));
                if (wait) { throw new TimeoutException("The invisibility of the element conatining the text specified cannot be ascertained."); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.WriteLine("Unable to complete wait for element correctly.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on a WebElement
        /// </summary>
        /// <param name="element">The WebElement on which to click</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        public void ClickLink(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                element.Click();
            }
            catch (Exception)
            {
                try
                {
                    if (_debugLevel == EnumConsoleDebugLevel.Human)
                    { Console.WriteLine("Could not use the click method. Atempting to send Enter key instead."); }
                    element.SendKeys(Keys.Enter);
                }
                catch (Exception ex)
                {
                    if (_debugLevel == EnumConsoleDebugLevel.Human)
                    {
                        Console.WriteLine("Cannot click on the element requested. Both methods fail.");
                    }
                    if (ex.GetType() == typeof(ElementNotVisibleException))
                    {
                        Console.WriteLine("The element passed by {0} is not visible on the page {1}", ex.Source, Driver.Url);
                    }
                    HandleErrors(ex);
                }
            }
        }

        /// <summary>
        /// Clicks on a WebElement
        /// </summary>
        /// <param name="element">The WebElement on which to click</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        public void ClickLink(IWebElement element, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                element.Click();
            }
            catch (Exception)
            {
                try
                {
                    if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Could not use the click method. Atempting to send Enter key instead."); }
                    element.SendKeys(Keys.Enter);
                }
                catch (Exception ex)
                {
                    if (_debugLevel == EnumConsoleDebugLevel.Human)
                    {
                        Console.WriteLine("Cannot click on the element requested. Both methods fail.");
                    }
                    if (ex.GetType() == typeof(ElementNotVisibleException))
                    {
                        Console.WriteLine("The element passed by {0} is not visible on the page {1}", ex.Source, Driver.Url);
                    }
                    HandleErrors(ex);
                }
            }
        }

        /// <summary>
        /// Clicks on a WebElement
        /// </summary>
        /// <param name="locator">The locator for the WebElement on which to click</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        public void ClickLink(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = _driver.FindElement(locator);
                if(wait) { WaitForElementToBeClickable(Element); }
                Element.Click();
            }
            catch (Exception ex)
            {
                try
                {
                    if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Could not use the click method. Atempting to send Enter key instead."); }
                    _driver.FindElement(locator).SendKeys(Keys.Enter);
                }
                catch (Exception)
                {
                    if (_debugLevel == EnumConsoleDebugLevel.Human)
                    {
                        Console.WriteLine("Cannot click on the element requested. Both methods fail.");
                    }
                    if (ex.GetType() == typeof(ElementNotVisibleException))
                    {
                        Console.WriteLine("The element passed by {0} is not visible on the page {1}", ex.Source, Driver.Url);
                    }
                    HandleErrors(ex);
                }
            }
        }

        /// <summary>
        /// Clicks on a WebElement
        /// </summary>
        /// <param name="locator">The locator for the WebElement on which to click</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        public void ClickLink(By locator, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = _driver.FindElement(locator);
                if(wait) { WaitForElementToBeClickable(Element); }
                if (typeof(TWebDriver) == typeof(OperaDriver))
                {
                    Element.SendKeys(Keys.Enter);
                }
                if (typeof(TWebDriver) != typeof(OperaDriver))
                {
                    Element.Click();
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Could not use the click method. Atempting to send Enter key instead."); }
                    Element.SendKeys(Keys.Enter);
                }
                catch (Exception)
                {
                    if (_debugLevel == EnumConsoleDebugLevel.Human)
                    {
                        Console.WriteLine("Cannot click on the element requested. Both methods fail.");
                    }
                    if (ex.GetType() == typeof(ElementNotVisibleException))
                    {
                        Console.WriteLine("The element passed by {0} is not visible on the page {1}", ex.Source, Driver.Url);
                    }
                    HandleErrors(ex);
                }
            }
        }

        /// <summary>
        /// Clicks on a link and waits for a new page to be loaded
        /// </summary>
        /// <param name="element">The element on which to click</param>
        /// <param name="waitForTarget">(Optional parameter) Whether to wait for the cliackability of the target element</param>
        public void ClickLinkAndWait(IWebElement element, [Optional, DefaultParameterValue(true)] bool waitForTarget)
        {
            Element = element;
            try
            {
                _lastPage = _driver.FindElement(By.TagName("html"));
                if(waitForTarget) { WaitForElementToBeClickable(element); }
                if (typeof(TWebDriver) == typeof(OperaDriver) || typeof(TWebDriver) == typeof(InternetExplorerDriver))
                {
                    Element.SendKeys(Keys.Enter);
                }
                else
                {
                    //TODO: IE & Opera currently not reporting staleness. To be investigated.
                    Element.SendKeys(Keys.Enter);
                    //Element.Click();
                    WaitForPageToLoad(_lastPage);
                }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Failure during attempt to click a link which opens a page."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on a link and waits for a new page to be loaded
        /// </summary>
        /// <param name="locator">The element on which to click</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        public void ClickLinkAndWait(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                _lastPage = _driver.FindElement(By.TagName("html"));
                if (wait) { WaitForPageToLoad(Element); }
                Element.Click();
                WaitForPageToLoad(_lastPage);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Failure during attempt to click a link which opens a page."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Gets the text of a WebElement
        /// </summary>
        /// <param name="element">The WebElement from which to retrieve text</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetElementText(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if(wait) { WaitForElementToBeClickable(element); }
                return element.GetAttribute("innerText");
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Could not get the text of the specified element."); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets the text of a WebElement
        /// </summary>
        /// <param name="element">The WebElement from which to retrieve text</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetElementText(IWebElement element, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if(wait) { WaitForElementToBeClickable(element); }
                return element.GetAttribute("innerText");
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Could not get the text of the specified element."); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets the text of a WebElement
        /// </summary>
        /// <param name="locator">The WebElement from which to retrieve text</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetElementText(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = _driver.FindElement(locator);
                if(wait) { WaitForElementToExist(locator); }
                return Element.GetAttribute("innerText");
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Could not get the text of the specified element."); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets the text of a WebElement
        /// </summary>
        /// <param name="locator">The WebElement from which to retrieve text</param>
        /// <param name="clock">A clock that may be used to set custom timeouts</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>The text of the WebElement</returns>
        public string GetElementText(By locator, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = _driver.FindElement(locator);
                if (wait) { WaitForElementToExist(locator); }
                return Element.GetAttribute("innerText");
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Could not get the text of the specified element."); }
                HandleErrors(ex);
            }
            return null;
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
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Could not confirm whether the specified element exists."); }
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
                Element = _driver.FindElement(locator);
                return true;
            }
            catch (Exception)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.WriteLine("Could not confirm whether the specified element exists."); }
                return false;
            }
        }

        /// <summary>
        /// Gets an attribute of a WebElement and returns it as text
        /// </summary>
        /// <param name="element">The WebElement whose attributes are to be tested</param>
        /// <param name="attribute">The attribute value to retrieve</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>True if the WebElement is present; false if the WebElement is not present</returns>
        public string GetElementAttribute(IWebElement element, string attribute, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if(wait) { WaitForElementToBeClickable(element); }
                return element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Failed to get the {0} attribute from the specified element.", attribute); }
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
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>The attribute to retrieve</returns>
        public string GetElementAttribute(IWebElement element, string attribute, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if(wait) { WaitForElementToBeClickable(element); }
                return element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Failed to get the {0} attribute from the specified element.", attribute); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets an attribute of a WebElement and returns it as text
        /// </summary>
        /// <param name="locator">The locator for the WebElement whose attributes are to be tested</param>
        /// <param name="attribute">The attribute value to retrieve</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>The attribute to retrieve</returns>
        public string GetElementAttribute(By locator, string attribute, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = _driver.FindElement(locator);
                if(wait) { WaitForElementToBeClickable(Element); }
                return Element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Failed to get the {0} attribute from the specified element.", attribute); }
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
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>The attribute to retrieve</returns>
        public string GetElementAttribute(By locator, string attribute, RatClock clock, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = _driver.FindElement(locator);
                if(wait) { WaitForElementToBeClickable(Element); }
                return Element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Failed to get the {0} attribute from the specified element.", attribute); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds an element with a unique CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A WebElements that has the CSS Selector</returns>
        public IWebElement FindElementByCssSelector(string cssSelector, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                IWebElement element = _driver.FindElement(By.CssSelector(cssSelector));
                Element = element;
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share a CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of WebElements that share the CSS Selector</returns>
        public IEnumerable<IWebElement> FindElementsByCssSelector(string cssSelector, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                IEnumerable<IWebElement> collection = _driver.FindElements(By.CssSelector(cssSelector));
                Elements = collection.ToList();
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByCssSelector(string cssSelector, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.CssSelector(cssSelector));
                Elements = collection.ToList();
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find subelements using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a CSS Selector
        /// </summary>
        /// <param name="cssSelector">The CSS Selector to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByCssSelector(string cssSelector, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(cssSelector)); }
                Element = _driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.CssSelector(cssSelector));
                Elements = collection.ToList();
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the CSS Selector: {0}.", cssSelector); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement that has a Class Name
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A WebElement described by the Class Name</returns>
        public IWebElement FindElementByClassName(string className, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.CssSelector(className)); }
                IWebElement element = _driver.FindElement(By.ClassName(className));
                Element = element;
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }
        /// <summary>
        /// Finds a list of elements that share a Class Name
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByClassName(string className, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.CssSelector(className)); }
                IEnumerable<IWebElement> collection = _driver.FindElements(By.ClassName(className));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a Class Name
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByClassName(string className, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(className)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.ClassName(className));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a Class Name
        /// </summary>
        /// <param name="className">The Class Name to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByClassName(string className, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.CssSelector(className)); }
                Element = _driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.ClassName(className));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the Class Name: {0}.", className); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement whose id is as specified
        /// </summary>
        /// <param name="id">The id to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A WebElement with the specified id</returns>
        public IWebElement FindElementById(string id, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.Id(id)); }
                IWebElement element = _driver.FindElement(By.Id(id));
                Element = element;
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the ID: {0}.", id); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A WebElement with the specified Link Text</returns>
        public IWebElement FindElementByLinkText(string linkText, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                IWebElement element = _driver.FindElement(By.LinkText(linkText));
                Element = element;
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByLinkText(string linkText, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                IEnumerable<IWebElement> collection = _driver.FindElements(By.LinkText(linkText));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByLinkText(string linkText, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.LinkText(linkText));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement whose link text is as specified
        /// </summary>
        /// <param name="linkText">The text to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByLinkText(string linkText, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.LinkText(linkText)); }
                Element = _driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.LinkText(linkText));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElement by name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A WebElement</returns>
        public IWebElement FindElementByName(string name, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                IWebElement element = _driver.FindElement(By.Name(name));
                Element = element;
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share a name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByName(string name, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                IEnumerable<IWebElement> collection = _driver.FindElements(By.Name(name));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByName(string name, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                IEnumerable<IWebElement> collection = Element.FindElements(By.Name(name));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByName(string name, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.Name(name)); }
                Element = _driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.Name(name));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the name: {0}.", name); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a WebElements whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A WebElement</returns>
        public IWebElement FindElementByPartialLinkText(string linkText, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                IWebElement element = _driver.FindElement(By.PartialLinkText(linkText));
                Element = element;
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByPartialLinkText(string linkText, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                IEnumerable<IWebElement> collection = _driver.FindElements(By.PartialLinkText(linkText));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByPartialLinkText(string linkText, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.PartialLinkText(linkText));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement whose link text is matched in part
        /// </summary>
        /// <param name="linkText">The text to find in whole or in part</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByPartialLinkText(string linkText, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.PartialLinkText(linkText)); }
                Element = _driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.PartialLinkText(linkText));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the partial link text: {0}.", linkText); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share a tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IWebElement FindElementByTag(string tagName, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.TagName(tagName)); }
                IWebElement element = _driver.FindElement(By.TagName(tagName));
                Element = element;
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share a tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByTag(string tagName, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.TagName(tagName)); }
                IEnumerable<IWebElement> collection = _driver.FindElements(By.TagName(tagName));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByTag(string tagName, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                IEnumerable<IWebElement> collection = element.FindElements(By.TagName(tagName));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share a tag
        /// </summary>
        /// <param name="tagName">The tag to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByTag(string tagName, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (wait) { WaitForElementToBeVisible(locator); }
                Element = _driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.TagName(tagName));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find a subelement using the tag name: {0}.", tagName); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IWebElement FindElementByXPath(string xpath, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                IWebElement element = _driver.FindElement(By.XPath(xpath));
                Element = element;
                return element;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the xpath: {0}.", xpath); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds a list of elements that share an xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindElementsByXPath(string xpath, [Optional, DefaultParameterValue(true)] bool wait)
        {
            try
            {
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                IEnumerable<IWebElement> collection = _driver.FindElements(By.XPath(xpath));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the xpath: {0}.", xpath); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share an xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="element">The parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByXPath(string xpath, IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                IEnumerable<IWebElement> collection = element.FindElements(By.XPath(xpath));
                Elements = collection;
                return collection;
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human || _debugLevel == EnumConsoleDebugLevel.Message)
                { Console.WriteLine("Could not find an element using the xpath: {0}.", xpath); }
                HandleErrors(ex);
            }
            return null;
        }

        /// <summary>
        /// Finds the child elements of a given WebElement that share an xpath
        /// </summary>
        /// <param name="xpath">The xpath to search for</param>
        /// <param name="locator">The locator for the parent WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the cliackability of the element</param>
        /// <returns>A collection of child WebElements</returns>
        public IEnumerable<IWebElement> FindSubElementsByXPath(string xpath, By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                if (wait) { WaitForElementToBeVisible(locator); }
                if (wait) { WaitForElementToBeVisible(By.XPath(xpath)); }
                Element = _driver.FindElement(locator);
                IEnumerable<IWebElement> collection = Element.FindElements(By.XPath(xpath));
                Elements = collection;
                return collection;
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
                if (wait) { WaitForElementToBeClickable(parentElement); }
                GetCollectionOfElements(type, locator);
                Element = Elements.Where(e => e.GetAttribute(attribute).Contains(value)).FirstOrDefault();
                return Element;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to extract the element required with defined parameters.");
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
                Element = _driver.FindElement(parentLocator);
                if (wait) { WaitForElementToBeVisible(parentLocator); }
                GetCollectionOfElements(type, locator);
                Element = Elements.Where(e => e.GetAttribute(attribute).Contains(value)).FirstOrDefault();
                return Element;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to extract the element required with defined parameters.");
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
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(Convert.ToDouble(Preferences.Preferences.KVList["MenuHoverTime"].Value)));
            _element = wait.Until(ExpectedConditions
                .ElementIsVisible(locator));
            return new Actions(_driver);
        }

        /// <summary>
        /// Provides a hover action for Hover based methods
        /// </summary>
        /// <param name="element">The WebElement to receive the Hover action</param>
        /// <returns>An action</returns>
        private Actions HoverAction(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(Convert.ToDouble(Preferences.Preferences.KVList["MenuHoverTime"].Value)));
            _element = wait.Until(ExpectedConditions.ElementToBeClickable(element));
            return new Actions(_driver);
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
                Console.WriteLine("Unable to retrieve the css value required with defined parameters.");
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
                Console.WriteLine("Unable to retrieve the css value required with defined parameters.");
                HandleErrors(ex);
                return null;
            }
        }

        #endregion

    }
}
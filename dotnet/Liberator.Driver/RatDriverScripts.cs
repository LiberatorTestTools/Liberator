using Liberator.Driver.Enums;
using OpenQA.Selenium;
using System;

namespace Liberator.Driver
{
    public partial class RatDriver<TWebDriver> : IRatDriver<TWebDriver>
        where TWebDriver : IWebDriver, new()
    {

        /// <summary>
        /// Takes a screenshot of the active web page
        /// </summary>
        /// <param name="path">The path and name under which to save the image</param>
        public void TakeScreenshot(string path)
        {
            try
            {
                Screenshot s = ((ITakesScreenshot)Driver).GetScreenshot();
                s.SaveAsFile(path, ScreenshotImageFormat.Jpeg);
                Console.Out.WriteLine("Screenshot taken and saved to {0}.", path);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not take a screenshot.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Returns a JavaScript Executor to the end user for custom JavaScript
        /// </summary>
        /// <returns>A JavaScript Executor</returns>
        public IJavaScriptExecutor Scripts()
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return js;
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not return the JavaScript Executor.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Executes a JavaScript statement passed as a string
        /// </summary>
        /// <param name="script">The JavaScript to be executed</param>
        public void ExecuteJavaScript(string script)
        {
            try
            {
                IJavaScriptExecutor js = Scripts();
                js.ExecuteScript(script);
                Console.Out.WriteLine("Executed the sctipt '{0}'.", script);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not execute the passed JavaScript.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Executes a JavaScript statement passed as a string
        /// </summary>
        /// <param name="script">The JavaScript to be executed</param>
        /// <param name="parameters">The parameters for the passed JavaScript</param>
        public void ExecuteJavaScript(string script, params object[] parameters)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript(script, parameters);
                Console.Out.WriteLine("Executed the sctipt '{0}'.", script);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not execute the passed JavaScript with the specified parameters.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Executes a JavaScript statement passed as a string
        /// </summary>
        /// <param name="script">The JavaScript to be executed</param>
        public void ExecuteAsyncJavaScript(string script)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteAsyncScript(script);
                Console.Out.WriteLine("Executed the sctipt '{0}'.", script);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not execute the passed asynchronous JavaScript.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Executes a JavaScript statement passed as a string
        /// </summary>
        /// <param name="script">The JavaScript to be executed</param>
        /// <param name="parameters">The parameters for the passed JavaScript</param>
        public void ExecuteAsyncJavaScript(string script, params object[] parameters)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteAsyncScript(script, parameters);
                Console.Out.WriteLine("Executed the sctipt '{0}'.", script);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not execute the passed asynchronous JavaScript.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Scrolls to an element using JavaScript
        /// </summary>
        /// <param name="webElement">The Web Element</param>
        public void ScrollToElement(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].scrollIntoView(true);", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not scroll to the element passed.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Scrolls to an element using JavaScript
        /// </summary>
        /// <param name="locator">The the locator for the Web Element.</param>
        public void ScrollToElement(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].scrollIntoView(true);", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not scroll to the element passed.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Adds an access key to a web element.
        /// </summary>
        /// <param name="webElement">The Web Element to receive the access key.</param>
        /// <param name="key">The key to be used for access.</param>
        public void AddAccessKeyToElement(IWebElement webElement, string key)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].accessKey = [1];", webElement, key);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not add the access key '[0]' to the [1] element.", key, webElement.TagName);
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Adds an access key to a web element.
        /// </summary>
        /// <param name="locator">The the locator for the Web Element to receive the access key.</param>
        /// <param name="key">The key to be used for access.</param>
        public void AddAccessKeyToElement(By locator, string key)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].accessKey = [1];", webElement, key);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not add the access key '[0]' to the element.", key);
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Gets an access key from a web element.
        /// </summary>
        /// <param name="webElement">The Web Element for which to retrieve the access key.</param>
        public void GetAccessKeyForElement(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].accessKey;", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not retrieve the access key for the [1] element.", webElement.TagName);
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Gets an access key from a web element.
        /// </summary>
        /// <param name="locator">The locator for the Web Element for which to retrieve the access key.</param>
        public void GetAccessKeyForElement(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].accessKey;", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not retrieve the access key for the element.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Returns the number of web elements.
        /// </summary>
        /// <param name="webElement">The web element for which a child element count is required.</param>
        /// <returns>The number of child elements that the web element possesses.</returns>
        public int ChildElementCount(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (int)js.ExecuteScript("arguments[0].childElementCount", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the child element count for the element.");
                }
                HandleErrors(ex);
                return -1;
            }
        }

        /// <summary>
        /// Returns the number of web elements
        /// </summary>
        /// <param name="locator">The locator for the web element for which a child element count is required.</param>
        public int ChildElementCount(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (int)js.ExecuteScript("arguments[0].childElementCount", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the child element count for the element.");
                }
                HandleErrors(ex);
                return -1;
            }
        }

        /// <summary>
        /// Gets the viewable area measurements of the element passed. This does not include borders, scrollbars or margins.
        /// </summary>
        /// <param name="webElement">The web element for which a child element count is required.</param>
        /// <returns>The viewable area measurements of the web element.</returns>
        public ElementSize GetElementSize(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return new ElementSize()
                {
                    Height = (int)js.ExecuteScript("arguments[0].clientHeight", webElement),
                    Width = (int)js.ExecuteScript("arguments[0].clientWidth", webElement),
                    Top = (int)js.ExecuteScript("arguments[0].clientTop", webElement),
                    Left = (int)js.ExecuteScript("arguments[0].clientLeft", webElement)
                };
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the client size for the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the viewable area measurements of the element passed. This does not include borders, scrollbars or margins.
        /// </summary>
        /// <param name="locator">The locator for the web element for which a child element count is required.</param>
        /// <returns>The viewable area measurements of the web element.</returns>
        public ElementSize GetElementSize(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return new ElementSize()
                {
                    Height = (int)js.ExecuteScript("arguments[0].clientHeight", webElement),
                    Width = (int)js.ExecuteScript("arguments[0].clientWidth", webElement),
                    Top = (int)js.ExecuteScript("arguments[0].clientTop", webElement),
                    Left = (int)js.ExecuteScript("arguments[0].clientLeft", webElement)
                };
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the client size for the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Compares the positions for a pair of elements.
        /// </summary>
        /// <param name="firstElement">The first element of the pair.</param>
        /// <param name="secondElement">The second element of the pair.</param>
        /// <returns>An enumerated value for the relative positions of the elements.</returns>
        public EnumElementRelationships CompareElementPositons(IWebElement firstElement, IWebElement secondElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                int returnedValue = (int)js.ExecuteScript("arguments[0].compareDocumentPosition(arguments[1])", firstElement, secondElement);
                return (EnumElementRelationships)returnedValue;
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not analyse the element relationships.");
                }
                HandleErrors(ex);
                return EnumElementRelationships.NoResponse;
            }
        }

        /// <summary>
        /// Compares the positions for a pair of elements.
        /// </summary>
        /// <param name="firstLocator">The locator for the first element of the pair.</param>
        /// <param name="secondLocator">The locator for the second element of the pair.</param>
        /// <returns>An enumerated value for the relative positions of the elements.</returns>
        public EnumElementRelationships CompareElementPositons(By firstLocator, By secondLocator)
        {
            try
            {
                IWebElement firstElement = Driver.FindElement(firstLocator);
                IWebElement secondElement = Driver.FindElement(secondLocator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                int returnedValue = (int)js.ExecuteScript("arguments[0].compareDocumentPosition(arguments[1])", firstElement, secondElement);
                return (EnumElementRelationships)returnedValue;
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not analyse the element relationships.");
                }
                HandleErrors(ex);
                return EnumElementRelationships.NoResponse;
            }
        }

        /// <summary>
        /// Checks to see if an element is contained by another element.
        /// </summary>
        /// <param name="firstElement">The first element of the pair.</param>
        /// <param name="secondElement">The second element of the pair.</param>
        /// <returns>True if the second element is a descendent of the first.</returns>
        public bool? Contains(IWebElement firstElement, IWebElement secondElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].contains(arguments[1])", firstElement, secondElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not analyse the element relationships.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks to see if an element is contained by another element.
        /// </summary>
        /// <param name="firstLocator">The locator for the first element of the pair.</param>
        /// <param name="secondLocator">The locator for the second element of the pair.</param>
        /// <returns>True if the second element is a descendent of the first.</returns>
        public bool? Contains(By firstLocator, By secondLocator)
        {
            try
            {
                IWebElement firstElement = Driver.FindElement(firstLocator);
                IWebElement secondElement = Driver.FindElement(secondLocator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].contains(arguments[1])", firstElement, secondElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not analyse the element relationships.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gives the focus to an element
        /// </summary>
        /// <param name="webElement">The Web Element</param>
        public void GiveFocus(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].focus();", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not give focus to the element.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Gives the focus to an element
        /// </summary>
        /// <param name="locator">The locator for the Web Element</param>
        public void GiveFocus(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].focus();", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not give focus to the element.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Gets the bounding rectangle for the element
        /// </summary>
        /// <param name="webElement">The web element</param>
        /// <returns>An object representing the bounding rectangle</returns>
        public DomRectangle GetBoundingRectagle(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return new DomRectangle()
                {
                    Height = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().height", webElement),
                    Width = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().width", webElement),
                    Top = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().top", webElement),
                    Left = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().left", webElement),
                    Bottom = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().bottom", webElement),
                    Right = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().right", webElement),
                    X = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().x", webElement),
                    Y = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().y", webElement)
                };
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the bounding rectangle for the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the bounding rectangle for the element
        /// </summary>
        /// <param name="locator">The locator for the web element</param>
        /// <returns>An object representing the bounding rectangle</returns>
        public DomRectangle GetBoundingRectagle(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return new DomRectangle()
                {
                    Height = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().height", webElement),
                    Width = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().width", webElement),
                    Top = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().top", webElement),
                    Left = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().left", webElement),
                    Bottom = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().bottom", webElement),
                    Right = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().right", webElement),
                    X = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().x", webElement),
                    Y = (int)js.ExecuteScript("arguments[0].getBoundingClientRect().y", webElement)
                };
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the bounding rectangle for the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks if a Web Element has a particular attribute
        /// </summary>
        /// <param name="webElement">The web element</param>
        /// <param name="attribute">The attribute to check for.</param>
        /// <returns>True if the element has the given attribute.</returns>
        public bool? HasAttribute(IWebElement webElement, string attribute)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].hasAttribute(arguments[1]);", webElement, attribute);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not assess whether the element has the attribute: {0}.", attribute);
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks if a Web Element has a particular attribute
        /// </summary>
        /// <param name="locator">The locator for the web element</param>
        /// <param name="attribute">The attribute to check for.</param>
        /// <returns>True if the element has the given attribute.</returns>
        public bool? HasAttribute(By locator, string attribute)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].hasAttribute(arguments[1]);", webElement, attribute);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not assess whether the element has the attribute: {0}.", attribute);
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks whether the web element has any attributes.
        /// </summary>
        /// <param name="webElement">The Web Element.</param>
        /// <returns>True if the web element has any attributes.</returns>
        public bool? HasAttributes(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].hasAttributes();", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not assess whether the element has attributes.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks whether the web element has any attributes.
        /// </summary>
        /// <param name="locator">The locator for the Web Element.</param>
        /// <returns>True if the web element has any attributes.</returns>
        public bool? HasAttributes(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].hasAttributes();", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not assess whether the element has attributes.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks whether the web element has any child nodes.
        /// </summary>
        /// <param name="webElement">The Web Element.</param>
        /// <returns>True if the web element has any child nodes.</returns>
        public bool? HasChildNodes(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].hasChildNodes();", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not get the number of child nodes.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks whether the web element has any child nodes.
        /// </summary>
        /// <param name="locator">The locator for the Web Element.</param>
        /// <returns>True if the web element has any child nodes.</returns>
        public bool? HasChildNodes(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].hasChildNodes();", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not get the number of child nodes.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks if the content of an element is editable.
        /// </summary>
        /// <param name="webElement">The Web Element</param>
        /// <returns>rue if the content is editable, false if not. Also may return Inherit, to denote it has inherited this status.</returns>
        public string IsContentEditable(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (string)js.ExecuteScript("arguments[0].contentEditable;", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not confirm if the element has editable content.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks if the content of an element is editable.
        /// </summary>
        /// <param name="locator">The the locator for the Web Element.</param>
        /// <returns>True if the content is editable, false if not. Also may return Inherit, to denote it has inherited this status.</returns>
        public string IsContentEditable(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (string)js.ExecuteScript("arguments[0].contentEditable);", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not confirm if the element has editable content.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the language assigned to a WebElement
        /// </summary>
        /// <param name="webElement">The Web Element</param>
        /// <returns>The ISO 639-1 code for the language.</returns>
        public string GetElementLanguage(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (string)js.ExecuteScript("arguments[0].lang;", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not confirm the language assignment of the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the language assigned to a WebElement
        /// </summary>
        /// <param name="locator">The locator for the Web Element</param>
        /// <returns>The ISO 639-1 code for the language.</returns>
        public string GetElementLanguage(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (string)js.ExecuteScript("arguments[0].lang;", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not confirm the language assignment of the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the offsets for an element.
        /// </summary>
        /// <param name="webElement">The Web Element</param>
        /// <returns>An object representing the offsets of the element</returns>
        public HeightWidth GetOffsets(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return new HeightWidth(){
                    Height = (int)js.ExecuteScript("arguments[0].offsetHeight;", webElement),
                    Width = (int)js.ExecuteScript("arguments[0].offsetWidth;", webElement)
                };
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not get the offsets for the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the offsets for an element.
        /// </summary>
        /// <param name="locator">The locator for the Web Element</param>
        /// <returns>An object representing the offsets of the element</returns>
        public HeightWidth GetOffsets(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return new HeightWidth()
                {
                    Height = (int)js.ExecuteScript("arguments[0].offsetHeight;", webElement),
                    Width = (int)js.ExecuteScript("arguments[0].offsetWidth;", webElement)
                };
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not get the offsets for the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks to see if an element is equal to another element.
        /// </summary>
        /// <param name="firstElement">The first element of the pair.</param>
        /// <param name="secondElement">The second element of the pair.</param>
        /// <returns>True if the second element is equal to the first.</returns>
        public bool? AreNodesEqual(IWebElement firstElement, IWebElement secondElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].isEqualNode(arguments[1])", firstElement, secondElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not compare the elements for equality.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks to see if an element is equal to another element.
        /// </summary>
        /// <param name="firstLocator">The locator for the first element of the pair.</param>
        /// <param name="secondLocator">The locator for the second element of the pair.</param>
        /// <returns>True if the second element is equal to the first.</returns>
        public bool? AreNodesEqual(By firstLocator, By secondLocator)
        {
            try
            {
                IWebElement firstElement = Driver.FindElement(firstLocator);
                IWebElement secondElement = Driver.FindElement(secondLocator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (bool)js.ExecuteScript("arguments[0].isEqualNode(arguments[1])", firstElement, secondElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not compare the elements for equality.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the height and width of an element in pixels, including padding, but not the border, scrollbar or margin.
        /// </summary>
        /// <param name="webElement">The Web Element</param>
        /// <returns>An object representing the scroll height and width of the element, along with current positioning.</returns>
        public ElementSize GetScrollSize(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return new ElementSize()
                {
                    Height = (int)js.ExecuteScript("arguments[0].scrollHeight;", webElement),
                    Width = (int)js.ExecuteScript("arguments[0].scrollWidth;", webElement),
                    Top = (int)js.ExecuteScript("arguments[0].scrollTop;", webElement),
                    Left = (int)js.ExecuteScript("arguments[0].scrollLeft;", webElement)
                };
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not get the offsets for the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the height and width of an element in pixels, including padding, but not the border, scrollbar or margin.
        /// </summary>
        /// <param name="locator">The locator for the Web Element</param>
        /// <returns>An object representing the scroll height and width of the element, along with current positioning.</returns>
        public ElementSize GetScrollSize(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return new ElementSize()
                {
                    Height = (int)js.ExecuteScript("arguments[0].scrollHeight;", webElement),
                    Width = (int)js.ExecuteScript("arguments[0].scrollWidth;", webElement),
                    Top = (int)js.ExecuteScript("arguments[0].scrollTop;", webElement),
                    Left = (int)js.ExecuteScript("arguments[0].scrollLeft;", webElement)
                };
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could not get the offsets for the element.");
                }
                HandleErrors(ex);
                return null;
            }
        }
        
        /// <summary>
        /// Returns the tab index of the chosen web element.
        /// </summary>
        /// <param name="webElement">The web element for which a tab index is required.</param>
        /// <returns>The tab index of the chosen element.</returns>
        public int GetTabIndex(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (int)js.ExecuteScript("arguments[0].tabIndex", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the tab index for the element.");
                }
                HandleErrors(ex);
                return -1;
            }
        }

        /// <summary>
        /// Returns the tab index of the chosen web element.
        /// </summary>
        /// <param name="locator">The locator for the web element for which a tab index is required.</param>
        /// <returns>The tab index of the chosen element.</returns>
        public int GetTabIndex(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                return (int)js.ExecuteScript("arguments[0].tabIndex", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the tab index for the element.l");
                }
                HandleErrors(ex);
                return -1;
            }
        }

        #region Private Methods

        /// <summary>
        /// Attempts a click using JavaScript
        /// </summary>
        /// <param name="webElement">The Web Element to which the click is sent.</param>
        internal void ClickUsingJava(IWebElement webElement)
        {
            try
            {
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].click()", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the child element count for the element.");
                }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Attempts a click using JavaScript
        /// </summary>
        /// <param name="locator">The locator for the Web Element to which the click is sent.</param>
        internal void ClickUsingJava(By locator)
        {
            try
            {
                IWebElement webElement = Driver.FindElement(locator);
                IJavaScriptExecutor js = ((IJavaScriptExecutor)Driver);
                js.ExecuteScript("arguments[0].click()", webElement);
            }
            catch (Exception ex)
            {
                if (Preferences.BaseSettings.DebugLevel == EnumConsoleDebugLevel.Human)
                {
                    Console.Out.WriteLine("Could get the child element count for the element.");
                }
                HandleErrors(ex);
            }
        }

        #endregion
    }
}

using Liberator.Driver.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Liberator.Driver
{
    public partial class RatDriver<TWebDriver> : IRatDriver<TWebDriver>
        where TWebDriver : IWebDriver, new()
    {
        #region Action Based Functionality

        /// <summary>
        /// Moves the virtual cursor position to a WebElement which acts as a menu and hovers
        /// </summary>
        /// <param name="element">The WebElement acting as a menu</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void HoverOverMenu(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Hovering over the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute the hover command."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Moves the virtual cursor position to a WebElement which acts as a menu and hovers
        /// </summary>
        /// <param name="locator">The locator for the WebElement acting as a menu</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void HoverOverMenu(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Hovering over the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute the hover command."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on a WebElement acting as a menu and continues to hover
        /// </summary>
        /// <param name="element">The WebElement acting as a menu</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickAndHoverOverMenu(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).Click().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Hovering over the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute the click and hover command."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on a WebElement acting as a menu and continues to hover
        /// </summary>
        /// <param name="locator">The locator for the WebElement acting as a menu</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickAndHoverOverMenu(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).Click().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Hovering over the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute the click and hover command."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on a WebElement acting as a menu and continues to hold
        /// </summary>
        /// <param name="element">The WebElement acting as a menu</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickAndHoldMenu(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).ClickAndHold().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Clicking and holding over the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute the click and hold command."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on a WebElement acting as a menu and continues to hold
        /// </summary>
        /// <param name="locator">The locator for the WebElement acting as a menu</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickAndHoldMenu(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).ClickAndHold().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Clicking and holding over the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute the click and hold command."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on an element and displays a contextual menu
        /// </summary>
        /// <param name="element">The WebElement which is the target of the contextual menu click</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickContextualMenu(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).ContextClick().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Executed a contextual click on the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute a contextual click on the given element."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Clicks on an element and displays a contextual menu
        /// </summary>
        /// <param name="locator">The locator for the WebElement which is the target of the contextual menu click</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ClickContextualMenu(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).ContextClick().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Executed a contextual click on the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute a contextual click on the given element."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Double clicks on a WebElement
        /// </summary>
        /// <param name="element">The WebElement on which a double click is required</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void DoubleClick(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).DoubleClick().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Executed a double click on the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute a double click on the given element."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Double clicks on a WebElement
        /// </summary>
        /// <param name="locator">The locator for the WebElement which is the target of a double click</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void DoubleClick(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).DoubleClick().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Executed a double click on the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to execute a double click on the given element."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Drags a WebElement over a target WebElement and drops it
        /// </summary>
        /// <param name="source">The Web Element being dragged</param>
        /// <param name="target">The target WebElement for the action</param>
        /// <param name="waitForSource">(Optional parameter) Whether to wait for the clickability of the source element</param>
        /// <param name="waitForTarget">(Optional parameter) Whether to wait for the clickability of the target element</param>
        public void DragAndDrop(IWebElement source, IWebElement target, [Optional, DefaultParameterValue(true)] bool waitForSource, [Optional, DefaultParameterValue(true)] bool waitForTarget)
        {
            Element = source;
            List<IWebElement> list = new List<IWebElement>
            {
                source,
                target
            };
            Elements = list;

            try
            {
                if (waitForSource) { WaitForElementToBeClickable(source); }
                if (waitForTarget) { WaitForElementToBeClickable(target); }
                _action = HoverAction(source);
                _action.MoveToElement(Element).DragAndDrop(source, target).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Dragged an {0} element to an {1} element.", source.TagName, target.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to drag and drop the given element to the given target."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Drags a WebElement over a target WebElement and drops it
        /// </summary>
        /// <param name="source">The locator for the Web Element being dragged</param>
        /// <param name="target">The locator for the target WebElement for the action</param>
        /// <param name="waitForSource">(Optional parameter) Whether to wait for the clickability of the source element</param>
        /// <param name="waitForTarget">(Optional parameter) Whether to wait for the clickability of the target element</param>
        public void DragAndDrop(By source, By target, [Optional, DefaultParameterValue(true)] bool waitForSource, [Optional, DefaultParameterValue(true)] bool waitForTarget)
        {
            Locator = source;
            Locators = new List<By>
            {
                source,
                target
            };
            try
            {
                Element = Driver.FindElement(source);
                var _target = Driver.FindElement(target);
                var list = new List<IWebElement>
                {
                    Element,
                    _target
                };

                Elements = list;

                if (waitForSource) { WaitForElementToBeClickable(source); }
                if (waitForTarget) { WaitForElementToBeClickable(target); }

                _action = HoverAction(source);
                _action.MoveToElement(Element).DragAndDrop(Element, _target).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Dragged an <{0}> element to an <{1}> element.", Element.TagName, _target.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to drag and drop the given element to the given target."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Drags a WebElement and drops it at an offset position
        /// </summary>
        /// <param name="element">The WebElement being dragged</param>
        /// <param name="xOffset">The x coordinate of the offset position</param>
        /// <param name="yOffset">The y coordinate of the offset position</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void DragAndDropToOffset(IWebElement element, int xOffset, int yOffset, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).DragAndDropToOffset(element, xOffset, yOffset).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Executed a drag and drop of the <{0}> element to the location X = {1}, Y = {2}.", Element.TagName, xOffset, yOffset); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to drag and drop the given element to the given target."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Drags a WebElement and drops it at an offset position
        /// </summary>
        /// <param name="locator">The locator for the WebElement being dragged</param>
        /// <param name="xOffset">The x coordinate of the offset position</param>
        /// <param name="yOffset">The y coordinate of the offset position</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void DragAndDropToOffset(By locator, int xOffset, int yOffset, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).DragAndDropToOffset(Element, xOffset, yOffset).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Executed a drag and drop of the <{0}> element to the location X = {1}, Y = {2}.", Element.TagName, xOffset, yOffset); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Failed to drag and drop the given element to the given target."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Moves the virtual cursor to a WebElement and presses a key
        /// </summary>
        /// <param name="element">The WebElement on which the key is to be pressed</param>
        /// <param name="key">The key to be pressed</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void KeyDownOnElement(IWebElement element, string key, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).KeyDown(element, key).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Pressed the {0} key on the <{1}> element.", key, Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to complete the key depression on the given element"); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Moves the virtual cursor to a WebElement and presses a key
        /// </summary>
        /// <param name="locator">The locator for the WebElement on which the key is to be pressed</param>
        /// <param name="key">The key to be pressed</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void KeyDownOnElement(By locator, string key, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Driver.FindElement(locator)).KeyDown(Element, key).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Pressed the {0} key on the <{1}> element.", key, Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to complete the key depression on the given element"); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Releases a depressed key which a WebElement is selected
        /// </summary>
        /// <param name="element">The target WebElement for the key release</param>
        /// <param name="key">The key to be released</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void KeyUpOnElement(IWebElement element, string key, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).KeyUp(element, key).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Released the {0} key on the <{1}> element.", key, Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to complete the key release on the given element"); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Releases a depressed key which a WebElement is selected
        /// </summary>
        /// <param name="locator">The locator for the target WebElement for the key release</param>
        /// <param name="key">The key to be released</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void KeyUpOnElement(By locator, string key, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).KeyUp(Element, key).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Released the {0} key on the <{1}> element.", key, Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to complete the key release on the given element"); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Moves the virtual cursor by an offset from a WebElement
        /// </summary>
        /// <param name="element">The WebElement from which the cursor will move</param>
        /// <param name="xOffset">The x coordinate offset for the movement</param>
        /// <param name="yOffset">The y coordinate offset for the movement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void MoveByOffset(IWebElement element, int xOffset, int yOffset, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).MoveByOffset(xOffset, yOffset).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Moved the <{0}> element to the point at X = {1}, Y = {2}", Element.TagName, xOffset, yOffset); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to move the cursor to the specified point."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Moves the virtual cursor by an offset from a WebElement
        /// </summary>
        /// <param name="locator">The locator for the WebElement from which the cursor will move</param>
        /// <param name="xOffset">The x coordinate offset for the movement</param>
        /// <param name="yOffset">The y coordinate offset for the movement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void MoveByOffset(By locator, int xOffset, int yOffset, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if(wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).MoveByOffset(xOffset, yOffset).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Moved the <{0}> element to the point at X = {1}, Y = {2}", Element.TagName, xOffset, yOffset); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to move the cursor to the specified point."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Moves the virtual cursor by an offset from a WebElement
        /// </summary>
        /// <param name="element">The WebElement from which the cursor will move</param>
        /// <param name="xOffset">The x coordinate offset for the movement</param>
        /// <param name="yOffset">The y coordinate offset for the movement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void MoveToElementWithOffset(IWebElement element, int xOffset, int yOffset, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if(wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).MoveToElement(element, xOffset, yOffset).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Moved to the <{0}> element to the point at X = {1}, Y = {2}", Element.TagName, xOffset, yOffset); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to move the cursor to the specified point."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Moves the virtual cursor by an offset from a WebElement
        /// </summary>
        /// <param name="locator">The locator for the WebElement from which the cursor will move</param>
        /// <param name="xOffset">The x coordinate offset for the movement</param>
        /// <param name="yOffset">The y coordinate offset for the movement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void MoveToElementWithOffset(By locator, int xOffset, int yOffset, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if(wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).MoveToElement(Element, xOffset, yOffset).Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Moved to the <{0}> element to the point at X = {1}, Y = {2}", Element.TagName, xOffset, yOffset); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to move the cursor to the specified point."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Releases the mouse button over a WebElement
        /// </summary>
        /// <param name="element">The WebElement over which to release the mouse button</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ReleaseMouseButton(IWebElement element, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                _action = HoverAction(element);
                _action.MoveToElement(element).Release().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Released the mouse button over the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to release the mouse button at the specified element."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Releases the mouse button over a WebElement
        /// </summary>
        /// <param name="locator">The locator for the WebElement over which to release the mouse button</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void ReleaseMouseButton(By locator, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); }
                _action = HoverAction(locator);
                _action.MoveToElement(Element).Release().Build().Perform();
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Released the mouse button over the <{0}> element.", Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Unable to release the mouse button at the specified element."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Sends a sequence of keystrokes to the WebElement specified
        /// </summary>
        /// <param name="element">The WebElement that receives the keystrokes</param>
        /// <param name="text">The keystrokes that are to be sent to the WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void SendValueToField(IWebElement element, string text, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if(wait) { WaitForElementToBeClickable(element); }
                element.Click();
                element.Clear();
                element.SendKeys(text);
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Sent the text '{0}' to the <{1}> element.", text, Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to send the value {0} to the specified field.", text); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Sends a sequence of keystrokes to the WebElement specified
        /// </summary>
        /// <param name="locator">The locator for the WebElement that receives the keystrokes</param>
        /// <param name="text">The keystrokes that are to be sent to the WebElement</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void SendValueToField(By locator, string text, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            try
            {
                Element = Driver.FindElement(locator);
                if (wait) { WaitForElementToBeClickable(Element); } ;
                Element.Click();
                Element.Clear();
                Element.SendKeys(text);
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Sent the text '{0}' to the <{1}> element.", text, Element.TagName); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to send the value {0} to the specified field.", text); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Selects an item from a dropdown by text value
        /// </summary>
        /// <param name="element">The dropdown menu</param>
        /// <param name="item">The item to choose</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void SelectItemFromDropdown(IWebElement element, string item, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); } ;
                SelectElement se = new SelectElement(element);
                se.SelectByText(item);
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Selecting the item '{0}' from the menu element.", item); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to send the value {0} to the specified field.", item); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Selects an item from a dropdown by text value
        /// </summary>
        /// <param name="locator">The locator for the dropdown menu</param>
        /// <param name="item">The item to choose</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void SelectItemFromDropdown(By locator, string item, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            Element = Driver.FindElement(locator);
            try
            {
                if(wait) { WaitForElementToBeClickable(Element); }
                SelectElement se = new SelectElement(Element);
                se.SelectByText(item);
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Selecting the item '{0}' from the menu element.", item); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to send the value {0} to the specified field.", item); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Selects an item from a dropdown by text value
        /// </summary>
        /// <param name="element">The dropdown menu</param>
        /// <param name="row">The row to choose</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void SelectRowFromDropdown(IWebElement element, int row, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); }
                SelectElement se = new SelectElement(element);
                se.SelectByIndex(row);
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Selecting the row {0} from the menu element.", row); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to send the value {0} to the specified field.", row.ToString()); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Selects an item from a dropdown by text value
        /// </summary>
        /// <param name="locator">The locator for the dropdown menu</param>
        /// <param name="row">The row to choose</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void SelectRowFromDropdown(By locator, int row, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            Element = Driver.FindElement(locator);
            try
            {
                if (wait) { WaitForElementToBeClickable(Element); } ;
                SelectElement se = new SelectElement(Element);
                se.SelectByIndex(row);
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Selecting the row {0} from the menu element.", row); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to send the value {0} to the specified field.", row.ToString()); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Selects an item from a dropdown by text value
        /// </summary>
        /// <param name="element">The dropdown menu</param>
        /// <param name="value">The value to choose</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void SelectValueFromDropdown(IWebElement element, string value, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Element = element;
            try
            {
                if (wait) { WaitForElementToBeClickable(element); } ;
                SelectElement se = new SelectElement(element);
                se.SelectByValue(value);
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Selecting the item from the menu element with the value {0}.", value); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to send the value {0} to the specified field.", value); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Selects an item from a dropdown by text value
        /// </summary>
        /// <param name="locator">The locator for the dropdown menu</param>
        /// <param name="value">The value to choose</param>
        /// <param name="wait">(Optional parameter) Whether to wait for the clickability of the element</param>
        public void SelectValueFromDropdown(By locator, string value, [Optional, DefaultParameterValue(true)] bool wait)
        {
            Locator = locator;
            Element = Driver.FindElement(locator);
            try
            {
                if(wait) { WaitForElementToBeClickable(Element); }
                SelectElement se = new SelectElement(Element);
                se.SelectByValue(value);
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Selecting the item from the menu element with the value {0}.", value); }
            }
            catch (Exception ex)
            {
                if (_debugLevel == EnumConsoleDebugLevel.Human) { Console.Out.WriteLine("Could to send the value {0} to the specified field.", value); }
                HandleErrors(ex);
            }
        }

        #endregion

        #region Touch Actions

        ///// <summary>
        ///// Double taps on the WebElement
        ///// </summary>
        ///// <param name="element">The WebElement that receives the double tap</param>
        //public void DoubleTap(IWebElement element)
        //{
        //    Element = element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.DoubleTap(element).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Double taps on the WebElement
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement that receives the double tap</param>
        //public void DoubleTap(By locator)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.DoubleTap(Element).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Presses down on the screen at a given location
        ///// </summary>
        ///// <param name="element">The WebElement that should be on screen which the press event occurs</param>
        ///// <param name="x">The x coordinate for the downward press</param>
        ///// <param name="y">The y coordinate for the downward press</param>
        //public void PressDown(IWebElement element, int x, int y)
        //{
        //    Element = element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Down(x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Presses down on the screen at a given location
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement that should be on screen which the press event occurs</param>
        ///// <param name="x">The x coordinate for the downward press</param>
        ///// <param name="y">The y coordinate for the downward press</param>
        //public void PressDown(By locator, int x, int y)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Down(x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Flicks the screen starting at a WebElement
        ///// </summary>
        ///// <param name="element">The WebElement which is at the start of the flick</param>
        ///// <param name="x">The offset movement on the x axis relative to the viewport</param>
        ///// <param name="y">The offset movement on the y axis relative to the viewport</param>
        ///// <param name="speed">The flick speed in pixrls per second</param>
        //public void FlickScreen(IWebElement element, int x, int y, int speed)
        //{
        //    Element = element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Flick(element, x, y, speed).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Flicks the screen starting at a WebElement
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement which is at the start of the flick</param>
        ///// <param name="x">The offset movement on the x axis relative to the viewport</param>
        ///// <param name="y">The offset movement on the y axis relative to the viewport</param>
        ///// <param name="speed">The flick speed in pixrls per second</param>
        //public void FlickScreen(By locator, int x, int y, int speed)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Flick(Element, x, y, speed).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Flicks the screen at a set speed in two dimensions
        ///// </summary>
        ///// <param name="element">The WebElement which must be displayed before the flick occurs</param>
        ///// <param name="speedX">The horizontal speed in pixels per second</param>
        ///// <param name="speedY">The vertical speed in pixels per second</param>
        //public void FlickScreen(IWebElement element, int speedX, int speedY)
        //{
        //    Element = element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Flick(speedX, speedY).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Flicks the screen at a set speed in two dimensions
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement which must be displayed before the flick occurs</param>
        ///// <param name="speedX">The horizontal speed in pixels per second</param>
        ///// <param name="speedY">The vertical speed in pixels per second</param>
        //public void FlickScreen(By locator, int speedX, int speedY)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Flick(speedX, speedY).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Presses and holds on a specified WebElement
        ///// </summary>
        ///// <param name="element">The WebElement to receive the Long Press event</param>
        //public void LongPress(IWebElement element)
        //{
        //    Element = element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.LongPress(element).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Presses and holds on a specified WebElement
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement to receive the Long Press event</param>
        //public void LongPress(By locator)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.LongPress(Element).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Moves to a specified point on the screen
        ///// </summary>
        ///// <param name="element">The WebElement which must be displayed before the movement occurs</param>
        ///// <param name="x">The x coordinate for the movement</param>
        ///// <param name="y">The y coordinate for the movement</param>
        //public void Move(IWebElement element, int x, int y)
        //{
        //    Element = element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Move(x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Moves to a specified point on the screen
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement which must be displayed before the movement occurs</param>
        ///// <param name="x">The x coordinate for the movement</param>
        ///// <param name="y">The y coordinate for the movement</param>
        //public void Move(By locator, int x, int y)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Move(x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Scrolls the screen to a given offset
        ///// </summary>
        ///// <param name="element">The WebElement that must be present before the scroll is attempted</param>
        ///// <param name="x">The x coordinate for the movement</param>
        ///// <param name="y">The y coordinate for the movement</param>
        //public void ScrollToOffset(IWebElement element, int x, int y)
        //{
        //    Element = _element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Scroll(x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Scrolls the screen to a given offset
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement that must be present before the scroll is attempted</param>
        ///// <param name="x">The x coordinate for the movement</param>
        ///// <param name="y">The y coordinate for the movement</param>
        //public void ScrollToOffset(By locator, int x, int y)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Scroll(x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Scrolls the screen from the given element
        ///// </summary>
        ///// <param name="element">The WebElement that is to receive the scroll</param>
        ///// <param name="x">The x coordinate for the movement</param>
        ///// <param name="y">The y coordinate for the movement</param>
        //public void ScrollElement(IWebElement element, int x, int y)
        //{
        //    Element = element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Scroll(element, x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Scrolls the screen from the given element
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement that is to receive the scroll</param>
        ///// <param name="x">The x coordinate for the movement</param>
        ///// <param name="y">The y coordinate for the movement</param>
        //public void ScrollElement(By locator, int x, int y)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Scroll(Element, x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Taps once on a WebElement
        ///// </summary>
        ///// <param name="element">The WebElement that receives the single tap</param>
        //public void SingleTap(IWebElement element)
        //{
        //    Element = element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.SingleTap(element).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Taps once on a WebElement
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement that receives the single tap</param>
        //public void SingleTap(By locator)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.SingleTap(Element).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Releases a press on the screen at a set location
        ///// </summary>
        ///// <param name="element">The WebElement which must be on screen before the release occurs</param>
        ///// <param name="x">The x coordinate of the release</param>
        ///// <param name="y">The y coordinate of the release</param>
        //public void ReleasePress(IWebElement element, int x, int y)
        //{
        //    Element = element;
        //    try
        //    {
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Up(x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        ///// <summary>
        ///// Releases a press on the screen at a set location
        ///// </summary>
        ///// <param name="locator">The locator for the WebElement which must be on screen before the release occurs</param>
        ///// <param name="x">The x coordinate of the release</param>
        ///// <param name="y">The y coordinate of the release</param>
        //public void ReleasePress(By locator, int x, int y)
        //{
        //    Locator = locator;
        //    try
        //    {
        //        Element = _driver.FindElement(locator);
        //        var wait = new WebDriverWait(_driver, Preferences.BaseSettings.Timeout).Until(ExpectedConditions.ElementToBeClickable(Element));
        //        Touch = new TouchActions(_driver);
        //        Touch.Up(x, y).Build().Perform();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Out.WriteLine("");
        //        HandleErrors(ex);
        //    }
        //}

        //#endregion

        
        #endregion
    }
}

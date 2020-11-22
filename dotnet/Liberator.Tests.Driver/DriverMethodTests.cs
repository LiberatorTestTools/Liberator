using Liberator.Driver;
using Liberator.Driver.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Linq;
using static Liberator.Tests.Driver.Hooks;

namespace Liberator.Tests.Driver
{
    [TestFixture]
    public class DriverMethodTests
    {
        [Test]
        [Category("Firefox")]
        public void Firefox_ClickLinkWithClock()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(devLink, clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_ClickLinkWithClock()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(devLink, clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_ClickLinkWithClock()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(devLink, clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_ClickLinkWithClock()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(devLink, clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_ClickLinkWithClock()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(devLink, clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_ClickLinkWithClock()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(devLink, clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }


        [Test]
        [Category("Firefox")]
        public void Firefox_ClickLinkAndWaitForPage()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWaitForUrl(devLink, "developments");
            ratDriver.ClosePagesAndQuitDriver();
        }


        [Test]
        [Category("Chrome")]
        public void Chrome_ClickLinkAndWaitForPage()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWaitForUrl(devLink, "developments");
            ratDriver.ClosePagesAndQuitDriver();
        }


        [Test]
        [Category("Edge")]
        public void Edge_ClickLinkAndWaitForPage()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWaitForUrl(devLink, "developments");
            ratDriver.ClosePagesAndQuitDriver();
        }


        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_ClickLinkAndWaitForPage()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWaitForUrl(devLink, "developments");
            ratDriver.ClosePagesAndQuitDriver();
        }


        [Test]
        [Category("Opera")]
        public void Opera_ClickLinkAndWaitForPage()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWaitForUrl(devLink, "developments");
            ratDriver.ClosePagesAndQuitDriver();
        }


        [Test]
        [Category("Safari")]
        public void Safari_ClickLinkAndWaitForPage()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWaitForUrl(devLink, "developments");
            ratDriver.ClosePagesAndQuitDriver();
        }


        [Test]
        [Category("Firefox")]
        public void Firefox_ClickLinkWithClockBy()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_ClickLinkWithClockBy()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_ClickLinkWithClockBy()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_ClickLinkWithClockBy()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_ClickLinkWithClockBy()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_ClickLinkWithClockBy()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            ratDriver.ClickLink(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetElementTextWithClock()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            var text = ratDriver.GetElementText(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            Assert.IsTrue(text.Contains("Developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_GetElementTextWithClock()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            var text = ratDriver.GetElementText(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            Assert.IsTrue(text.Contains("Developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetElementTextWithClock()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            var text = ratDriver.GetElementText(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            Assert.IsTrue(text.Contains("Developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetElementTextWithClock()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            var text = ratDriver.GetElementText(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            Assert.IsTrue(text.Contains("Developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetElementTextWithClock()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            var text = ratDriver.GetElementText(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            Assert.IsTrue(text.Contains("Developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_GetElementTextWithClock()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            RatClock clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            var text = ratDriver.GetElementText(By.LinkText("Developments"), clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            Assert.IsTrue(text.Contains("Developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetElementAttribute()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            Assert.IsTrue(ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride").Contains("carousel"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_GetElementAttribute()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            Assert.IsTrue(ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride").Contains("carousel"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetElementAttribute()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            Assert.IsTrue(ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride").Contains("carousel"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetElementAttribute()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            Assert.IsTrue(ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride").Contains("carousel"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetElementAttribute()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            Assert.IsTrue(ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride").Contains("carousel"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_GetElementAttribute()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            Assert.IsTrue(ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride").Contains("carousel"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetElementAttributeWithClock()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            _ = ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride", clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_GetElementAttributeWithClock()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            _ = ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride", clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetElementAttributeWithClock()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            _ = ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride", clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetElementAttributeWithClock()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            _ = ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride", clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetElementAttributeWithClock()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            _ = ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride", clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_GetElementAttributeWithClock()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var clock = new RatClock();
            var targetTime = clock.LaterBy(new TimeSpan(0, 0, 0, 30, 0));
            _ = ratDriver.GetElementAttribute(ratDriver.FindElementById("carousel-example"), "data-ride", clock);
            Assert.IsTrue(clock.IsNowBefore(targetTime));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindByClassName()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByClassName("carousel-inner");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindByClassName()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByClassName("carousel-inner");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindByClassName()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByClassName("carousel-inner");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindByClassName()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByClassName("carousel-inner");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindByClassName()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByClassName("carousel-inner");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindByClassName()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByClassName("carousel-inner");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindElementsByClassName()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByClassName("menutext");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindElementsByClassName()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByClassName("menutext");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindElementsByClassName()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByClassName("menutext");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindElementsByClassName()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByClassName("menutext");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindElementsByClassName()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByClassName("menutext");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindElementsByClassName()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByClassName("menutext");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindSubElementsByClassName()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByCssSelector(".nav.navbar-nav.navbar-right");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindSubElementsByClassName()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByCssSelector(".nav.navbar-nav.navbar-right");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindSubElementsByClassName()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByCssSelector(".nav.navbar-nav.navbar-right");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindSubElementsByClassName()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByCssSelector(".nav.navbar-nav.navbar-right");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindSubElementsByClassName()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByCssSelector(".nav.navbar-nav.navbar-right");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindSubElementsByClassName()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByCssSelector(".nav.navbar-nav.navbar-right");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindSubElementsByClassNameLocator()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", By.ClassName("container"));
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindSubElementsByClassNameLocator()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", By.ClassName("container"));
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindSubElementsByClassNameLocator()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", By.ClassName("container"));
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindSubElementsByClassNameLocator()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", By.ClassName("container"));
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindSubElementsByClassNameLocator()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", By.ClassName("container"));
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindSubElementsByClassNameLocator()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByClassName("menutext", By.ClassName("container"));
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindById()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementById("home");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindById()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementById("home");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindById()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementById("home");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindById()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementById("home");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindById()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementById("home");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindById()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementById("home");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindByLinkText()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindByLinkText()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindByLinkText()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindByLinkText()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindByLinkText()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindByLinkText()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByLinkText("Developments");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindElementsByLinkText()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByLinkText("Read Details");
            Assert.IsTrue(elements.Count() == 6);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindElementsByLinkText()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByLinkText("Read Details");
            Assert.IsTrue(elements.Count() == 6);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindElementsByLinkText()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByLinkText("Read Details");
            Assert.IsTrue(elements.Count() == 6);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindElementsByLinkText()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByLinkText("Read Details");
            Assert.IsTrue(elements.Count() == 6);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindElementsByLinkText()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByLinkText("Read Details");
            Assert.IsTrue(elements.Count() == 6);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindElementsByLinkText()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByLinkText("Read Details");
            Assert.IsTrue(elements.Count() == 6);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindSubElementsByLinkText()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindSubElementsByLinkText()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindSubElementsByLinkText()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindSubElementsByLinkText()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindSubElementsByLinkText()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindSubElementsByLinkText()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindSubElementsByLinkTextLocator()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindSubElementsByLinkTextLocator()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindSubElementsByLinkTextLocator()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindSubElementsByLinkTextLocator()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindSubElementsByLinkTextLocator()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindSubElementsByLinkTextLocator()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByLinkText("Read Details", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindElementByTagName()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByTag("body");
            Assert.IsNotNull(element);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindElementByTagName()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByTag("body");
            Assert.IsNotNull(element);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindElementByTagName()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByTag("body");
            Assert.IsNotNull(element);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindElementByTagName()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByTag("body");
            Assert.IsNotNull(element);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindElementByTagName()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByTag("body");
            Assert.IsNotNull(element);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindElementByTagName()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByTag("body");
            Assert.IsNotNull(element);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindElementsByTagName()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByTag("section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindElementsByTagName()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByTag("section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindElementsByTagName()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByTag("section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindElementsByTagName()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByTag("section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindElementsByTagName()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByTag("section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindElementsByTagName()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByTag("section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindSubElementsByTagName()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindSubElementsByTagName()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindSubElementsByTagName()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindSubElementsByTagName()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindSubElementsByTagName()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindSubElementsByTagName()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementById("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindSubElementsByTagNameLocator()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindSubElementsByTagNameLocator()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindSubElementsByTagNameLocator()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindSubElementsByTagNameLocator()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindSubElementsByTagNameLocator()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindSubElementsByTagNameLocator()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.Id("just-intro");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByTag("a", parent);
            Assert.IsTrue(elements.Count() == 3);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindElementByXPath()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByXPath(".//*[@id='carousel-example']");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindElementByXPath()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByXPath(".//*[@id='carousel-example']");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindElementByXPath()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByXPath(".//*[@id='carousel-example']");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindElementByXPath()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByXPath(".//*[@id='carousel-example']");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindElementByXPath()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByXPath(".//*[@id='carousel-example']");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindElementByXPath()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement element = ratDriver.FindElementByXPath(".//*[@id='carousel-example']");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindElementsByXPath()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByXPath(".//section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindElementsByXPath()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByXPath(".//section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindElementsByXPath()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByXPath(".//section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindElementsByXPath()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByXPath(".//section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindElementsByXPath()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByXPath(".//section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindElementsByXPath()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IEnumerable<IWebElement> elements = ratDriver.FindElementsByXPath(".//section");
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindSubElementsByXPath()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindSubElementsByXPath()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindSubElementsByXPath()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindSubElementsByXPath()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindSubElementsByXPath()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindSubElementsByXPath()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_FindSubElementsByXPathLocator()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_FindSubElementsByXPathLocator()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_FindSubElementsByXPathLocator()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_FindSubElementsByXPathLocator()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_FindSubElementsByXPathLocator()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_FindSubElementsByXPathLocator()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IEnumerable<IWebElement> elements = ratDriver.FindSubElementsByXPath(".//section", parent);
            Assert.IsTrue(elements.Count() == 5);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_ExtractElementFromCollectionByAttributeLocator()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_ExtractElementFromCollectionByAttributeLocator()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_ExtractElementFromCollectionByAttributeLocator()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_ExtractElementFromCollectionByAttributeLocator()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_ExtractElementFromCollectionByAttributeLocator()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_ExtractElementFromCollectionByAttributeLocator()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            By parent = By.XPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_ExtractElementFromCollectionByAttribute()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_ExtractElementFromCollectionByAttribute()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_ExtractElementFromCollectionByAttribute()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_ExtractElementFromCollectionByAttribute()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_ExtractElementFromCollectionByAttribute()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_ExtractElementFromCollectionByAttribute()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            IWebElement parent = ratDriver.FindElementByXPath("html/body");
            IWebElement element = ratDriver.ExtractElementFromCollectionByAttribute(parent, EnumLocatorType.XPath, ".//section", "class", "note-sec");
            Assert.IsTrue(element.Displayed);
            Assert.IsTrue(element.Enabled);
            ratDriver.ClosePagesAndQuitDriver();
        }

    }
}

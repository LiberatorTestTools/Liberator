using Liberator.Driver;
using Liberator.Driver.Preferences;
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
using System.Threading;
using static Liberator.Tests.Driver.Hooks;

namespace Liberator.Tests.Driver
{
    [TestFixture]
    public class DriverManagementTests
    {
        [Test]
        [Category("Firefox")]
        public void InstantiateFirefoxDriver()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsTrue(((RatDriver<FirefoxDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void InstantiateFirefoxDriver_WithProfile()
        {
            ratDriver = new RatDriver<FirefoxDriver>("Automation");
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsTrue(((RatDriver<FirefoxDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void InstantiateChromeDriver()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsTrue(((RatDriver<ChromeDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void InstantiateEdgeDriver()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsTrue(((RatDriver<EdgeDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InstantiateInternetExplorerDriver()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsTrue(((RatDriver<InternetExplorerDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void InstantiateOperaDriver()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsTrue(((RatDriver<OperaDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void InstantiateSafariDriver()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsTrue(((RatDriver<SafariDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_ClickLink()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            var devLink = ((RatDriver<FirefoxDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(((RatDriver<FirefoxDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_ClickLink()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            var devLink = ((RatDriver<ChromeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(((RatDriver<ChromeDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_ClickLink()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            var devLink = ((RatDriver<EdgeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(((RatDriver<EdgeDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_ClickLink()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            var devLink = ((RatDriver<InternetExplorerDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(((RatDriver<InternetExplorerDriver>)ratDriver).Driver != null);
            Thread.Sleep(500);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().ToLower().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_ClickLink()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            var devLink = ((RatDriver<OperaDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(((RatDriver<OperaDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_ClickLink()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<SafariDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(((RatDriver<SafariDriver>)ratDriver).Driver != null);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetText()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<FirefoxDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetElementText(By.CssSelector(".col-md-12>h1"));
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_GetText()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<ChromeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetElementText(By.CssSelector(".col-md-12>h1"));
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetText()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<EdgeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetElementText(By.CssSelector(".col-md-12>h1"));
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetText()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<InternetExplorerDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".row.text-center.pad-row"));
            var str = ratDriver.GetElementText(By.CssSelector(".row.text-center.pad-row"));
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetText()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<OperaDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetElementText(By.CssSelector(".col-md-12>h1"));
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Safari")]
        public void Safari_GetText()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<SafariDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetElementText(By.CssSelector(".col-md-12>h1"));
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_CheckPageSourceForText()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<FirefoxDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12"));
            var str = ratDriver.GetPageSource();
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_CheckPageSourceForText()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<ChromeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetPageSource();
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Edge")]
        public void Edge_CheckPageSourceForText()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<EdgeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetPageSource();
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_CheckPageSourceForText()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<InternetExplorerDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetPageSource();
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Opera")]
        public void Opera_CheckPageSourceForText()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<OperaDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetPageSource();
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Safari")]
        public void Safari_CheckPageSourceForText()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<SafariDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            ratDriver.WaitForElementToLoad(By.CssSelector(".col-md-12>h1"));
            var str = ratDriver.GetPageSource();
            ratDriver.ClosePagesAndQuitDriver();
            Assert.That(str.Contains("Totally Ratted Developments"));
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_CookieTests()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            ratDriver.WaitForElementToLoad(ratDriver.FindElementByCssSelector(".navbar-brand.tradewinds"));

            ratDriver.AddCookie("ratCookie", "ratData");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratData"));
            ratDriver.ReplaceCookie("ratCookie", "ratUpdate");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratCookie"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratUpdate"));
            ratDriver.DeleteCookieNamed("ratCookie");

            ratDriver.AddCookie("ratMuffin", "caramel", "/");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("caramel"));
            ratDriver.ReplaceCookie("ratMuffin", "blueberry");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratMuffin"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("blueberry"));
            ratDriver.DeleteCookieNamed("ratMuffin");

            ratDriver.AddCookie("ratBagel", "cheese", "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("cheese"));
            ratDriver.ReplaceCookie("ratBagel", "ham");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratBagel"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("ham"));
            ratDriver.DeleteCookieNamed("ratBagel");

            ratDriver.AddCookie("ratCrumpet", "jam", null, "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("jam"));
            ratDriver.ReplaceCookie("ratCrumpet", "honey");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratCrumpet"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("honey"));
            ratDriver.DeleteCookieNamed("ratCrumpet");

            ratDriver.DeleteAllCookies();
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Ignore("Currently not allowing a cookie access")]
        [Category("Chrome")]
        public void Chrome_CookieTests()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            ratDriver.WaitForElementToLoad(ratDriver.FindElementByCssSelector(".navbar-brand.tradewinds"));

            ratDriver.AddCookie("ratCookie", "ratData");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratData"));
            ratDriver.ReplaceCookie("ratCookie", "ratUpdate");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratUpdate"));
            ratDriver.DeleteCookieNamed("ratCookie");

            ratDriver.AddCookie("ratMuffin", "caramel", "/");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("caramel"));
            ratDriver.ReplaceCookie("ratMuffin", "blueberry");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratMuffin"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("blueberry"));
            ratDriver.DeleteCookieNamed("ratMuffin");

            ratDriver.AddCookie("ratBagel", "cheese", "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("cheese"));
            ratDriver.ReplaceCookie("ratBagel", "ham");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratBagel"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("ham"));
            ratDriver.DeleteCookieNamed("ratBagel");

            ratDriver.AddCookie("ratCrumpet", "jam", null, "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("jam"));
            ratDriver.ReplaceCookie("ratCrumpet", "honey");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratCrumpet"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("honey"));
            ratDriver.DeleteCookieNamed("ratCrumpet");

            ratDriver.DeleteAllCookies();
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_CookieTests()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            ratDriver.WaitForElementToLoad(ratDriver.FindElementByCssSelector(".navbar-brand.tradewinds"));

            ratDriver.AddCookie("ratCookie", "ratData");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratData"));
            ratDriver.ReplaceCookie("ratCookie", "ratUpdate");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratUpdate"));
            ratDriver.DeleteCookieNamed("ratCookie");

            ratDriver.AddCookie("ratMuffin", "caramel", "/");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("caramel"));
            ratDriver.ReplaceCookie("ratMuffin", "blueberry");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratMuffin"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("blueberry"));
            ratDriver.DeleteCookieNamed("ratMuffin");

            ratDriver.AddCookie("ratBagel", "cheese", "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("cheese"));
            ratDriver.ReplaceCookie("ratBagel", "ham");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratBagel"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("ham"));
            ratDriver.DeleteCookieNamed("ratBagel");

            ratDriver.AddCookie("ratCrumpet", "jam", null, "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("jam"));
            ratDriver.ReplaceCookie("ratCrumpet", "honey");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratCrumpet"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("honey"));
            ratDriver.DeleteCookieNamed("ratCrumpet");

            ratDriver.DeleteAllCookies();
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Ignore("Currently not allowing a cookie access")]
        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_CookieTests()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            ratDriver.WaitForElementToLoad(ratDriver.FindElementByCssSelector(".navbar-brand.tradewinds"));

            ratDriver.AddCookie("ratCookie", "ratData");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratData"));
            ratDriver.ReplaceCookie("ratCookie", "ratUpdate");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratUpdate"));
            ratDriver.DeleteCookieNamed("ratCookie");

            ratDriver.AddCookie("ratMuffin", "caramel", "/");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("caramel"));
            ratDriver.ReplaceCookie("ratMuffin", "blueberry");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratMuffin"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("blueberry"));
            ratDriver.DeleteCookieNamed("ratMuffin");

            ratDriver.AddCookie("ratBagel", "cheese", "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("cheese"));
            ratDriver.ReplaceCookie("ratBagel", "ham");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratBagel"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("ham"));
            ratDriver.DeleteCookieNamed("ratBagel");

            ratDriver.AddCookie("ratCrumpet", "jam", null, "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("jam"));
            ratDriver.ReplaceCookie("ratCrumpet", "honey");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratCrumpet"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("honey"));
            ratDriver.DeleteCookieNamed("ratCrumpet");

            ratDriver.DeleteAllCookies();
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_CookieTests()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            ratDriver.WaitForElementToLoad(ratDriver.FindElementByCssSelector(".navbar-brand.tradewinds"));

            ratDriver.AddCookie("ratCookie", "ratData");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratData"));
            ratDriver.ReplaceCookie("ratCookie", "ratUpdate");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratUpdate"));
            ratDriver.DeleteCookieNamed("ratCookie");

            ratDriver.AddCookie("ratMuffin", "caramel", "/");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("caramel"));
            ratDriver.ReplaceCookie("ratMuffin", "blueberry");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratMuffin"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("blueberry"));
            ratDriver.DeleteCookieNamed("ratMuffin");

            ratDriver.AddCookie("ratBagel", "cheese", "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("cheese"));
            ratDriver.ReplaceCookie("ratBagel", "ham");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratBagel"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("ham"));
            ratDriver.DeleteCookieNamed("ratBagel");

            ratDriver.AddCookie("ratCrumpet", "jam", null, "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("jam"));
            ratDriver.ReplaceCookie("ratCrumpet", "honey");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratCrumpet"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("honey"));
            ratDriver.DeleteCookieNamed("ratCrumpet");

            ratDriver.DeleteAllCookies();
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_CookieTests()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            ratDriver.WaitForElementToLoad(ratDriver.FindElementByCssSelector(".navbar-brand.tradewinds"));

            ratDriver.AddCookie("ratCookie", "ratData");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratData"));
            ratDriver.ReplaceCookie("ratCookie", "ratUpdate");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCookie").Value.Contains("ratUpdate"));
            ratDriver.DeleteCookieNamed("ratCookie");

            ratDriver.AddCookie("ratMuffin", "caramel", "/");
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("caramel"));
            ratDriver.ReplaceCookie("ratMuffin", "blueberry");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratMuffin"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratMuffin").Value.Contains("blueberry"));
            ratDriver.DeleteCookieNamed("ratMuffin");

            ratDriver.AddCookie("ratBagel", "cheese", "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("cheese"));
            ratDriver.ReplaceCookie("ratBagel", "ham");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratBagel"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratBagel").Value.Contains("ham"));
            ratDriver.DeleteCookieNamed("ratBagel");

            ratDriver.AddCookie("ratCrumpet", "jam", null, "/", DateTime.Now.AddDays(1));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("jam"));
            ratDriver.ReplaceCookie("ratCrumpet", "honey");
            Assert.IsTrue(ratDriver.CheckCookieExists("ratCrumpet"));
            Assert.IsTrue(ratDriver.GetCookieNamed("ratCrumpet").Value.Contains("honey"));
            ratDriver.DeleteCookieNamed("ratCrumpet");

            ratDriver.DeleteAllCookies();
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Ignore("Logs broken in Selenium 3.14. Will not be fixed until Selenium4")]
        [Category("Chrome")]
        public void Chrome_GetAvailableLogTypes()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            List<string> logTypes = ratDriver.GetAvailableLogTypes().ToList();
            Assert.IsTrue(logTypes[0] == "browser");
            Assert.IsTrue(logTypes[1] == "driver");
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetAvailableLogTypes()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsNull(ratDriver.GetAvailableLogTypes());
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetAvailableLogTypes()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsNull(ratDriver.GetAvailableLogTypes());
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetAvailableLogTypes()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Assert.IsNull(ratDriver.GetAvailableLogTypes());
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetAvailableLogTypes()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            List<string> logTypes = ratDriver.GetAvailableLogTypes().ToList();
            Assert.IsTrue(logTypes[0] == "browser");
            Assert.IsTrue(logTypes[1] == "driver");
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Ignore("Logs broken in Selenium 3.14. Will not be fixed until Selenium4")]
        [Category("Safari")]
        public void Safari_GetAvailableLogTypes()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            List<string> logTypes = ratDriver.GetAvailableLogTypes().ToList();
            Assert.IsTrue(logTypes[0] == "browser");
            Assert.IsTrue(logTypes[1] == "driver");
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Ignore("Logs broken in Selenium 3.14. Will not be fixed until Selenium4")]
        [Category("Chrome")]
        public void Chrome_GetBrowserLogEntries()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            var logEntries = ratDriver.GetAvailableLogEntries("browser");
            Assert.IsNotNull(logEntries);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Ignore("Logs broken in Selenium 3.14. Will not be fixed until Selenium4")]
        [Category("Chrome")]
        public void Chrome_GetDriverLogEntries()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            var logEntries = ratDriver.GetAvailableLogEntries("driver");
            Assert.IsNotNull(logEntries);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_SetImplicitWait()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.SetImplicitWait(0, 5, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_SetImplicitWait()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.SetImplicitWait(0, 5, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_SetImplicitWait()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.SetImplicitWait(0, 5, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_SetImplicitWait()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.SetImplicitWait(0, 5, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_SetImplicitWait()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.SetImplicitWait(0, 5, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_SetImplicitWait()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.SetImplicitWait(0, 5, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_SetPageLoadTimeout()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.SetPageLoadTimeout(0, 30, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_SetPageLoadTimeout()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.SetPageLoadTimeout(0, 30, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_SetPageLoadTimeout()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.SetPageLoadTimeout(0, 30, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_SetPageLoadTimeout()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.SetPageLoadTimeout(0, 30, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_SetPageLoadTimeout()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.SetPageLoadTimeout(0, 30, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_SetPageLoadTimeout()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.SetPageLoadTimeout(0, 30, 0);
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetWindowPosition()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowPosition();
            Assert.IsTrue(position.Item1 >= -10);
            Assert.IsTrue(position.Item2 >= -10);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_GetWindowPosition()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowPosition();
            Assert.IsTrue(position.Item1 >= 0);
            Assert.IsTrue(position.Item2 >= 0);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetWindowPosition()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowPosition();
            Assert.IsTrue(position.Item1 >= -20);
            Assert.IsTrue(position.Item2 >= -20);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetWindowPosition()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowPosition();
            Assert.IsTrue(position.Item1 >= -20);
            Assert.IsTrue(position.Item2 >= -20);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetWindowPosition()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowPosition();
            Assert.IsTrue(position.Item1 >= -20);
            Assert.IsTrue(position.Item2 >= -20);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_GetWindowPosition()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowPosition();
            Assert.IsTrue(position.Item1 >= -20);
            Assert.IsTrue(position.Item2 >= -20);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetWindowSize()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowSize();
            Assert.IsTrue(position.Item1 >= 0);
            Assert.IsTrue(position.Item2 >= 0);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_GetWindowSize()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowSize();
            Assert.IsTrue(position.Item1 >= 0);
            Assert.IsTrue(position.Item2 >= 0);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetWindowSize()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowSize();
            Assert.IsTrue(position.Item1 >= 0);
            Assert.IsTrue(position.Item2 >= 0);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetWindowSize()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowSize();
            Assert.IsTrue(position.Item1 >= 0);
            Assert.IsTrue(position.Item2 >= 0);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetWindowSize()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowSize();
            Assert.IsTrue(position.Item1 >= 0);
            Assert.IsTrue(position.Item2 >= 0);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_GetWindowSize()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            Tuple<int, int> position = ratDriver.GetWindowSize();
            Assert.IsTrue(position.Item1 >= 0);
            Assert.IsTrue(position.Item2 >= 0);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_ResizeWindow()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ResizeBrowserWindow(640, 480);
            Tuple<int, int> size = ratDriver.GetWindowSize();
            Assert.IsTrue(size.Item1 == 640);
            Assert.IsTrue(size.Item2 == 480);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_ResizeWindow()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ResizeBrowserWindow(640, 480);
            Tuple<int, int> size = ratDriver.GetWindowSize();
            Assert.IsTrue(size.Item1 == 640);
            Assert.IsTrue(size.Item2 == 480);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_ResizeWindow()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ResizeBrowserWindow(640, 480);
            Tuple<int, int> size = ratDriver.GetWindowSize();
            Assert.IsTrue(size.Item1 >= 690 || size.Item1 <= 694);
            Assert.IsTrue(size.Item2 >= 479 || size.Item2 <= 481);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_ResizeWindow()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ResizeBrowserWindow(640, 480);
            Tuple<int, int> size = ratDriver.GetWindowSize();
            Assert.IsTrue(size.Item1 == 640);
            Assert.IsTrue(size.Item2 == 480);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_ResizeWindow()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ResizeBrowserWindow(640, 480);
            Tuple<int, int> size = ratDriver.GetWindowSize();
            Assert.IsTrue(size.Item1 == 640);
            Assert.IsTrue(size.Item2 == 480);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_ResizeWindow()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.ResizeBrowserWindow(640, 480);
            Tuple<int, int> size = ratDriver.GetWindowSize();
            Assert.IsTrue(size.Item1 == 640);
            Assert.IsTrue(size.Item2 == 480);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_BrowserButtons()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<FirefoxDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressBackButton();
            devLink = ((RatDriver<FirefoxDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressForwardButton();
            devLink = ((RatDriver<FirefoxDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_BrowserButtons()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<ChromeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressBackButton();
            devLink = ((RatDriver<ChromeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressForwardButton();
            devLink = ((RatDriver<ChromeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_BrowserButtons()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<EdgeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressBackButton();
            devLink = ((RatDriver<EdgeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressForwardButton();
            devLink = ((RatDriver<EdgeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_BrowserButtons()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<InternetExplorerDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Thread.Sleep(250);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressBackButton();
            devLink = ((RatDriver<InternetExplorerDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressForwardButton();
            devLink = ((RatDriver<InternetExplorerDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_BrowserButtons()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByPartialLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressBackButton();
            devLink = ((RatDriver<OperaDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressForwardButton();
            devLink = ((RatDriver<OperaDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_BrowserButtons()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByPartialLinkText("Developments");
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            ratDriver.ClickLinkAndWait(devLink);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressBackButton();
            devLink = ((RatDriver<SafariDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.PressForwardButton();
            devLink = ((RatDriver<SafariDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_NavigateUsingUri()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<FirefoxDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_NavigateUsingUri()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<ChromeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_NavigateUsingUri()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<EdgeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_NavigateUsingUri()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<InternetExplorerDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_NavigateUsingUri()
        {
            ratDriver = new RatDriver<OperaDriver>();
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<OperaDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_NavigateUsingUri()
        {
            ratDriver = new RatDriver<SafariDriver>();
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<SafariDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            Assert.IsFalse(ratDriver.GetBrowserWindowUrl().Contains("developments"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetPageSource()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<FirefoxDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetPageSource().Length >= 256);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_GetPageSource()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<ChromeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetPageSource().Length >= 256);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetPageSource()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<EdgeDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetPageSource().Length >= 256);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetPageSource()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<InternetExplorerDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetPageSource().Length >= 256);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetPageSource()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<OperaDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetPageSource().Length >= 256);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_GetPageSource()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var devLink = ((RatDriver<SafariDriver>)ratDriver).Driver.FindElement(By.LinkText("Developments"));
            Assert.IsTrue(ratDriver.ElementExists(devLink));
            Assert.IsTrue(ratDriver.GetPageSource().Length >= 256);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_SwitchToWindow()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            //var pageOne = ratDriver.FindElementByTag("body");
            var window = ((RatDriver<FirefoxDriver>)ratDriver).Driver.CurrentWindowHandle;
            ratDriver.OpenNewView();
            ratDriver.NavigateToPage("http://www.google.com");
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("google"));
            ratDriver.SwitchToWindow(window);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_SwitchToWindow()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            //var pageOne = ratDriver.FindElementByTag("body");
            var window = ((RatDriver<ChromeDriver>)ratDriver).Driver.CurrentWindowHandle;
            ratDriver.OpenNewView();
            ratDriver.NavigateToPage("http://www.google.com");
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("google"));
            ratDriver.SwitchToWindow(window);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Ignore("Currently not allowing a java script open window command")]
        [Test]
        [Category("Edge")]
        public void Edge_SwitchToWindow()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var window = ((RatDriver<EdgeDriver>)ratDriver).Driver.CurrentWindowHandle;
            ratDriver.OpenNewView();
            ratDriver.NavigateToPage("http://www.google.com");
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("google"));
            ratDriver.SwitchToWindow(window);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Ignore("Currently not allowing a java script open window command")]
        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_SwitchToWindow()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var window = ((RatDriver<InternetExplorerDriver>)ratDriver).Driver.CurrentWindowHandle;
            ratDriver.OpenNewView();
            ratDriver.NavigateToPage("http://www.google.com");
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("google"));
            ratDriver.SwitchToWindow(window);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_SwitchToWindow()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var window = ((RatDriver<OperaDriver>)ratDriver).Driver.CurrentWindowHandle;
            ratDriver.OpenNewView();
            ratDriver.NavigateToPage("http://www.google.com");
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("google"));
            ratDriver.SwitchToWindow(window);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_SwitchToWindow()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var window = ((RatDriver<SafariDriver>)ratDriver).Driver.CurrentWindowHandle;
            ratDriver.OpenNewView();
            ratDriver.NavigateToPage("http://www.google.com");
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains("google"));
            ratDriver.SwitchToWindow(window);
            Assert.IsTrue(ratDriver.GetBrowserWindowUrl().Contains(WebsiteUrl));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetBrowserWindowTitle()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetBrowserWindowTitle();
            Assert.IsTrue(title.Contains("Totally Ratted Limited"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_GetBrowserWindowTitle()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetBrowserWindowTitle();
            Assert.IsTrue(title.Contains("Totally Ratted Limited"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetBrowserWindowTitle()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetBrowserWindowTitle();
            Assert.IsTrue(title.Contains("Totally Ratted Limited"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetBrowserWindowTitle()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetBrowserWindowTitle();
            Assert.IsTrue(title.Contains("Totally Ratted Limited"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetBrowserWindowTitle()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetBrowserWindowTitle();
            Assert.IsTrue(title.Contains("Totally Ratted Limited"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_GetBrowserWindowTitle()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetBrowserWindowTitle();
            Assert.IsTrue(title.Contains("Totally Ratted Limited"));
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Firefox")]
        public void Firefox_GetAllWindowHandles()
        {
            ratDriver = new RatDriver<FirefoxDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetAllWindowHandles();
            Assert.IsNotNull(title);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_GetAllWindowHandles()
        {
            ratDriver = new RatDriver<ChromeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetAllWindowHandles();
            Assert.IsNotNull(title);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_GetAllWindowHandles()
        {
            ratDriver = new RatDriver<EdgeDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetAllWindowHandles();
            Assert.IsNotNull(title);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_GetAllWindowHandles()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetAllWindowHandles();
            Assert.IsNotNull(title);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_GetAllWindowHandles()
        {
            ratDriver = new RatDriver<OperaDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetAllWindowHandles();
            Assert.IsNotNull(title);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_GetAllWindowHandles()
        {
            ratDriver = new RatDriver<SafariDriver>();
            ratDriver.NavigateToPage(WebsiteToTest);
            ratDriver.MaximiseView();
            var title = ratDriver.GetAllWindowHandles();
            Assert.IsNotNull(title);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_EnableEmulationMode()
        {
            string userAgent = "Mozilla/5.0 (Linux; U; Android 4.3; en-us; SM-N900T Build/JSS15J) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";
            ratDriver = new RatDriver<ChromeDriver>(360, 640, userAgent, 3.0d, true);
            ratDriver.NavigateToPage(WebsiteToTest);
        }




        [Test]
        [Category("Firefox")]
        public void Firefox_NavigateUsingUriWithPerformance()
        {
            ratDriver = new RatDriver<FirefoxDriver>(new FirefoxSettings(), true);
            Assert.That(((RatDriver<FirefoxDriver>)ratDriver).RatTimerCollection.Timings.Count == 2, Is.True);
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            Assert.That(((RatDriver<FirefoxDriver>)ratDriver).RatTimerCollection.Timings.Count == 3, Is.True);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.That(((RatDriver<FirefoxDriver>)ratDriver).RatTimerCollection.Timings.Count == 4, Is.True);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Chrome")]
        public void Chrome_NavigateUsingUriWithPerformance()
        {
            ratDriver = new RatDriver<ChromeDriver>(new ChromeSettings(), true);
            Assert.That(((RatDriver<ChromeDriver>)ratDriver).RatTimerCollection.Timings.Count == 2, Is.True);
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            Assert.That(((RatDriver<ChromeDriver>)ratDriver).RatTimerCollection.Timings.Count == 3, Is.True);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.That(((RatDriver<ChromeDriver>)ratDriver).RatTimerCollection.Timings.Count == 4, Is.True);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Edge")]
        public void Edge_NavigateUsingUriWithPerformance()
        {
            ratDriver = new RatDriver<EdgeDriver>(new EdgeSettings(), true);
            Assert.That(((RatDriver<EdgeDriver>)ratDriver).RatTimerCollection.Timings.Count == 2, Is.True);
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            Assert.That(((RatDriver<EdgeDriver>)ratDriver).RatTimerCollection.Timings.Count == 3, Is.True);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.That(((RatDriver<EdgeDriver>)ratDriver).RatTimerCollection.Timings.Count == 4, Is.True);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("InternetExplorer")]
        public void InternetExplorer_NavigateUsingUriWithPerformance()
        {
            ratDriver = new RatDriver<InternetExplorerDriver>(new InternetExplorerSettings(), true);
            Assert.That(((RatDriver<InternetExplorerDriver>)ratDriver).RatTimerCollection.Timings.Count == 2, Is.True);
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            Assert.That(((RatDriver<InternetExplorerDriver>)ratDriver).RatTimerCollection.Timings.Count == 3, Is.True);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.That(((RatDriver<InternetExplorerDriver>)ratDriver).RatTimerCollection.Timings.Count == 4, Is.True);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Opera")]
        public void Opera_NavigateUsingUriWithPerformance()
        {
            ratDriver = new RatDriver<OperaDriver>(new OperaSettings(), true);
            Assert.That(((RatDriver<OperaDriver>)ratDriver).RatTimerCollection.Timings.Count == 2, Is.True);
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            Assert.That(((RatDriver<OperaDriver>)ratDriver).RatTimerCollection.Timings.Count == 3, Is.True);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.That(((RatDriver<OperaDriver>)ratDriver).RatTimerCollection.Timings.Count == 4, Is.True);
            ratDriver.ClosePagesAndQuitDriver();
        }

        [Test]
        [Category("Safari")]
        public void Safari_NavigateUsingUriWithPerformance()
        {
            ratDriver = new RatDriver<SafariDriver>(new SafariSettings(), true);
            Assert.That(((RatDriver<SafariDriver>)ratDriver).RatTimerCollection.Timings.Count == 2, Is.True);
            Uri url = new Uri(WebsiteToTest);
            ratDriver.NavigateToPage(url);
            Assert.That(((RatDriver<SafariDriver>)ratDriver).RatTimerCollection.Timings.Count == 3, Is.True);
            ratDriver.MaximiseView();
            var devLink = ratDriver.FindElementByLinkText("Developments");
            Assert.That(((RatDriver<SafariDriver>)ratDriver).RatTimerCollection.Timings.Count == 4, Is.True);
            ratDriver.ClosePagesAndQuitDriver();
        }
    }
}

package org.liberator.ratdriver.tests;

import org.junit.After;
import org.junit.Assert;
import org.junit.Ignore;
import org.junit.Test;
import org.liberator.ratdriver.IRodent;
import org.liberator.ratdriver.RatDriver;
import org.liberator.ratdriver.enums.DriverType;
import org.liberator.ratdriver.enums.LocatorType;
import org.openqa.selenium.By;
import org.openqa.selenium.Dimension;
import org.openqa.selenium.Point;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.logging.LogEntry;

import java.util.List;
import java.util.Set;

public class SafariTests {

    IRodent ratDriver;

    String website = "http://localhost:80";
    String hostname = "localhost";
    
    @After
    public void cleanUp(){
        ratDriver.closePagesAndQuitDriver();
    }
    
    @Test
    public void testInstantiationSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.maximiseView();
        ratDriver.navigateToPage(website);
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains(hostname));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testClickLinkSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.maximiseView();
        ratDriver.navigateToPage(website);
        WebElement devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        ratDriver.clickLinkAndWait(devLink);
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains("developments"));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetTextSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        ratDriver.clickLinkAndWait(devLink);
        ratDriver.waitForElementToLoad(By.cssSelector(".col-md-12>h1"));
        String str = ratDriver.getElementText(By.cssSelector(".col-md-12>h1"), true);
        ratDriver.closePagesAndQuitDriver();
        Assert.assertTrue(str.contains("Totally Ratted Developments"));
    }

    @Test
    public void testCheckPageSourceSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        ratDriver.clickLinkAndWait(devLink);
        ratDriver.waitForElementToLoad(By.cssSelector(".col-md-12>h1"));
        String str = ratDriver.getPageSource();
        ratDriver.closePagesAndQuitDriver();
        Assert.assertTrue(str.contains("Totally Ratted Developments"));
    }

    @Ignore
    @Test
    public void testGetAvailableLogTypesSafari() {

        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        Set<String> logTypes = ratDriver.getAvailableLogTypes();
        Assert.assertTrue(logTypes.contains("browser"));
        Assert.assertTrue(logTypes.contains("driver"));
        Assert.assertTrue(logTypes.contains("client"));
        ratDriver.closePagesAndQuitDriver();
    }

/*    @Ignore
    @Test
    public void testGetBrowserLogEntriesSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        List<LogEntry> logEntries = ratDriver.getAvailableLogEntries("browser");
        Assert.assertNotNull(logEntries);
        ratDriver.closePagesAndQuitDriver();
    }*/

    @Test
    public void testSetImplicitWaitSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.setImplicitWait(0, 5, 0);
        ratDriver.navigateToPage(website);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testSetPageLoadTimeoutSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.setPageLoadTimeout(0, 30, 0);
        ratDriver.navigateToPage(website);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetWindowPositionSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        Point position = ratDriver.getWindowPosition();
        Assert.assertTrue(position.x >= 0);
        Assert.assertTrue(position.y >= 0);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetWindowSizeSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        Dimension position = ratDriver.getWindowSize();
        Assert.assertTrue(position.width >= 0);
        Assert.assertTrue(position.height >= 0);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testResizeWindowSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.resizeBrowserWindow(640, 480);
        Dimension size = ratDriver.getWindowSize();
        Assert.assertEquals(640, size.width);
        Assert.assertEquals(480, size.height);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testBrowserButtonsSafari() {
        WebElement devLink;
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        ratDriver.clickLinkAndWait(devLink);
        ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains("developments"));
        ratDriver.pressBackButton();
        Assert.assertFalse(ratDriver.getBrowserWindowUrl().contains("developments"));
        ratDriver.pressForwardButton();
        ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains("developments"));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetPageSourceSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, true);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        Assert.assertTrue(ratDriver.getPageSource().length() >= 256);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testSwitchToWindowSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        String window = ratDriver.getCurrentWindowHandle();
        ratDriver.openNewView();
        ratDriver.navigateToPage("http://www.google.com");
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains("google"));
        ratDriver.switchToWindow(window);
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains(hostname));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetBrowserWindowTitleSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        String title = ratDriver.getBrowserWindowTitle();
        Assert.assertTrue(title.contains("Totally Ratted Limited"));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetAllWindowHandlesSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        Set<String> handles = ratDriver.getAllWindowHandles();
        Assert.assertNotNull(handles);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testClickLinkAndWaitForPageSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement devLink = ratDriver.findElementByLinkText("Developments", false);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        ratDriver.clickLinkAndWaitForUrl(devLink, "developments");
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetElementAttributeSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementById("carousel-example", false);
        Assert.assertTrue(ratDriver.getElementAttribute(element, "data-ride", false).contains("carousel"));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindByClassNameSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementByClassName("carousel-inner", false);
        Assert.assertTrue(element.isDisplayed());
        Assert.assertTrue(element.isEnabled());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementsByClassNameSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findElementsByClassName("menutext", false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByClassNameSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement parent = ratDriver.findElementByCssSelector(".nav.navbar-nav.navbar-right", false);
        List<WebElement> elements = ratDriver.findSubElementsByClassName("menutext", parent, false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByClassNameLocatorSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findSubElementsByClassName("menutext", By.className("container"), false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindByIdSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementById("home", false);
        Assert.assertTrue(element.isDisplayed());
        Assert.assertTrue(element.isEnabled());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindByLinkTextSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementByLinkText("Developments", false);
        Assert.assertTrue(element.isEnabled());
        Assert.assertTrue(element.isDisplayed());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementsByLinkTextSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findElementsByLinkText("Read Details", false);
        Assert.assertEquals(6, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByLinkTextSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement parent = ratDriver.findElementById("just-intro", false);
        List<WebElement> elements = ratDriver.findSubElementsByLinkText("Read Details", parent, false);
        Assert.assertEquals(3, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByLinkTextLocatorSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        By parent = By.id("just-intro");
        List<WebElement> elements = ratDriver.findSubElementsByLinkText("Read Details", parent, false);
        Assert.assertEquals(3, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementByTagNameSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementByTag("body", false);
        Assert.assertNotNull(element);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementsByTagNameSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findElementsByTag("section", false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByTagNameSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement parent = ratDriver.findElementById("just-intro", false);
        List<WebElement> elements = ratDriver.findSubElementsByTag("a", parent, false);
        Assert.assertEquals(3, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByTagNameLocatorSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        By parent = By.id("just-intro");
        List<WebElement> elements = ratDriver.findSubElementsByTag("a", parent, false);
        Assert.assertEquals(3, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementByXPathSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementByXPath(".//*[@id='carousel-example']", false);
        Assert.assertTrue(element.isDisplayed());
        Assert.assertTrue(element.isEnabled());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementsByXPathSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findElementsByXPath(".//section", false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByXPathSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement parent = ratDriver.findElementByXPath("html/body", false);
        List<WebElement> elements = ratDriver.findSubElementsByXPath(".//section", parent, false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByXPathLocatorSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        By parent = By.xpath("html/body");
        List<WebElement> elements = ratDriver.findSubElementsByXPath(".//section", parent, false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testExtractElementFromCollectionByAttributeLocatorSafari() {
        ratDriver= new RatDriver(DriverType.SafariDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        By parent = By.xpath("html/body");
        WebElement element = ratDriver.extractElementFromCollectionByAttribute(parent, LocatorType.XPath, ".//section", "class", "note-sec", false);
        Assert.assertTrue(element.isDisplayed());
        Assert.assertTrue(element.isEnabled());
        ratDriver.closePagesAndQuitDriver();
    }
}

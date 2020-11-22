package org.liberator.ratdriver.tests;

import org.junit.After;
import org.junit.Assert;
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

public class OperaTests {

    IRodent ratDriver;

    String website = "http://localhost:80";
    String hostname = "localhost";

    @After
    public void cleanUp(){
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testInstantiationOpera(){
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains(hostname));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testClickLinkOpera(){
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        WebElement devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        ratDriver.clickLinkAndWait(devLink);
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains("developments"));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetTextOpera(){
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
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
    public void  testCheckPageSourceOpera(){
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
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


    @Test
    public void testGetAvailableLogTypesOpera(){

        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        Set<String> logTypes = ratDriver.getAvailableLogTypes();
        Assert.assertTrue(logTypes.contains("browser"));
        Assert.assertTrue(logTypes.contains("driver"));
        Assert.assertTrue(logTypes.contains("client"));
        ratDriver.closePagesAndQuitDriver();
    }


    @Test
    public  void testGetBrowserLogEntriesOpera(){
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        List<LogEntry> logEntries = ratDriver.getAvailableLogEntries("browser");
        Assert.assertNotNull(logEntries);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testSetImplicitWaitOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.setImplicitWait(0, 5, 0);
        ratDriver.navigateToPage(website);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testSetPageLoadTimeoutOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.setPageLoadTimeout(0, 30, 0);
        ratDriver.navigateToPage(website);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetWindowPositionOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        Point position = ratDriver.getWindowPosition();
        Assert.assertTrue(position.x >= -8);
        Assert.assertTrue(position.y >= -8);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetWindowSizeOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        Dimension position = ratDriver.getWindowSize();
        Assert.assertTrue(position.width >= 0);
        Assert.assertTrue(position.height >= 0);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testResizeWindowOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.resizeBrowserWindow(640, 480);
        Dimension size = ratDriver.getWindowSize();
        Assert.assertEquals(640, size.width);
        Assert.assertEquals(480, size.height);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testBrowserButtonsOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        ratDriver.clickLinkAndWait(devLink);
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains("developments"));
        ratDriver.pressBackButton();
        devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        Assert.assertFalse(ratDriver.getBrowserWindowUrl().contains("developments"));
        ratDriver.pressForwardButton();
        devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        Assert.assertTrue(ratDriver.getBrowserWindowUrl().contains("developments"));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetPageSourceOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, true);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement devLink = ratDriver.findElementByLinkText("Developments", true);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        Assert.assertTrue(ratDriver.getPageSource().length() >= 256);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testSwitchToWindowOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
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
    public void testGetBrowserWindowTitleOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        String title = ratDriver.getBrowserWindowTitle();
        Assert.assertTrue(title.contains("Totally Ratted Limited"));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetAllWindowHandlesOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        Set<String> handles = ratDriver.getAllWindowHandles();
        Assert.assertNotNull(handles);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testClickLinkAndWaitForPageOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement devLink = ratDriver.findElementByLinkText("Developments", false);
        Assert.assertTrue(ratDriver.elementExists(devLink));
        ratDriver.clickLinkAndWaitForUrl(devLink, "developments");
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testGetElementAttributeOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementById("carousel-example", false);
        Assert.assertTrue(ratDriver.getElementAttribute(element, "data-ride", false).contains("carousel"));
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindByClassNameOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementByClassName("carousel-inner", false);
        Assert.assertTrue(element.isDisplayed());
        Assert.assertTrue(element.isEnabled());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementsByClassNameOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findElementsByClassName("menutext", false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByClassNameOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement parent = ratDriver.findElementByCssSelector(".nav.navbar-nav.navbar-right", false);
        List<WebElement> elements = ratDriver.findSubElementsByClassName("menutext", parent, false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByClassNameLocatorOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findSubElementsByClassName("menutext", By.className("container"), false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindByIdOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementById("home", false);
        Assert.assertTrue(element.isDisplayed());
        Assert.assertTrue(element.isEnabled());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindByLinkTextOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementByLinkText("Developments", false);
        Assert.assertTrue(element.isEnabled());
        Assert.assertTrue(element.isDisplayed());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementsByLinkTextOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findElementsByLinkText("Read Details", false);
        Assert.assertEquals(6, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByLinkTextOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement parent = ratDriver.findElementById("just-intro", false);
        List<WebElement> elements = ratDriver.findSubElementsByLinkText("Read Details", parent, false);
        Assert.assertEquals(3, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByLinkTextLocatorOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        By parent = By.id("just-intro");
        List<WebElement> elements = ratDriver.findSubElementsByLinkText("Read Details", parent, false);
        Assert.assertEquals(3, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementByTagNameOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementByTag("body", false);
        Assert.assertNotNull(element);
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementsByTagNameOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findElementsByTag("section", false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByTagNameOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement parent = ratDriver.findElementById("just-intro", false);
        List<WebElement> elements = ratDriver.findSubElementsByTag("a", parent, false);
        Assert.assertEquals(3, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByTagNameLocatorOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        By parent = By.id("just-intro");
        List<WebElement> elements = ratDriver.findSubElementsByTag("a", parent, false);
        Assert.assertEquals(3, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementByXPathOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement element = ratDriver.findElementByXPath(".//*[@id='carousel-example']", false);
        Assert.assertTrue(element.isDisplayed());
        Assert.assertTrue(element.isEnabled());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindElementsByXPathOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        List<WebElement> elements = ratDriver.findElementsByXPath(".//section", false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByXPathOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        WebElement parent = ratDriver.findElementByXPath("html/body", false);
        List<WebElement> elements = ratDriver.findSubElementsByXPath(".//section", parent, false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testFindSubElementsByXPathLocatorOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        By parent = By.xpath("html/body");
        List<WebElement> elements = ratDriver.findSubElementsByXPath(".//section", parent, false);
        Assert.assertEquals(5, elements.size());
        ratDriver.closePagesAndQuitDriver();
    }

    @Test
    public void testExtractElementFromCollectionByAttributeLocatorOpera()
    {
        ratDriver = new RatDriver(DriverType.OperaDriver, false);
        ratDriver.navigateToPage(website);
        ratDriver.maximiseView();
        By parent = By.xpath("html/body");
        WebElement element = ratDriver.extractElementFromCollectionByAttribute(parent, LocatorType.XPath, ".//section", "class", "note-sec", false);
        Assert.assertTrue(element.isDisplayed());
        Assert.assertTrue(element.isEnabled());
        ratDriver.closePagesAndQuitDriver();
    }
}

package org.liberator.ratdriver;

import com.sun.javafx.PlatformUtil;
import lombok.Getter;
import lombok.Setter;
import org.liberator.ratdriver.control.*;
import org.liberator.ratdriver.entities.DomRectangle;
import org.liberator.ratdriver.entities.ElementSize;
import org.liberator.ratdriver.entities.HeightWidth;
import org.liberator.ratdriver.enums.*;
import org.liberator.ratdriver.performance.RatWatch;
import org.liberator.ratdriver.preferences.BasePreferences;
import org.liberator.ratdriver.settings.BaseSettings;
import org.openqa.selenium.*;
import org.openqa.selenium.firefox.FirefoxProfile;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.interactions.MoveTargetOutOfBoundsException;
import org.openqa.selenium.logging.LogEntries;
import org.openqa.selenium.logging.LogEntry;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.io.File;
import java.util.*;
import java.util.concurrent.TimeUnit;


@SuppressWarnings("unused")
public class RatDriver implements IRatDriver {

    //region Instance Variables

    /**
     * Gets the unique Id of the RatDriver instance
     */
    @Getter
    @Setter
    public UUID Id;

    //endregion

    //region Public Properties

    /**
     * The current WebDriver instance
     */
    @Getter
    @Setter
    public WebDriver Driver;

    /**
     * Text name for the driver
     */
    @Getter
    @Setter
    public String DriverName;

    /**
     * The current WebElement under scrutiny
     */
    @Getter
    @Setter
    public WebElement Element;

    /**
     * The locator for a WebElement under scrutiny
     */
    @Getter
    @Setter
    public By Locator;

    /**
     * A list of Elements returned to the user by a query
     */
    @Getter
    @Setter
    public List<WebElement> Elements;

    /**
     * A list of Locators returned to the user by a query
     */
    @Getter
    @Setter
    public List<By> Locators;

    /**
     * The last exception thrown by the driver
     */
    @Getter
    @Setter
    public Exception LastException;

    /**
     * The currently selected Firefox Profile
     */
    @Getter
    @Setter
    public FirefoxProfile FireFoxProfile;

    /**
     * The last page opened by this incarnation of the driver
     */
    @Getter
    @Setter
    public WebElement LastPage;

    /**
     * The current collection of windows by handle
     */
    @Getter
    @Setter
    public Dictionary<String, String> WindowHandles;

    /**
     * Contains timers for
     */
    private RatWatch RatTimerCollection;

    /**
     * Whether to record performance statistics
     */
    @Getter
    @Setter
    public Boolean RecordPerformance;

    /**
     *
     */
    @Getter
    @Setter
    private WebDriver EncapsulatedDriver;

    //endregion


    //region Constructors

    public RatDriver(DriverType type) {
        try {

            if (RecordPerformance) {
                initialiseRatWatch(false);
                RatTimerCollection.StartTimer();
            }

            establishDriverType(type, null);

            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.Instantiation);
            }


            WindowHandles.put(EncapsulatedDriver.getTitle(), EncapsulatedDriver.getWindowHandle());
        } catch (Exception ex) {
            System.out.println("An unexpected error has been detected.");
        }
    }

    public RatDriver(DriverType type, BasePreferences preferences) {
        try {
            if (RecordPerformance) {
                initialiseRatWatch(false);
                RatTimerCollection.StartTimer();
            }
            establishDriverType(type, preferences);
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.Instantiation);
            }

            WindowHandles.put(EncapsulatedDriver.getTitle(), EncapsulatedDriver.getWindowHandle());
        } catch (Exception ex) {
            System.out.println("An unexpected error has been detected.");
        }
    }

    public RatDriver(DriverType type, BasePreferences preferences, Boolean performanceTimings) {
        try {
            if (RecordPerformance) {
                initialiseRatWatch(performanceTimings);
                RatTimerCollection.StartTimer();
            }
            establishDriverType(type, preferences);
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.Instantiation);
            }

            WindowHandles.put(EncapsulatedDriver.getTitle(), EncapsulatedDriver.getWindowHandle());
        } catch (Exception ex) {
            System.out.println("An unexpected error has been detected.");
        }
    }

    public RatDriver(DriverType type, Boolean performanceTimings) {
        String title;
        WindowHandles = new Hashtable<>();
        try {
            RecordPerformance = performanceTimings;
            if (RecordPerformance) {
                initialiseRatWatch(true);
                RatTimerCollection.StartTimer();
            }

            establishDriverType(type, null);

            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.Instantiation);
            }

            if (EncapsulatedDriver.getTitle().length() == 0) {
                title = "empty page";
            } else {
                title = EncapsulatedDriver.getTitle();
            }
            WindowHandles.put(title, EncapsulatedDriver.getWindowHandle());
        } catch (Exception ex) {
            System.out.println("An unexpected error has been detected.");
        }
    }

    //endregion


    /**
     * Moves the virtual cursor position to a WebElement which acts as a menu and hovers
     *
     * @param element The WebElement acting as a menu
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override

    public void hoverOverMenu(WebElement element, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "hoverOverMenu", "Unable to hover over the named menu");
        }
    }

    /**
     * Moves the virtual cursor position to a WebElement which acts as a menu and hovers
     *
     * @param locator The locator for the WebElement acting as a menu
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void hoverOverMenu(By locator, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "hoverOverMenu", "Unable to hover over the named menu");
        }
    }

    /**
     * Clicks on a WebElement acting as a menu and continues to hover
     *
     * @param element The WebElement acting as a menu<
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void clickAndHoverOverMenu(WebElement element, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(element);
                clickLink(element, wait);
            } else {
                clickLink(element, null);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickAndHoverOverMenu", "Unable to click and hover over the named menu");
        }
    }

    /**
     * Clicks on a WebElement acting as a menu and continues to hover
     *
     * @param locator The locator for the WebElement acting as a menu
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void clickAndHoverOverMenu(By locator, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).click(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickAndHoverOverMenu", "Unable to click and hover over the named menu");
        }
    }

    /**
     * Clicks on a WebElement acting as a menu and continues to hold
     *
     * @param element The WebElement acting as a menu
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void clickAndHoldMenu(WebElement element, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToBeClickable(Element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).clickAndHold(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickAndHoldMenu", "Unable to click and hold over the named menu");
        }
    }

    /**
     * Clicks on a WebElement acting as a menu and continues to hold
     *
     * @param locator The locator for the WebElement acting as a menu
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void clickAndHoldMenu(By locator, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToBeClickable(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).clickAndHold(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickAndHoldMenu", "Unable to click and hold over the named menu");
        }
    }

    /**
     * Clicks on an element and displays a contextual menu
     *
     * @param element The WebElement which is the target of the contextual menu click
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void clickContextualMenu(WebElement element, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToBeClickable(element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).contextClick(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickContextualMenu", "Unable to click the contextual menu.");
        }
    }

    /**
     * Clicks on an element and displays a contextual menu
     *
     * @param locator The locator for the WebElement which is the target of the contextual menu click
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void clickContextualMenu(By locator, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToBeClickable(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).contextClick(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickContextualMenu", "Unable to click the contextual menu.");
        }
    }

    /**
     * Double clicks on a WebElement
     *
     * @param element The WebElement on which a double click is required
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void doubleClick(WebElement element, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToBeClickable(element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).doubleClick(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "doubleClick", "Unable to double click the element.");
        }
    }

    /**
     * Double clicks on a WebElement
     *
     * @param locator The locator for the WebElement which is the target of a double click
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void doubleClick(By locator, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToBeClickable(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).doubleClick(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "doubleClick", "Unable to double click the element.");
        }
    }

    /**
     * Drags a WebElement over a target WebElement and drops it
     *
     * @param source        The Web Element being dragged
     * @param target        The target WebElement for the action
     * @param waitForSource Whether to wait for the source element to reach a known condition
     * @param waitForTarget Whether to wait for the target element to reach a known condition
     */
    @Override
    public void dragAndDrop(WebElement source, WebElement target, Boolean waitForSource, Boolean waitForTarget) {
        try {
            Element = source;
            if (waitForSource == null || waitForSource) {
                waitForElementToLoad(Element);
            }
            if (waitForTarget == null || waitForTarget) {
                waitForElementToLoad(target);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).dragAndDrop(Element, target).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "dragAndDrop", "Unable to drag and drop the element.");
        }
    }

    /**
     * Drags a WebElement over a target WebElement and drops it
     *
     * @param source        The locator for the Web Element being dragged
     * @param target        The locator for the target WebElement for the action
     * @param waitForSource Whether to wait for the source element to reach a known condition
     * @param waitForTarget Whether to wait for the target element to reach a known condition
     */
    @Override
    public void dragAndDrop(By source, By target, Boolean waitForSource, Boolean waitForTarget) {
        try {
            Locator = source;
            if (waitForSource == null || waitForSource) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Locator = target;
            if (waitForTarget == null || waitForTarget) {
                waitForElementToLoad(Locator);
            }
            WebElement targetElement = (EncapsulatedDriver).findElement(target);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).dragAndDrop(Element, targetElement).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "dragAndDrop", "Unable to drag and drop the element.");
        }
    }

    /**
     * Drags a WebElement and drops it at an offset position
     *
     * @param element The WebElement being dragged
     * @param xOffset The x coordinate of the offset position
     * @param yOffset The y coordinate of the offset position
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void dragAndDropToOffset(WebElement element, int xOffset, int yOffset, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).dragAndDropBy(Element, xOffset, yOffset).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "dragAndDropToOffset", "Unable to drag and drop the element.");
        }
    }

    /**
     * Drags a WebElement and drops it at an offset position
     *
     * @param locator The locator for the WebElement being dragged
     * @param xOffset The x coordinate of the offset position
     * @param yOffset The y coordinate of the offset position
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void dragAndDropToOffset(By locator, int xOffset, int yOffset, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).dragAndDropBy(Element, xOffset, yOffset).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "dragAndDropToOffset", "Unable to drag and drop the element.");
        }
    }

    /**
     * Moves the virtual cursor to a WebElement and presses a key
     *
     * @param element The WebElement on which the key is to be pressed
     * @param key     The key to be pressed
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void keyDownOnElement(WebElement element, Keys key, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).keyDown(Element, key).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "keyDownOnElement", "Unable to key down on the element.");
        }
    }

    /**
     * Moves the virtual cursor to a WebElement and presses a key
     *
     * @param locator The locator for the WebElement on which the key is to be pressed
     * @param key     The key to be pressed
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void keyDownOnElement(By locator, Keys key, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).keyDown(Element, key).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "keyDownOnElement", "Unable to key down on the element.");
        }
    }

    /**
     * Releases a depressed key which a WebElement is selected
     *
     * @param element The target WebElement for the key release
     * @param key     The key to be released
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void keyUpOnElement(WebElement element, Keys key, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).keyUp(Element, key).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "keyUpOnElement", "Unable to key up on the element.");
        }
    }

    /**
     * Releases a depressed key which a WebElement is selected
     *
     * @param locator The locator for the target WebElement for the key release
     * @param key     The key to be released
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void keyUpOnElement(By locator, Keys key, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).keyUp(Element, key).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "keyUpOnElement", "Unable to key up on the element.");
        }
    }

    /**
     * Moves the virtual cursor by an offset from a WebElement
     *
     * @param element The WebElement from which the cursor will move
     * @param xOffset The x coordinate offset for the movement
     * @param yOffset The y coordinate offset for the movement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void moveByOffset(WebElement element, int xOffset, int yOffset, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveByOffset(xOffset, yOffset).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "moveByOffset", "Unable to move by offset.");
        }
    }

    /**
     * Moves the virtual cursor by an offset from a WebElement
     *
     * @param locator The locator for the WebElement from which the cursor will move
     * @param xOffset The x coordinate offset for the movement
     * @param yOffset The y coordinate offset for the movement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void moveByOffset(By locator, int xOffset, int yOffset, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveByOffset(xOffset, yOffset).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "moveByOffset", "Unable to move by offset.");
        }
    }

    /**
     * Moves the virtual cursor by an offset from a WebElement
     *
     * @param element The WebElement from which the cursor will move<
     * @param xOffset The x coordinate offset for the movement
     * @param yOffset The y coordinate offset for the movement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void moveToElementWithOffset(WebElement element, int xOffset, int yOffset, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).moveByOffset(xOffset, yOffset).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "moveToElementWithOffset", "Unable to move by offset.");
        }
    }

    /**
     * Moves the virtual cursor by an offset from a WebElement
     *
     * @param locator The locator for the WebElement from which the cursor will move
     * @param xOffset The x coordinate offset for the movement
     * @param yOffset The y coordinate offset for the movement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void moveToElementWithOffset(By locator, int xOffset, int yOffset, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).moveByOffset(xOffset, yOffset).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "moveToElementWithOffset", "Unable to move by offset.");
        }
    }

    /**
     * Releases the mouse button over a WebElement
     *
     * @param element The WebElement over which to release the mouse button
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void releaseMouseButton(WebElement element, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).release(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "releaseMouseButton", "Unable to send a mouse button release.");
        }
    }

    /**
     * Releases the mouse button over a WebElement
     *
     * @param locator The locator for the WebElement over which to release the mouse button
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void releaseMouseButton(By locator, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).release(Element).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "releaseMouseButton", "Unable to send a mouse button release.");
        }
    }

    /**
     * Sends a sequence of keystrokes to the WebElement specified
     *
     * @param element The WebElement that receives the keystrokes
     * @param text    The keystrokes that are to be sent to the WebElement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void sendValueToField(WebElement element, Keys text, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).sendKeys(Element, text).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "sendValueToField", "Unable to send the value to the field.");
        }
    }

    /**
     * Sends a sequence of keystrokes to the WebElement specified
     *
     * @param locator The locator for the WebElement that receives the keystrokes
     * @param text    The keystrokes that are to be sent to the WebElement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void sendValueToField(By locator, Keys text, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(Locator);
            }
            Element = (EncapsulatedDriver).findElement(Locator);
            Actions actions = new Actions(EncapsulatedDriver);
            actions.moveToElement(Element).sendKeys(Element, text).build().perform();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "sendValueToField", "Unable to send the value to the field.");
        }
    }

    /**
     * Selects an item from a dropdown by text value
     *
     * @param element The dropdown menu
     * @param item    The item to choose
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void selectItemFromDropdown(WebElement element, String item, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToBeClickable(Element);
            }
            Select select = new Select(Element);
            select.selectByVisibleText(item);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "selectItemFromDropdown", "Unable to select the item from the dropdown.");
        }
    }

    /**
     * Selects an item from a dropdown by text value
     *
     * @param locator The locator for the dropdown menu
     * @param item    The item to choose
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void selectItemFromDropdown(By locator, String item, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToBeClickable(Locator);
            }
            Element = EncapsulatedDriver.findElement(locator);
            Select select = new Select(Element);
            select.selectByVisibleText(item);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "selectItemFromDropdown", "Unable to select the item from the dropdown.");
        }
    }

    /**
     * Selects an item from a dropdown by row number
     *
     * @param element The dropdown menu
     * @param row     The row number to choose
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void selectRowFromDropdown(WebElement element, int row, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            Select select = new Select(Element);
            select.selectByIndex(row);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "selectRowFromDropdown", "Unable to select the item from the dropdown.");
        }
    }

    /**
     * Selects an item from a dropdown by text value
     *
     * @param locator The locator for the dropdown menu
     * @param row     The row number to choose
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void selectRowFromDropdown(By locator, int row, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToBeClickable(Locator);
            }
            Element = EncapsulatedDriver.findElement(locator);
            Select select = new Select(Element);
            select.selectByIndex(row);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "selectRowFromDropdown", "Unable to select the row from the dropdown.");
        }
    }

    /**
     * Selects an item from a dropdown by text value
     *
     * @param element The dropdown menu
     * @param value   The value to choose
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void selectValueFromDropdown(WebElement element, String value, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            Select select = new Select(Element);
            select.selectByValue(value);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "selectValueFromDropdown", "Unable to select the value from the dropdown.");
        }
    }

    /**
     * Selects an item from a dropdown by text value
     *
     * @param locator The locator for the dropdown menu
     * @param value   The value to choose
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void selectValueFromDropdown(By locator, String value, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToBeClickable(Locator);
            }
            Element = EncapsulatedDriver.findElement(locator);
            Select select = new Select(Element);
            select.selectByValue(value);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "selectValueFromDropdown", "Unable to select the item from the dropdown.");
        }
    }

    /**
     * Gets a reference to the encapsulated WebDriver
     *
     * @return The encapsulated IWebDriver
     */
    @Override
    public WebDriver returnEncapsulatedDriver() {
        try {
            EncapsulatedDriver = Driver;
            EncapsulatedDriver.manage().timeouts().pageLoadTimeout(BaseSettings.PageLoad, TimeUnit.SECONDS);
            EncapsulatedDriver.manage().timeouts().implicitlyWait(BaseSettings.ImplicitWait, TimeUnit.SECONDS);
            EncapsulatedDriver.manage().timeouts().setScriptTimeout(BaseSettings.AsyncJavaScript, TimeUnit.SECONDS);
            return EncapsulatedDriver;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "returnEncapsulatedDriver", "Could not return encapsulated driver.");
            return null;
        }
    }

    /**
     * Checks if a cookie exists
     *
     * @param cookieName The name of the cookie to check for
     * @return A Boolean value indicating whether the cookie exists
     */
    @Override
    public Boolean doesCookieExist(String cookieName) {
        try {
            EncapsulatedDriver.manage().getCookieNamed(cookieName);
            System.out.println("Named cookie exists");
            return true;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "doesCookieExist", "Named cookie does not exist");
            return false;
        }
    }

    /**
     * Adds a cookie to the browser
     *
     * @param name  The name of the cookie to add
     * @param value The value set within the cookie
     */
    @Override
    public void addCookie(String name, String value) {
        try {
            Cookie cookie = new Cookie(name, value);
            EncapsulatedDriver.manage().addCookie(cookie);
            System.out.println("Named cookie was added.");
        } catch (Exception ex) {
            System.out.println("Named cookie was not added.");
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "addCookie", "Named cookie was not added.");
        }
    }

    /**
     * Adds a cookie to the browser
     *
     * @param name  The name of the cookie to add
     * @param value The value set within the cookie
     * @param path  The path of the cookie
     */
    @Override
    public void addCookie(String name, String value, String path) {
        try {
            Cookie cookie = new Cookie(name, value, path);
            EncapsulatedDriver.manage().addCookie(cookie);
            System.out.println("Named cookie was added.");
        } catch (Exception ex) {
            System.out.println("Named cookie was not added.");
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "addCookie", "Named cookie was not added.");
        }
    }

    /**
     * Adds a cookie to the browser
     *
     * @param name   The name of the cookie to add
     * @param value  The value set within the cookie
     * @param path   The path of the cookie
     * @param expiry The date and time at which the cookie expires
     */
    @Override
    public void addCookie(String name, String value, String path, Date expiry) {
        try {
            Cookie cookie = new Cookie(name, value, path, expiry);
            EncapsulatedDriver.manage().addCookie(cookie);
            System.out.println("Named cookie was added.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "addCookie", "Named cookie was not added.");
        }
    }

    /**
     * Adds a cookie to the browser
     *
     * @param name   The name of the cookie to add
     * @param value  The value set within the cookie
     * @param domain The domain for which the cookie is being added
     * @param path   The path of the cookie
     * @param expiry The date and time at which the cookie expires
     */
    @Override
    public void addCookie(String name, String value, String domain, String path, Date expiry) {
        try {
            Cookie cookie = new Cookie(name, value, domain, path, expiry);
            EncapsulatedDriver.manage().addCookie(cookie);
            System.out.println("Named cookie was added.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "addCookie", "Named cookie was not added.");
        }
    }

    /**
     * Adds a predefined cookie to the current browser session
     *
     * @param cookie The cookie to be added
     */
    @Override
    public void addCookie(Cookie cookie) {
        try {
            EncapsulatedDriver.manage().addCookie(cookie);
            System.out.println("Named cookie was added.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "addCookie", "Named cookie was not added.");
        }
    }

    /**
     * Gets a list of cookies currently saved by the browser
     *
     * @return The list of cookies
     */
    @Override
    public Set<Cookie> getCookies() {
        try {
            Set<Cookie> cookies = EncapsulatedDriver.manage().getCookies();
            System.out.println("Cookie set returned.");
            return cookies;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getCookies", "Could not return the cookies.");
            return null;
        }
    }

    /**
     * Deletes all cookies currently found in the browser
     */
    @Override
    public void deleteAllCookies() {
        try {
            EncapsulatedDriver.manage().deleteAllCookies();
            System.out.println("All cookies deleted.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "deleteAllCookies", "Could not delete all cookies.");
        }
    }

    /**
     * Deletes a cookie, given a copy of that cookie
     *
     * @param cookie The definition of the cookie to be deleted
     */
    @Override
    public void deleteCookie(Cookie cookie) {
        try {
            EncapsulatedDriver.manage().deleteCookie(cookie);
            System.out.println("Cookie was deleted.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "deleteCookieNamed", "Cookie was not deleted.");
        }
    }

    /**
     * Deletes a named cookie
     *
     * @param cookie The name of the cookie
     */
    @Override
    public void deleteCookieNamed(String cookie) {
        try {
            EncapsulatedDriver.manage().deleteCookieNamed(cookie);
            System.out.println("Cookie was deleted.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "deleteCookieNamed", "Cookie was not deleted.");
        }
    }

    /**
     * Gets a cookie from the browser
     *
     * @param cookie The name of the cookie to find
     * @return The named cookie
     */
    @Override
    public Cookie getCookieNamed(String cookie) {
        try {
            Cookie returnedCookie = EncapsulatedDriver.manage().getCookieNamed(cookie);
            System.out.println("Cookie was returned.");
            return returnedCookie;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getCookieNamed", "Cookie could not be returned.");
            return null;
        }
    }

    /**
     * Deletes and then replaces a named cookie
     *
     * @param name  The name of the cookie
     * @param value The value to set within the cookie
     */
    @Override
    public void replaceCookie(String name, String value) {
        try {
            if (doesCookieExist(name)) {
                deleteCookieNamed(name);
            }
            addCookie(name, value);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "replaceCookie", "Cookie could not be replaced.");
        }
    }

    /**
     * Deletes and then replaces a named cookie
     *
     * @param name  The name of the cookie
     * @param value The value to set within the cookie
     * @param path  The path of the cookie
     */
    @Override
    public void replaceCookie(String name, String value, String path) {
        try {
            if (doesCookieExist(name)) {
                deleteCookieNamed(name);
            }
            addCookie(name, value, path);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "replaceCookie", "Cookie could not be replaced.");
        }
    }

    /**
     * Deletes and then replaces a named cookie
     *
     * @param name   The name of the cookie
     * @param value  The value to set within the cookie
     * @param path   The path of the cookie
     * @param expiry The date and time at which the cookie expires
     */
    @Override
    public void replaceCookie(String name, String value, String path, Date expiry) {
        try {
            if (doesCookieExist(name)) {
                deleteCookieNamed(name);
            }
            addCookie(name, value, path, expiry);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "replaceCookie", "Cookie could not be replaced.");
        }
    }

    /**
     * Deletes and then replaces a named cookie
     *
     * @param name   The name of the cookie
     * @param value  The value to set within the cookie
     * @param domain The domain for which the cookie is being added
     * @param path   The path of the cookie
     * @param expiry The date and time at which the cookie expires
     */
    @Override
    public void replaceCookie(String name, String value, String domain, String path, Date expiry) {
        try {
            if (doesCookieExist(name)) {
                deleteCookieNamed(name);
            }
            addCookie(name, value, domain, path, expiry);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "replaceCookie", "Cookie could not be replaced.");
        }
    }

    /**
     * Gets a list of available log types
     *
     * @return The list of log types
     */
    @Override
    public Set<String> getAvailableLogTypes() {
        try {
            Set<String> logTypes = EncapsulatedDriver.manage().logs().getAvailableLogTypes();
            System.out.println("Returning log types.");
            return logTypes;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getAvailableLogTypes", "Could not return log types.");
            return null;
        }
    }

    /**
     * Gets the available log entries for a particular log type
     *
     * @param logKind The type of log to consult
     * @return The list of log entries
     */
    @Override
    public List<LogEntry> getAvailableLogEntries(String logKind) {
        try {
            LogEntries log = EncapsulatedDriver.manage().logs().get(logKind);
            System.out.println("Returning log entries.");
            return log.getAll();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getAvailableLogEntries", "Could not return log entries.");
            return null;
        }
    }

    /**
     * Sets the implicit wait via a method.
     *
     * @param minutes      The number of minutes in the  implicit wait
     * @param seconds      The number of seconds in the  implicit wait
     * @param milliseconds The number of milliseconds in the  implicit wait
     */
    @Override
    public void setImplicitWait(int minutes, int seconds, int milliseconds) {
        try {
            long millis = (minutes * 60 + seconds) * 1000 + milliseconds;
            EncapsulatedDriver.manage().timeouts().implicitlyWait(millis, TimeUnit.MILLISECONDS);
            System.out.println("Set implicit timeout.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "setImplicitWait", "Could not set implicit timeout.");
        }
    }

    /**
     * Sets the page load timeout via a method.
     *
     * @param minutes      The number of minutes in the  page load timeout
     * @param seconds      The number of seconds in the  page load timeout
     * @param milliseconds The number of milliseconds in the page load timeout
     */
    @Override
    public void setPageLoadTimeout(int minutes, int seconds, int milliseconds) {
        try {
            long millis = (minutes * 60 + seconds) * 1000 + milliseconds;
            EncapsulatedDriver.manage().timeouts().pageLoadTimeout(millis, TimeUnit.MILLISECONDS);
            System.out.println("Set page load timeout.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "setPageLoadTimeout", "Could not set page load timeout.");
        }
    }

    /**
     * Sets the script timeout via a method.
     *
     * @param minutes      The number of minutes in the script timeout
     * @param seconds      The number of seconds in the script timeout
     * @param milliseconds The number of milliseconds in the script timeout
     */
    @Override
    public void setScriptTimeout(int minutes, int seconds, int milliseconds) {
        try {
            long millis = (minutes * 60 + seconds) * 1000 + milliseconds;
            EncapsulatedDriver.manage().timeouts().setScriptTimeout(millis, TimeUnit.MILLISECONDS);
            System.out.println("Set script timeout.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "setScriptTimeout", "Could not set script timeout.");
        }
    }

    /**
     * Maximises the current window
     */
    @Override
    public void maximiseView() {
        try {
            EncapsulatedDriver.manage().window().maximize();
            System.out.println("Browser window was maximised.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "maximiseView", "Browser window could not be maximised.");
        }
    }

    /**
     * Gets the current window position
     *
     * @return An object containing the x and y coordinates of the top left corner of the window
     */
    @Override
    public Point getWindowPosition() {
        try {
            Point point = EncapsulatedDriver.manage().window().getPosition();
            System.out.println("Browser window was maximised.");
            return point;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getWindowPosition", "Browser window could not be maximised.");
            return null;
        }
    }

    /**
     * Gets the current window size
     *
     * @return An object containing the width and height of the current window
     */
    @Override
    public Dimension getWindowSize() {
        try {
            Dimension dimension = EncapsulatedDriver.manage().window().getSize();
            System.out.println("Browser size was returned.");
            return dimension;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getWindowSize", "Browser size could not be returned.");
            return null;
        }
    }

    /**
     * Resize the browser window to the assigned width and height
     *
     * @param width  The width to assign to the browser
     * @param height The height to assign to the browser
     */
    @Override
    public void resizeBrowserWindow(int width, int height) {
        try {
            Dimension dimension = new Dimension(width, height);
            EncapsulatedDriver.manage().window().setSize(dimension);
            System.out.println("Browser was resized.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "resizeBrowserWindow", "Browser could not be resized.");
        }
    }

    /**
     * Presses the back button of the browser
     */
    @Override
    public void pressBackButton() {
        try {
            EncapsulatedDriver.navigate().back();
            System.out.println("Back button was pressed.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "pressBackButton", "Back button could not be pressed.");
        }
    }

    /**
     * Presses the forward button of the browser
     */
    @Override
    public void pressForwardButton() {
        try {
            EncapsulatedDriver.navigate().forward();
            System.out.println("Forward button was pressed.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "pressForwardButton", "Forward button could not be pressed.");
        }
    }

    /**
     * Navigates to a particular website by URL
     *
     * @param url The URL of the website to load in the browser
     */
    @Override
    public void navigateToPage(String url) {
        try {
            if (!DriverName.toLowerCase().contains("internetexplorer")) {
                EncapsulatedDriver.navigate().to(url);
            } else {
                EncapsulatedDriver.get(url);
            }
            System.out.println("\nNavigation request sent");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "navigateToPage", "Could not send navigation request.");
        }
    }

    /**
     * Refreshes the currently selected browser
     */
    @Override
    public void refreshBrowser() {
        try {
            EncapsulatedDriver.navigate().refresh();
            System.out.println("\nRefresh request was sent.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "refreshBrowser", "Refresh request was not sent.");
        }
    }

    /**
     * Checks the entire html source for the page for a piece of text
     *
     * @param text The text to search the page for
     * @return True if the page contains the text
     */
    @Override
    public Boolean checkPageSourceForText(String text) {
        try {
            boolean contains = EncapsulatedDriver.getPageSource().contains(text);
            System.out.println("\nPage source checked for presence of text.");
            return contains;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "checkPageSourceForText", "Could not check page source for presence of text.");
            return false;
        }
    }

    /**
     * Gets the source code for the page
     *
     * @return The Page Source
     */
    @Override
    public String getPageSource() {
        try {
            String pageSource = EncapsulatedDriver.getPageSource();
            System.out.println("\nPage source requested.");
            return pageSource;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getPageSource", "Page source could not be requested.");
            return null;
        }
    }

    /**
     * Closes the browser pages and quits the driver
     */
    @Override
    public void closePagesAndQuitDriver() {
        try {
            EncapsulatedDriver.quit();
            System.out.println("\nTerminated driver.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "closePagesAndQuitDriver", "Could not terminate driver.");
        }
    }

    /**
     * Checks if a window is open, using its window handle
     *
     * @param window The window handle to query
     * @return A true/false value
     */
    @Override
    public Boolean isWindowOpen(String window) {
        try {
            EncapsulatedDriver.switchTo().window(window);
            System.out.println("\nWindow presence confirmed.");
            return true;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "isWindowOpen", "Could not confirm window presence.");
            return false;
        }
    }

    /**
     * Switches to the currently active WebElement
     */
    @Override
    public void switchToActiveWebElement() {
        try {
            EncapsulatedDriver.switchTo().activeElement();
            System.out.println("\nSwitched to active element.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "switchToActiveWebElement", "Could not switch to active element.");
        }
    }

    /**
     * Switches to the currently active Alert Dialog
     */
    @Override
    public void switchToAlertDialog() {
        try {
            EncapsulatedDriver.switchTo().alert();
            System.out.println("Switched to alert.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "switchToAlertDialog", "Could not switch to alert.");
        }
    }

    //TODO: add dialog manipulations

    /**
     * Switches to the original frame or window
     */
    @Override
    public void switchToDefaultContent() {
        try {
            EncapsulatedDriver.switchTo().defaultContent();
            System.out.println("Switched to default content.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "switchToDefaultContent", "Could not switch to default content.");
        }
    }

    /**
     * Switches to a numbered frame by index
     *
     * @param frameIndex The index number for the frame
     */
    @Override
    public void switchToFrame(int frameIndex) {
        try {
            EncapsulatedDriver.switchTo().frame(frameIndex);
            System.out.println("Switched to frame with given index.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "switchToFrame", "Could not switch to a frame with that index.");
        }
    }

    /**
     * Switches to the frame identified by the WebElement
     *
     * @param frameElement A WebElement representing the frame to activate
     */
    @Override
    public void switchToFrame(WebElement frameElement) {
        try {
            EncapsulatedDriver.switchTo().frame(frameElement);
            System.out.println("Switched to the chosen frame element.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "switchToFrame", "Could not switch to the web element.");
        }
    }

    /**
     * Switches to the frame identified by the WebElement
     *
     * @param frameLocator The locator for the WebElement representing the frame to activate
     */
    @Override
    public void switchToFrame(By frameLocator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(frameLocator);
            EncapsulatedDriver.switchTo().frame(webElement);
            System.out.println("Switched to the chosen frame element.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "switchToFrame", "Could not switch to the web element.");
        }
    }

    /**
     * Switches to the frame identified by the WebElement
     *
     * @param frameName The name of the frame to activate
     */
    @Override
    public void switchToFrame(String frameName) {
        try {
            EncapsulatedDriver.switchTo().frame(frameName);
            System.out.println("Switched to the named frame element.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "switchToFrame", "Could not switch to a frame with thew given name.");
        }
    }

    /**
     * Switches to the parent frame of a selected WebElement
     */
    @Override
    public void switchToParentFrame() {
        try {
            EncapsulatedDriver.switchTo().parentFrame();
            System.out.println("Switched to the parent frame.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "switchToParentFrame", "Could not switch to the parent frame.");
        }
    }

    /**
     * Switches to a window, given the name of that window
     *
     * @param windowName The name of the window to switch to
     */
    @Override
    public void switchToWindow(String windowName) {
        try {
            EncapsulatedDriver.switchTo().window(windowName);
            System.out.println("Switched to the window.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "switchToWindow", "Could not switch to the window.");
        }
    }

    /**
     * Gets the title of the currently active browser
     *
     * @return The name of the currently active window
     */
    @Override
    public String getBrowserWindowTitle() {
        try {
            String title = EncapsulatedDriver.getTitle();
            System.out.println("Returned the browser window title.");
            return title;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getBrowserWindowTitle", "Could not return the browser window title.");
            return null;
        }
    }

    /**
     * Gets the URL in the currently active browser window
     *
     * @return The URL for the browser
     */
    @Override
    public String getBrowserWindowUrl() {
        try {
            String windowUrl = EncapsulatedDriver.getCurrentUrl();
            System.out.println("Returned the url for the current browser window.");
            return windowUrl;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getBrowserWindowUrl", "Could not return the current url.");
            return null;
        }
    }

    /**
     * Gets the Window Handle for the currently selected page
     *
     * @return A window handle representing a unique reference to a page
     */
    @Override
    public String getCurrentWindowHandle() {
        try {
            String windowUrl = EncapsulatedDriver.getWindowHandle();
            System.out.println("Returned the handle for the current browser window.");
            return windowUrl;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getCurrentWindowHandle", "Could not return the current window handle.");
            return null;
        }
    }

    /**
     * Gets a list of WindowHandles for all windows under the control of the current driver session
     *
     * @return A collection of WindowHandles
     */
    @Override
    public Set<String> getAllWindowHandles() {
        try {
            Set<String> handles = EncapsulatedDriver.getWindowHandles();
            System.out.println("Returned the handle for the current browser window.");
            return handles;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getAllWindowHandles", "Could not return the current window handle.");
            return null;
        }
    }

    /**
     * Opens a new window using the send keys command
     */
    @Override
    public void openNewView() {
        try {

            String tab;
            if (!DriverName.toLowerCase().contains("internetexplorer")) {
                JavascriptExecutor js = (JavascriptExecutor) EncapsulatedDriver;
                js.executeScript("window.open();");
                Set<String> handles = EncapsulatedDriver.getWindowHandles();
                @SuppressWarnings("rawtypes") Iterator iterator = handles.iterator();
                if (!DriverName.toLowerCase().contains("safari")) {
                    iterator.next();
                }
                tab = (String) iterator.next();
                EncapsulatedDriver.switchTo().window(tab);
            } else {
                Element = EncapsulatedDriver.findElement(By.tagName("body"));
                Element.sendKeys(Keys.chord(Keys.CONTROL, "t"));
            }

            System.out.println("Opened a new tab.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "openNewView", "Could not open a new tab.");
        }
    }

    /**
     * Closes the currently selected window
     */
    @Override
    public void closeView() {
        try {
            EncapsulatedDriver.close();
            System.out.println("Closed the open view.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "closeView", "Could not close the current view.");
        }
    }

    /**
     * Waits for an element to be loaded
     *
     * @param element The element for which to wait
     */
    @Override
    public void waitForElementToLoad(WebElement element) {
        try {
            WebDriverWait webDriverWait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            webDriverWait.until(ExpectedConditions.visibilityOf(element));
            System.out.println("Element confirmed as visible.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "waitForElementToLoad", "Element was not visible within the timeout period.");
        }
    }

    /**
     * Waits for an element to be loaded
     *
     * @param locator The locator for the element for which to wait
     */
    @Override
    public void waitForElementToLoad(By locator) {
        try {
            WebDriverWait webDriverWait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            webDriverWait.until(ExpectedConditions.visibilityOfElementLocated(locator));
            System.out.println("Element confirmed as visible.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "waitForElementToLoad", "Element was not visible within the timeout period.");
        }
    }

    /**
     * Waits for an element to be loaded
     *
     * @param element The element for which to wait
     * @param seconds Maximum number of seconds to wait
     */
    @Override
    public void waitForElementToLoad(WebElement element, int seconds) {
        try {
            WebDriverWait webDriverWait = new WebDriverWait(EncapsulatedDriver, seconds);
            webDriverWait.until(ExpectedConditions.visibilityOf(element));
            System.out.println("Element confirmed as visible.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "waitForElementToLoad", "Element was not visible within the timeout period.");
        }
    }

    /**
     * Waits for an element to be loaded
     *
     * @param locator The locator for the element for which to wait
     * @param seconds Maximum number of seconds to wait
     */
    @Override
    public void waitForElementToLoad(By locator, int seconds) {
        try {
            WebDriverWait webDriverWait = new WebDriverWait(EncapsulatedDriver, seconds);
            webDriverWait.until(ExpectedConditions.visibilityOfElementLocated(locator));
            System.out.println("Element confirmed as visible.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "waitForElementToLoad", "Element was not visible within the timeout period.");
        }
    }

    /**
     * Waits for a page to load
     *
     * @param element An element from the previous page. If omitted, the code will wait for the body of the page to be visible
     */
    @Override
    public void waitForPageToLoad(WebElement element) {
        try {
            WebDriverWait webDriverWait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            webDriverWait.until(ExpectedConditions.stalenessOf(element));
            System.out.println("Element confirmed as visible.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "waitForPageToLoad", "Element was not visible within the timeout period.");
        }
    }

    /**
     * Waits for an element to disappear from the DOM
     *
     * @param locator The locator for the element for which to wait
     */
    @Override
    public void waitForInvisibilityOfElement(By locator) {
        try {
            WebDriverWait webDriverWait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            webDriverWait.until(ExpectedConditions.invisibilityOfElementLocated(locator));
            System.out.println("Element confirmed as visible.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "waitForInvisibilityOfElement", "Element was not visible within the timeout period.");
        }
    }

    /**
     * Waits for an element containing text to disappear from the DOM
     *
     * @param locator The locator for the element for which to wait
     * @param text    The text that should be found in the element
     */
    @Override
    public void waitForInvisibilityOfElementWithText(By locator, String text) {
        try {
            WebDriverWait webDriverWait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            webDriverWait.until(ExpectedConditions.invisibilityOfElementWithText(locator, text));
            System.out.println("Element confirmed as visible.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "waitForInvisibilityOfElementWithText", "Element was not visible within the timeout period.");
        }
    }

    /**
     * Clicks on a WebElement
     *
     * @param element The WebElement on which to click
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void clickLink(WebElement element, Boolean wait) {
        try {
            if (wait == null || wait) {
                waitForElementToBeClickable(element);
            }
            if (DriverName.equals("SafariDriver")) {
                Element.sendKeys(Keys.RETURN);
            } else {
                Element.click();
            }
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickLink", "Clicked on the element.");
        }
    }

    /**
     * Clicks on a WebElement
     *
     * @param locator The locator for the WebElement on which to click
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     */
    @Override
    public void clickLink(By locator, Boolean wait) {
        try {
            Element = EncapsulatedDriver.findElement(locator);
            if (wait == null || wait) {
                waitForElementToBeClickable(locator);
            }
            if (DriverName.equals("SafariDriver")) {
                Element.sendKeys(Keys.RETURN);
            } else {
                Element.click();
            }
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickLink", "Clicked on the element.");
        }
    }

    /**
     * Clicks on a link and waits for a new page to be loaded
     *
     * @param element The element on which to click
     */
    @Override
    public void clickLinkAndWait(WebElement element) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            Element = element;
            waitForElementToBeClickable(element);
            if (DriverName.equals("SafariDriver")) {
                Element.sendKeys(Keys.RETURN);
            } else if(DriverName.equals("InternetExplorerDriver")) {
                Actions actions = new Actions(Driver);
                actions.moveToElement(element).click(element).build().perform();
            } else {
                Element.click();
            }
            System.out.println("Clicked on the element.");
            waitForPageToLoad(element);
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.PageLoad);
            }
        } catch (MoveTargetOutOfBoundsException ex) {
            scripts().executeAsyncScript("arguments[0].click();", Element);
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickLinkAndWait", "Element is reporting an out of bounds exception.");
        } catch (Exception e) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, e, "RatDriver", "clickLinkAndWait", "Could not click on the element.");
        }
    }

    /**
     * Clicks on a link and waits for a new page to be loaded
     *
     * @param locator The element on which to click
     */
    @Override
    public void clickLinkAndWait(By locator) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            waitForElementToBeClickable(locator);
            Element = EncapsulatedDriver.findElement(locator);
            if (DriverName.equals("SafariDriver")) {
                Element.sendKeys(Keys.RETURN);
            } else {
                Element.click();
            }
            System.out.println("Clicked on the element.");
            waitForPageToLoad(Element);
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.PageLoad);
            }
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickLinkAndWait", "Could not click on the element.");
        }
    }


    /**
     * Clicks on a link and waits for a new page to be loaded that contains a specified URL or part URL
     *
     * @param element The element on which to click
     * @param url     The partial URL to be waited for
     */
    @Override
    public void clickLinkAndWaitForUrl(WebElement element, String url) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            Element = element;
            clickLink(Element, null);
            System.out.println("Clicked on the element.");
            waitForPageToLoad(Element);
            waitForUrlToContain(url);
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.PageLoad);
            }
            System.out.println("Url of page confirmed.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickLinkAndWaitForUrl", "Failure during attempt to click a link which opens a page.");
        }
    }

    /**
     * Clicks on a link and waits for a new page to be loaded that contains a specified URL or part URL
     *
     * @param locator The locator for the element on which to click
     * @param url     The partial URL to be waited for
     */
    @Override
    public void clickLinkAndWaitForUrl(By locator, String url) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            waitForElementToBeClickable(locator);
            Element = EncapsulatedDriver.findElement(locator);
            if (DriverName.equals("SafariDriver")) {
                Element.sendKeys(Keys.RETURN);
            } else {
                Element.click();
            }
            System.out.println("Clicked on the element.");
            waitForPageToLoad(Element);
            waitForUrlToContain(url);
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.PageLoad);
            }
            System.out.println("Url of page confirmed.");
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "clickLinkAndWaitForUrl", "Failure during attempt to click a link which opens a page.");
        }
    }

    /**
     * Gets the text of a WebElement
     *
     * @param element The WebElement from which to retrieve text
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return The text of the WebElement
     */
    @Override
    public String getElementText(WebElement element, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            return Element.getText();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getElementText", "Could not get the text of the specified element.");
            return null;
        }
    }

    /**
     * Gets the text of a WebElement
     *
     * @param locator The WebElement from which to retrieve text
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return The text of the WebElement
     */
    @Override
    public String getElementText(By locator, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(locator);
            }
            Element = EncapsulatedDriver.findElement(locator);
            return Element.getText();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getElementText", "Could not get the text of the specified element.");
            return null;
        }
    }

    /**
     * Checks the browser for the presence of a particular WebElement
     *
     * @param element The WebElement whose presence is tested
     * @return True if the WebElement is present; false if the WebElement is not present
     */
    @Override
    public Boolean elementExists(WebElement element) {
        try {
            Element = element;
            return Element.isDisplayed();
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "elementExists", "Could not confirm whether the specified element exists.");
            return null;
        }
    }

    /**
     * Checks the browser for the presence of a particular WebElement
     *
     * @param locator The locator for the WebElement whose presence is tested
     * @return True if the WebElement is present; false if the WebElement is not present
     */
    @Override
    public Boolean elementExists(By locator) {
        try {
            Locator = locator;
            Element = EncapsulatedDriver.findElement(locator);
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Gets an attribute of a WebElement and returns it as text
     *
     * @param element   The WebElement whose attributes are to be tested
     * @param attribute The attribute value to retrieve
     * @param wait      (Optional parameter) Whether to wait for the element to reach a known state
     * @return True if the WebElement is present; false if the WebElement is not present
     */
    @Override
    public String getElementAttribute(WebElement element, String attribute, Boolean wait) {
        try {
            Element = element;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            return element.getAttribute(attribute);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getElementAttribute", "Failed to get the attribute from the specified element.");
            return null;
        }
    }

    /**
     * Gets an attribute of a WebElement and returns it as text
     *
     * @param locator   The locator for the WebElement whose attributes are to be tested
     * @param attribute The attribute value to retrieve
     * @param wait      (Optional parameter) Whether to wait for the element to reach a known state
     * @return The attribute to retrieve
     */
    @Override
    public String getElementAttribute(By locator, String attribute, Boolean wait) {
        try {
            Locator = locator;
            if (wait == null || wait) {
                waitForElementToLoad(locator);
            }
            Element = EncapsulatedDriver.findElement(locator);
            return Element.getAttribute(attribute);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getElementAttribute", "Failed to get the attribute from the specified element.");
            return null;
        }
    }

    /**
     * Finds an element with a unique CSS Selector
     *
     * @param cssSelector The CSS Selector to search for
     * @param wait        (Optional parameter) Whether to wait for the element to reach a known state
     * @return A WebElements that has the CSS Selector
     */
    @Override
    public WebElement findElementByCssSelector(String cssSelector, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(By.cssSelector(cssSelector));
            }
            Element = EncapsulatedDriver.findElement(By.cssSelector(cssSelector));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementByCssSelector", "Could not find an element using the CSS Selector");
            return null;
        }
    }

    /**
     * Finds a list of elements that share a CSS Selector
     *
     * @param cssSelector The CSS Selector to search for
     * @param wait        (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of WebElements that share the CSS Selector
     */
    @Override
    public List<WebElement> findElementsByCssSelector(String cssSelector, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForPresenceOfAllElementsLocatedBy(By.cssSelector(cssSelector));
            }
            Elements = EncapsulatedDriver.findElements(By.cssSelector(cssSelector));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementsByCssSelector", "Could not find an element using the CSS Selector");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share a CSS Selector
     *
     * @param cssSelector The CSS Selector to search for
     * @param element     The parent WebElement
     * @param wait        (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByCssSelector(String cssSelector, WebElement element, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(element);
                waitForPresenceOfAllElementsLocatedBy(By.cssSelector(cssSelector));
            }
            Element = element;
            Elements = element.findElements(By.cssSelector(cssSelector));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByCssSelector", "Could not find an element using the CSS Selector");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share a CSS Selector
     *
     * @param cssSelector The CSS Selector to search for
     * @param locator     The locator for the parent WebElement
     * @param wait        (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByCssSelector(String cssSelector, By locator, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(locator);
                waitForPresenceOfAllElementsLocatedBy(By.cssSelector(cssSelector));
            }
            Element = EncapsulatedDriver.findElement(locator);
            Elements = Element.findElements(By.cssSelector(cssSelector));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByCssSelector", "Could not find the sub elements using the CSS Selector");
            return null;
        }
    }

    /**
     * Finds a WebElement that has a Class Name
     *
     * @param className The Class Name to search for
     * @param wait      (Optional parameter) Whether to wait for the element to reach a known state
     * @return A WebElement described by the Class Name
     */
    @Override
    public WebElement findElementByClassName(String className, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(By.className(className));
            }
            Element = EncapsulatedDriver.findElement(By.className(className));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementByClassName", "Could not find an element using the class name");
            return null;
        }
    }

    /**
     * Finds a list of elements that share a Class Name
     *
     * @param className The Class Name to search for
     * @param wait      (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findElementsByClassName(String className, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForPresenceOfAllElementsLocatedBy(By.className(className));
            }
            Elements = EncapsulatedDriver.findElements(By.className(className));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementsByClassName", "Could not find an element using the class name");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share a Class Name
     *
     * @param className The Class Name to search for
     * @param element   The parent WebElement
     * @param wait      (Optional parameter) Whether to wait for the element to reach a known state<
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByClassName(String className, WebElement element, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(element);
                waitForPresenceOfAllElementsLocatedBy(By.className(className));
            }
            Element = element;
            Elements = element.findElements(By.className(className));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByClassName", "Could not find an element using the class name");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share a Class Name
     *
     * @param className The Class Name to search for
     * @param locator   The locator for the parent WebElement
     * @param wait      (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByClassName(String className, By locator, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(locator);
                waitForPresenceOfAllElementsLocatedBy(By.className(className));
            }
            Locator = locator;
            Element = EncapsulatedDriver.findElement(Locator);
            Elements = Element.findElements(By.className(className));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByClassName", "Could not find an element using the class name");
            return null;
        }
    }

    /**
     * Finds a WebElement whose id is as specified
     *
     * @param id   The id to search for
     * @param wait (Optional parameter) Whether to wait for the element to reach a known state
     * @return A WebElement with the specified id
     */
    @Override
    public WebElement findElementById(String id, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(By.id(id));
            }
            Element = EncapsulatedDriver.findElement(By.id(id));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementById", "Could not find an element using the id");
            return null;
        }
    }

    //TODO: Add Id handlers - even ones that should not exist (ElementsById for example)

    /**
     * Finds a WebElement whose link text is as specified
     *
     * @param linkText The text to search for
     * @param wait     (Optional parameter) Whether to wait for the element to reach a known state
     * @return A WebElement with the specified Link Text
     */
    @Override
    public WebElement findElementByLinkText(String linkText, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(By.linkText(linkText));
            }
            Element = EncapsulatedDriver.findElement(By.linkText(linkText));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementByLinkText", "Could not find an element using the link text");
            return null;
        }
    }

    /**
     * Finds a list of elements that whose link text is as specified
     *
     * @param linkText The text to search for
     * @param wait     (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findElementsByLinkText(String linkText, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForPresenceOfAllElementsLocatedBy(By.linkText(linkText));
            }
            Elements = EncapsulatedDriver.findElements(By.linkText(linkText));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementsByLinkText", "Could not find an element using the link text");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement whose link text is as specified
     *
     * @param linkText The text to search for
     * @param element  The parent WebElement
     * @param wait     (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByLinkText(String linkText, WebElement element, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(element);
                waitForPresenceOfAllElementsLocatedBy(By.linkText(linkText));
            }
            Element = element;
            Elements = element.findElements(By.linkText(linkText));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByLinkText", "Could not find an element using the link text");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement whose link text is as specified
     *
     * @param linkText The text to search for
     * @param locator  The locator for the parent WebElement
     * @param wait     (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByLinkText(String linkText, By locator, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(locator);
                waitForPresenceOfAllElementsLocatedBy(By.linkText(linkText));
            }
            Locator = locator;
            Element = EncapsulatedDriver.findElement(locator);
            Elements = Element.findElements(By.linkText(linkText));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByLinkText", "Could not find an element using the link text");
            return null;
        }
    }

    /**
     * Finds a WebElement by name
     *
     * @param name The name to search for
     * @param wait (Optional parameter) Whether to wait for the element to reach a known state
     * @return A WebElement
     */
    @Override
    public WebElement findElementByName(String name, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(By.name(name));
            }
            Element = EncapsulatedDriver.findElement(By.name(name));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementByName", "Could not find an element using the name");
            return null;
        }
    }

    /**
     * Finds a list of elements that share a name
     *
     * @param name The name to search for
     * @param wait (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of WebElements
     */
    @Override
    public List<WebElement> findElementsByName(String name, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForPresenceOfAllElementsLocatedBy(By.name(name));
            }
            Elements = EncapsulatedDriver.findElements(By.name(name));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementsByName", "Could not find an element using the name");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share a name
     *
     * @param name    The name to search for
     * @param element The parent WebElement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByName(String name, WebElement element, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(element);
                waitForPresenceOfAllElementsLocatedBy(By.name(name));
            }
            Element = element;
            Elements = element.findElements(By.name(name));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByName", "Could not find an element using the name");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share a name
     *
     * @param name    The name to search for
     * @param locator The locator for the parent WebElement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByName(String name, By locator, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(locator);
                waitForPresenceOfAllElementsLocatedBy(By.name(name));
            }
            Locator = locator;
            Element = EncapsulatedDriver.findElement(locator);
            Elements = Element.findElements(By.name(name));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByName", "Could not find an element using the name");
            return null;
        }
    }

    /**
     * Finds a WebElement whose link text is matched in part
     *
     * @param linkText The text to find in whole or in part
     * @param wait     (Optional parameter) Whether to wait for the element to reach a known state
     * @return A WebElement
     */
    @Override
    public WebElement findElementByPartialLinkText(String linkText, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(By.partialLinkText(linkText));
            }
            Element = EncapsulatedDriver.findElement(By.partialLinkText(linkText));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementByPartialLinkText", "Could not find an element using the partial link text");
            return null;
        }
    }

    /**
     * Finds a list of elements whose link text is matched in part
     *
     * @param linkText The text to find in whole or in part
     * @param wait     (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findElementsByPartialLinkText(String linkText, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForPresenceOfAllElementsLocatedBy(By.partialLinkText(linkText));
            }
            Elements = EncapsulatedDriver.findElements(By.partialLinkText(linkText));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementsByPartialLinkText", "Could not find an element using the partial link text");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement whose link text is matched in part
     *
     * @param linkText The text to find in whole or in part
     * @param element  The parent WebElement
     * @param wait     (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByPartialLinkText(String linkText, WebElement element, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(element);
                waitForPresenceOfAllElementsLocatedBy(By.partialLinkText(linkText));
            }
            Element = element;
            Elements = element.findElements(By.partialLinkText(linkText));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByPartialLinkText", "Could not find an element using the partial link text");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement whose link text is matched in part
     *
     * @param linkText The text to find in whole or in part
     * @param locator  The locator for the parent WebElement
     * @param wait     (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByPartialLinkText(String linkText, By locator, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(locator);
                waitForPresenceOfAllElementsLocatedBy(By.partialLinkText(linkText));
            }
            Locator = locator;
            Element = EncapsulatedDriver.findElement(locator);
            Elements = Element.findElements(By.partialLinkText(linkText));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByPartialLinkText", "Could not find an element using the partial link text");
            return null;
        }
    }

    /**
     * Finds a WebElement that possesses a tag
     *
     * @param tagName The tag to search for
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return A WebElement
     */
    @Override
    public WebElement findElementByTag(String tagName, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(By.tagName(tagName));
            }
            Element = EncapsulatedDriver.findElement(By.tagName(tagName));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementByTag", "Could not find an element using the tag name");
            return null;
        }
    }

    /**
     * Finds a list of elements that share a tag
     *
     * @param tagName The tag to search for
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findElementsByTag(String tagName, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForPresenceOfAllElementsLocatedBy(By.tagName(tagName));
            }
            Elements = EncapsulatedDriver.findElements(By.tagName(tagName));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementsByTag", "Could not find an element using the tag name");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share a tag
     *
     * @param tagName The tag to search for
     * @param element The parent WebElement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByTag(String tagName, WebElement element, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(element);
                waitForPresenceOfAllElementsLocatedBy(By.tagName(tagName));
            }
            Element = element;
            Elements = element.findElements(By.tagName(tagName));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByTag", "Could not find an element using the tag name");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share a tag
     *
     * @param tagName The tag to search for
     * @param locator The locator for the parent WebElement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByTag(String tagName, By locator, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(locator);
                waitForPresenceOfAllElementsLocatedBy(By.tagName(tagName));
            }
            Locator = locator;
            Element = EncapsulatedDriver.findElement(locator);
            Elements = Element.findElements(By.tagName(tagName));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByTag", "Could not find an element using the tag name");
            return null;
        }
    }

    /**
     * Finds a Web Elements by its xpath
     *
     * @param xpath The xpath to search for
     * @param wait  (Optional parameter) Whether to wait for the element to reach a known state
     * @return A WebElement
     */
    @Override
    public WebElement findElementByXPath(String xpath, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(By.xpath(xpath));
            }
            Element = EncapsulatedDriver.findElement(By.xpath(xpath));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementByXPath", "Could not find an element using xpath");
            return null;
        }
    }

    /**
     * Finds a list of elements that share an xpath
     *
     * @param xpath The xpath to search for
     * @param wait  (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findElementsByXPath(String xpath, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForPresenceOfAllElementsLocatedBy(By.xpath(xpath));
            }
            Elements = EncapsulatedDriver.findElements(By.xpath(xpath));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findElementsByXPath", "Could not find an element using xpath");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share an xpath
     *
     * @param xpath   The xpath to search for
     * @param element The parent WebElement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByXPath(String xpath, WebElement element, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(element);
                waitForPresenceOfAllElementsLocatedBy(By.xpath(xpath));
            }
            Element = element;
            Elements = element.findElements(By.xpath(xpath));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByXPath", "Could not find an element using xpath");
            return null;
        }
    }

    /**
     * Finds the child elements of a given WebElement that share an xpath
     *
     * @param xpath   The xpath to search for
     * @param locator The locator for the parent WebElement
     * @param wait    (Optional parameter) Whether to wait for the element to reach a known state
     * @return A collection of child WebElements
     */
    @Override
    public List<WebElement> findSubElementsByXPath(String xpath, By locator, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            if (wait == null || wait) {
                waitForElementToLoad(locator);
                waitForPresenceOfAllElementsLocatedBy(By.xpath(xpath));
            }
            Locator = locator;
            Element = EncapsulatedDriver.findElement(locator);
            Elements = Element.findElements(By.xpath(xpath));
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Elements;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "findSubElementsByXPath", "Could not find an element using xpath");
            return null;
        }
    }

    /**
     * Finds a WebElement identified by an attribute value from a collection of WebElements sharing a parent
     *
     * @param parentElement The parent element for the process
     * @param type          The type of locator to use to fetch the collection
     * @param locator       The locator value for items in the collection
     * @param attribute     The HTML attribute to use to find the unique item
     * @param value         The value of the attribute of the unique item
     * @param wait          (Optional parameter) Whether to wait for the element to reach a known state
     * @return A web element identified by a locator type and an attribute value
     */
    @Override
    public WebElement extractElementFromCollectionByAttribute(WebElement parentElement, LocatorType type, String locator, String attribute, String value, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            Element = parentElement;
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            getCollectionOfElements(type, locator);
            findMatchingElement(attribute, value);
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "extractElementFromCollectionByAttribute", "Unable to extract the element required with defined parameters.");
            return null;
        }
    }

    /**
     * Finds a WebElement identified by an attribute value from a collection of WebElements sharing a parent
     *
     * @param parentLocator The locator for the parent element for the process
     * @param type          The type of locator to use to fetch the collection
     * @param locator       The locator value for items in the collection
     * @param attribute     The HTML attribute to use to find the unique item
     * @param value         The value of the attribute of the unique item
     * @param wait          (Optional parameter) Whether to wait for the element to reach a known state
     * @return A web element identified by a locator type and an attribute value
     */
    @Override
    public WebElement extractElementFromCollectionByAttribute(By parentLocator, LocatorType type, String locator, String attribute, String value, Boolean wait) {
        try {
            if (RecordPerformance) {
                RatTimerCollection.StartTimer();
            }
            Locator = parentLocator;
            Element = EncapsulatedDriver.findElement(Locator);
            if (wait == null || wait) {
                waitForElementToLoad(Element);
            }
            getCollectionOfElements(type, locator);
            findMatchingElement(attribute, value);
            if (RecordPerformance) {
                RatTimerCollection.StopTimer(Timing.ElementFindTime);
            }
            return Element;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "extractElementFromCollectionByAttribute", "Unable to extract the element required with defined parameters.");
            return null;
        }
    }

    private void findMatchingElement(String attribute, String value) {
        for (WebElement element : Elements) {
            if (element.getAttribute(attribute).contains(value)) {
                Element = element;
                break;
            } else {
                Element = null;
            }
        }
    }

    //region Scripts

    /**
     * Takes a screenshot of the active web page
     *
     * @param path The path and name under which to save the image
     */
    @SuppressWarnings("ResultOfMethodCallIgnored")
    @Override
    public void takeScreenshot(String path) {
        try {
            File screenshotFile = ((TakesScreenshot) Driver).getScreenshotAs(OutputType.FILE);
            File destinationFile = new File(path);
            screenshotFile.renameTo(destinationFile);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "takeScreenshot", "Unable to save a screenshot.");
        }
    }

    /**
     * Returns a JavaScript Executor to the end user for custom JavaScript
     *
     * @return A JavaScript Executor
     */
    @Override
    public JavascriptExecutor scripts() {
        try {
            return (JavascriptExecutor) Driver;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "scripts", "Unable to execute the script required.");
            return null;
        }
    }

    /**
     * Executes a JavaScript statement passed as a String
     *
     * @param script The JavaScript to be executed
     */
    @Override
    public void executeJavaScript(String script) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeScript(script);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "executeJavaScript", "Unable to execute the script required.");
        }
    }

    /**
     * Executes a JavaScript statement passed as a String
     *
     * @param script     The JavaScript to be executed
     * @param parameters The parameters for the passed JavaScript
     */
    @Override
    public void executeJavaScript(String script, Object[] parameters) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeScript(script, parameters);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "executeJavaScript", "Unable to execute the script required.");
        }
    }

    /**
     * Executes a JavaScript statement passed as a String
     *
     * @param script The JavaScript to be executed
     */
    @Override
    public void executeAsyncJavaScript(String script) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript(script);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "executeAsyncJavaScript", "Unable to execute the script required.");
        }
    }

    /**
     * Executes a JavaScript statement passed as a String
     *
     * @param script     The JavaScript to be executed
     * @param parameters The parameters for the passed JavaScript
     */
    @Override
    public void executeAsyncJavaScript(String script, Object[] parameters) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript(script, parameters);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "executeAsyncJavaScript", "Unable to execute the script required.");
        }
    }

    /**
     * Scrolls to an element using JavaScript
     *
     * @param webElement The Web element
     */
    public void scrollToElement(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript("arguments[0].scrollIntoView(true);", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "scrollToElement", "Could not scroll to the element passed.");
        }
    }

    /**
     * Scrolls to an element using JavaScript
     *
     * @param locator The the locator for the Web Element.
     */
    public void scrollToElement(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript("arguments[0].scrollIntoView(true);", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "scrollToElement", "Could not scroll to the element passed.");
        }
    }

    /**
     * Adds an access key to a web element.
     *
     * @param webElement The Web Element to receive the access key.
     * @param key        The key to be used for access.
     */
    public void addAccessKeyToElement(WebElement webElement, String key) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript("arguments[0].accessKey = [1];", webElement, key);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "addAccessKeyToElement", "Could not add the access key to the element.");
        }
    }

    /**
     * Adds an access key to a web element.
     *
     * @param locator The the locator for the Web Element to receive the access key.
     * @param key     The key to be used for access.
     */
    public void addAccessKeyToElement(By locator, String key) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript("arguments[0].accessKey = [1];", webElement, key);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "addAccessKeyToElement", "Could not add the access key to the element.");
        }
    }

    /**
     * Gets an access key from a web element.
     *
     * @param webElement The Web Element for which to retrieve the access key.
     */
    public void getAccessKeyForElement(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript("arguments[0].accessKey;", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getAccessKeyForElement", "Could not get the access key for the element.");
        }
    }

    /**
     * Gets an access key from a web element.
     *
     * @param locator The locator for the Web Element for which to retrieve the access key.
     */
    public void getAccessKeyForElement(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript("arguments[0].accessKey;", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getAccessKeyForElement", "Could not get the access key for the element.");
        }
    }

    /**
     * Returns the number of web elements.
     *
     * @param webElement The web element for which a child element count is required.
     * @return The number of child elements that the web element possesses.
     */
    public int childElementCount(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (int) javascriptExecutor.executeAsyncScript("arguments[0].childElementCount", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "childElementCount", "Could not get the child element count for the element.");
            return 0;
        }
    }

    /**
     * Returns the number of web elements
     *
     * @param locator The locator for the web element for which a child element count is required.
     * @return The number of web element
     */
    public int childElementCount(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (int) javascriptExecutor.executeAsyncScript("arguments[0].childElementCount", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "childElementCount", "Could not get the child element count for the element.");
            return 0;
        }
    }

    /**
     * Gets the viewable area measurements of the element passed. This does not include borders, scrollbars or margins.
     *
     * @param webElement The web element for which a child element count is required.
     * @return The viewable area measurements of the web element.
     */
    public ElementSize getElementSize(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            ElementSize elementSize = new ElementSize();
            elementSize.Height = (int) javascriptExecutor.executeScript("arguments[0].clientHeight", webElement);
            elementSize.Width = (int) javascriptExecutor.executeScript("arguments[0].clientWidth", webElement);
            elementSize.Top = (int) javascriptExecutor.executeScript("arguments[0].clientTop", webElement);
            elementSize.Left = (int) javascriptExecutor.executeScript("arguments[0].clientLeft", webElement);
            return elementSize;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getElementSize", "Could not get the client size for the element.");
            return null;
        }
    }

    /**
     * Gets the viewable area measurements of the element passed. This does not include borders, scrollbars or margins.
     *
     * @param locator The locator for the web element for which a child element count is required.
     * @return The viewable area measurements of the web element.
     */
    public ElementSize getElementSize(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            ElementSize elementSize = new ElementSize();
            elementSize.Height = (int) javascriptExecutor.executeScript("arguments[0].clientHeight", webElement);
            elementSize.Width = (int) javascriptExecutor.executeScript("arguments[0].clientWidth", webElement);
            elementSize.Top = (int) javascriptExecutor.executeScript("arguments[0].clientTop", webElement);
            elementSize.Left = (int) javascriptExecutor.executeScript("arguments[0].clientLeft", webElement);
            return elementSize;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getElementSize", "Could not get the client size for the element.");
            return null;
        }
    }

    /**
     * Compares the positions for a pair of elements.
     *
     * @param firstElement  The first element of the pair.
     * @param secondElement The second element of the pair.
     * @return An enumerated value for the relative positions of the elements.
     */
    public ElementRelationships compareElementPositions(WebElement firstElement, WebElement secondElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (ElementRelationships) javascriptExecutor.executeAsyncScript("arguments[0].compareDocumentPosition(arguments[1])", firstElement, secondElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "compareElementPositions", "Could not analyse the element relationships.");
            return null;
        }
    }

    /**
     * Compares the positions for a pair of elements.
     *
     * @param firstLocator  The locator for the first element of the pair.
     * @param secondLocator The locator for the second element of the pair.
     * @return An enumerated value for the relative positions of the elements.
     */
    public ElementRelationships compareElementPositions(By firstLocator, By secondLocator) {
        try {
            WebElement firstElement = EncapsulatedDriver.findElement(firstLocator);
            WebElement secondElement = EncapsulatedDriver.findElement(secondLocator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (ElementRelationships) javascriptExecutor.executeAsyncScript("arguments[0].compareDocumentPosition(arguments[1])", firstElement, secondElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "compareElementPositions", "Could not analyse the element relationships.");
            return null;
        }
    }

    /**
     * Checks to see if an element is contained by another element.
     *
     * @param firstElement  The first element of the pair.
     * @param secondElement The second element of the pair.
     * @return True if the second element is a descendant of the first.
     */
    public Boolean contains(WebElement firstElement, WebElement secondElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].contains(arguments[1])", firstElement, secondElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "contains", "Could not analyse the element relationships.");
            return null;
        }
    }

    /**
     * Checks to see if an element is contained by another element.
     *
     * @param firstLocator  The locator for the first element of the pair.
     * @param secondLocator The locator for the second element of the pair.
     * @return True if the second element is a descendant of the first.
     */
    public Boolean contains(By firstLocator, By secondLocator) {
        try {
            WebElement firstElement = EncapsulatedDriver.findElement(firstLocator);
            WebElement secondElement = EncapsulatedDriver.findElement(secondLocator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].contains(arguments[1])", firstElement, secondElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "contains", "Could not analyse the element relationships.");
            return null;
        }
    }

    /**
     * Gives the focus to an element
     *
     * @param webElement The Web Element
     */
    public void giveFocus(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript("arguments[0].focus()", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "giveFocus", "Could not give focus to the element.");
        }
    }

    /**
     * Gives the focus to an element
     *
     * @param locator The locator for the Web Element
     */
    public void giveFocus(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            javascriptExecutor.executeAsyncScript("arguments[0].focus()", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "", "Could not give focus to the element.");
        }
    }

    /**
     * Gets the bounding rectangle for the element
     *
     * @param webElement The web element
     * @return An object representing the bounding rectangle
     */
    public DomRectangle getBoundingRectangle(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            DomRectangle domRectangle = new DomRectangle();
            domRectangle.Height = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().height", webElement);
            domRectangle.Width = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().width", webElement);
            domRectangle.Top = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().top", webElement);
            domRectangle.Left = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().left", webElement);
            domRectangle.Bottom = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().bottom", webElement);
            domRectangle.Right = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().right", webElement);
            domRectangle.X = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().x", webElement);
            domRectangle.Y = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().y", webElement);
            return domRectangle;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getBoundingRectangle", "Could get the bounding rectangle for the element.");
            return null;
        }
    }

    /**
     * Gets the bounding rectangle for the element
     *
     * @param locator The locator for the web element
     * @return An object representing the bounding rectangle
     */
    public DomRectangle getBoundingRectangle(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            DomRectangle domRectangle = new DomRectangle();
            domRectangle.Height = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().height", webElement);
            domRectangle.Width = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().width", webElement);
            domRectangle.Top = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().top", webElement);
            domRectangle.Left = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().left", webElement);
            domRectangle.Bottom = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().bottom", webElement);
            domRectangle.Right = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().right", webElement);
            domRectangle.X = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().x", webElement);
            domRectangle.Y = (int) javascriptExecutor.executeScript("arguments[0].getBoundingClientRect().y", webElement);
            return domRectangle;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getBoundingRectangle", "Could get the bounding rectangle for the element.");
            return null;
        }
    }

    /**
     * Checks if a Web Element has a particular attribute
     *
     * @param webElement The web element
     * @param attribute  The attribute to check for.
     * @return True if the element has the given attribute.
     */
    public Boolean hasAttribute(WebElement webElement, String attribute) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].hasAttribute(arguments[1]);", webElement, attribute);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "hasAttribute", "Could not assess whether the element has the attribute");
            return null;
        }
    }

    /**
     * Checks if a Web Element has a particular attribute
     *
     * @param locator   The locator for the web element
     * @param attribute The attribute to check for.
     * @return True if the element has the given attribute.
     */
    public Boolean hasAttribute(By locator, String attribute) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].hasAttribute(arguments[1]);", webElement, attribute);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "hasAttribute", "Could not assess whether the element has the attribute");
            return null;
        }
    }

    /**
     * Checks whether the web element has any attributes.
     *
     * @param webElement The Web Element.
     * @return True if the web element has any attributes.
     */
    public Boolean hasAttributes(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].hasAttributes();", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "hasAttributes", "Could not assess whether the element has attributes");
            return null;
        }
    }

    /**
     * Checks whether the web element has any attributes.
     *
     * @param locator The locator for the Web Element.
     * @return True if the web element has any attributes.
     */
    public Boolean hasAttributes(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].hasAttributes();", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "hasAttributes", "Could not assess whether the element has attributes");
            return null;
        }
    }

    /**
     * Checks whether the web element has any child nodes.
     *
     * @param webElement The Web Element.
     * @return True if the web element has any child nodes.
     */
    public Boolean hasChildNodes(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].hasChildNodes();", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "hasChildNodes", "Could not assess whether the element has child nodes");
            return null;
        }
    }

    /**
     * Checks whether the web element has any child nodes.
     *
     * @param locator The locator for the Web Element.
     * @return True if the web element has any child nodes.
     */
    public Boolean hasChildNodes(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].hasChildNodes();", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "hasChildNodes", "Could not assess whether the element has child nodes");
            return null;
        }
    }

    /**
     * Checks if the content of an element is editable.
     *
     * @param webElement The Web Element
     * @return True if the content is editable, false if not. Also may return Inherit, to denote it has inherited this status.
     */
    public String isContentEditable(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (String) javascriptExecutor.executeAsyncScript("arguments[0].contentEditable;", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "isContentEditable", "Could not confirm if the element has editable content.");
            return null;
        }
    }

    /**
     * Checks if the content of an element is editable.
     *
     * @param locator The the locator for the Web Element.
     * @return True if the content is editable, false if not. Also may return Inherit, to denote it has inherited this status.
     */
    public String isContentEditable(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (String) javascriptExecutor.executeAsyncScript("arguments[0].contentEditable;", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "isContentEditable", "Could not confirm if the element has editable content.");
            return null;
        }
    }

    /**
     * Gets the language assigned to a WebElement
     *
     * @param webElement The Web Element
     * @return The ISO 639-1 code for the language.
     */
    public String getElementLanguage(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (String) javascriptExecutor.executeAsyncScript("arguments[0].lang;", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getElementLanguage", "Could not confirm the language assignment of the element.");
            return null;
        }
    }

    /**
     * Gets the language assigned to a WebElement
     *
     * @param locator The locator for the Web Element
     * @return The ISO 639-1 code for the language.
     */
    public String getElementLanguage(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (String) javascriptExecutor.executeAsyncScript("arguments[0].lang;", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getElementLanguage", "Could not confirm the language assignment of the element.");
            return null;
        }
    }

    /**
     * Gets the offsets for an element.
     *
     * @param webElement The Web Element
     * @return An object representing the offsets of the element
     */
    public HeightWidth getOffsets(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            HeightWidth heightWidth = new HeightWidth();
            heightWidth.Height = (int) javascriptExecutor.executeScript("arguments[0].offsetHeight;", webElement);
            heightWidth.Width = (int) javascriptExecutor.executeScript("arguments[0].offsetWidth;", webElement);
            return heightWidth;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getOffsets", "Could not get the offsets for the element.");
            return null;
        }
    }

    /**
     * Gets the offsets for an element.
     *
     * @param locator The locator for the Web Element
     * @return An object representing the offsets of the element
     */
    public HeightWidth getOffsets(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            HeightWidth heightWidth = new HeightWidth();
            heightWidth.Height = (int) javascriptExecutor.executeScript("arguments[0].offsetHeight;", webElement);
            heightWidth.Width = (int) javascriptExecutor.executeScript("arguments[0].offsetWidth;", webElement);
            return heightWidth;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getOffsets", "Could not get the offsets for the element.");
            return null;
        }
    }

    /**
     * Checks to see if an element is equal to another element.
     *
     * @param firstElement  The first element of the pair.
     * @param secondElement The second element of the pair.
     * @return True if the second element is equal to the first.
     */
    public Boolean areNodesEqual(WebElement firstElement, WebElement secondElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].isEqualNode(arguments[1]);", firstElement, secondElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "areNodesEqual", "Could not compare the elements for equality.");
            return null;
        }
    }

    /**
     * Checks to see if an element is equal to another element.
     *
     * @param firstLocator  The locator for the first element of the pair.
     * @param secondLocator The locator for the second element of the pair.
     * @return True if the second element is equal to the first.
     */
    public Boolean areNodesEqual(By firstLocator, By secondLocator) {
        try {
            WebElement firstElement = EncapsulatedDriver.findElement(firstLocator);
            WebElement secondElement = EncapsulatedDriver.findElement(secondLocator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (Boolean) javascriptExecutor.executeAsyncScript("arguments[0].isEqualNode(arguments[1]);", firstElement, secondElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "areNodesEqual", "Could not compare the elements for equality.");
            return null;
        }
    }

    /**
     * Gets the height and width of an element in pixels, including padding, but not the border, scrollbar or margin.
     *
     * @param webElement The Web Element
     * @return An object representing the scroll height and width of the element, along with current positioning.
     */
    public ElementSize getScrollSize(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            ElementSize elementSize = new ElementSize();
            elementSize.Height = (int) javascriptExecutor.executeScript("arguments[0].scrollHeight;", webElement);
            elementSize.Width = (int) javascriptExecutor.executeScript("arguments[0].scrollWidth;", webElement);
            elementSize.Top = (int) javascriptExecutor.executeScript("arguments[0].scrollTop;", webElement);
            elementSize.Left = (int) javascriptExecutor.executeScript("arguments[0].scrollLeft;", webElement);
            return elementSize;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getScrollSize", "Could not get the scroll size of the element.");
            return null;
        }
    }

    /**
     * Gets the height and width of an element in pixels, including padding, but not the border, scrollbar or margin.
     *
     * @param locator The locator for the Web Element
     * @return An object representing the scroll height and width of the element, along with current positioning.
     */
    public ElementSize getScrollSize(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            ElementSize elementSize = new ElementSize();
            elementSize.Height = (int) javascriptExecutor.executeScript("arguments[0].offsetHeight;", webElement);
            elementSize.Width = (int) javascriptExecutor.executeScript("arguments[0].offsetWidth;", webElement);
            return elementSize;
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getScrollSize", "Could not get the scroll size of the element.");
            return null;
        }
    }

    /**
     * Returns the tab index of the chosen web element.
     *
     * @param webElement The web element for which a tab index is required.
     * @return The tab index of the chosen element.
     */
    public int getTabIndex(WebElement webElement) {
        try {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (int) javascriptExecutor.executeAsyncScript("arguments[0].tabIndex", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getTabIndex", "Could get the tab index for the element.");
            return 0;
        }
    }

    /**
     * Returns the tab index of the chosen web element.
     *
     * @param locator The locator for the web element for which a tab index is required.
     * @return The tab index of the chosen element.
     */
    public int getTabIndex(By locator) {
        try {
            WebElement webElement = EncapsulatedDriver.findElement(locator);
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) Driver;
            return (int) javascriptExecutor.executeAsyncScript("arguments[0].tabIndex", webElement);
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "getTabIndex", "Could get the tab index for the element.");
            return 0;
        }
    }


    //endregion

    //region Waits

    /**
     * Waits for an alert style dialog to be displayed
     *
     * @return True if the wait terminates with an alert being found
     */
    @Override
    public Boolean waitForAlertToBePresent() {
        try {
            WebDriverWait wait = new WebDriverWait(this.Driver, BaseSettings.Timeout);
            Alert alert = wait.until(ExpectedConditions.alertIsPresent());
            if (alert == null) {
                throw new Exception("Could not confirm selection of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for an element to reach a clickable state
     *
     * @param element The WebElement representing the object
     * @return True if the wait is terminated by the element becoming clickable
     */
    @Override
    public Boolean waitForElementToBeClickable(WebElement element) {
        try {
            WebDriverWait wait = new WebDriverWait(Driver, BaseSettings.Timeout);
            WebElement ele = wait.until(ExpectedConditions.elementToBeClickable(element));
            if (ele == null) {
                throw new Exception("Could not confirm click-ability of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for an element to reach a clickable state
     *
     * @param locator The locator for the WebElement representing the object
     * @return True if the wait is terminated by the element becoming clickable
     */
    @Override
    public Boolean waitForElementToBeClickable(By locator) {
        try {
            WebDriverWait wait = new WebDriverWait(this.Driver, BaseSettings.Timeout);
            WebElement ele = wait.until(ExpectedConditions.elementToBeClickable(locator));
            if (ele == null) {
                throw new Exception("Could not confirm click-ability of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }


    /**
     * Waits for an element to have a selected status
     *
     * @param locator The locator for the WebElement to await
     * @return True if the wait is terminated by the element being found to be selected
     */
    @Override
    public Boolean waitForElementToBeSelected(By locator) {
        try {
            WebDriverWait wait = new WebDriverWait(this.Driver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.elementToBeSelected(locator));
            if (bool) {
                throw new Exception("Could not confirm selection of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for an element to have a selected status
     *
     * @param element The WebElement to await
     * @return True if the wait is terminated by the element being found to be selected
     */
    @Override
    public Boolean waitForElementToBeSelected(WebElement element) {
        try {
            WebDriverWait wait = new WebDriverWait(this.Driver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.elementToBeSelected(element));
            if (bool) {
                throw new Exception("Could not confirm selection of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for an element to have a visible attribute of true
     *
     * @param locator The locator for the WebElement to await
     * @return True if the wait is terminated by the element being found to be visible
     */
    @Override
    public Boolean waitForElementToBeVisible(By locator) {
        try {
            WebDriverWait wait = new WebDriverWait(this.Driver, BaseSettings.Timeout);
            WebElement element = wait.until(ExpectedConditions.visibilityOfElementLocated(locator));
            if (element == null) {
                throw new Exception("Could not confirm visibility of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for the selection of an element to reach a certain state
     *
     * @param locator The locator for the WebElement to be used
     * @param state   The selection state to check for
     * @return True if the wait is terminated by the element being found to be in a given selection state.
     */
    @Override
    public Boolean waitForElementSelectionStateToBe(By locator, Boolean state) {
        try {
            WebDriverWait wait = new WebDriverWait(this.Driver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.elementSelectionStateToBe(locator, state));
            if (bool) {
                throw new Exception("Could not confirm selection state of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for the selection of an element to reach a certain state
     *
     * @param element The WebElement to await
     * @param state   The selection state to check for
     * @return True if the wait is terminated by the element being found to be in a given selection state.
     */
    @Override
    public Boolean waitForElementSelectionStateToBe(WebElement element, Boolean state) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.elementSelectionStateToBe(element, state));
            if (bool) {
                throw new Exception("Could not confirm selection state of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for a given element containing text to be invisible
     *
     * @param locator The locator for the WebElement to be used
     * @return True if the elements is invisible, false if the wait expires.
     */
    @Override
    public Boolean waitForElementInvisibility(By locator) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.invisibilityOfElementLocated(locator));
            if (bool) {
                throw new Exception("Could not confirm invisibility of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for a given element containing text to be invisible or to be removed from the DOM
     *
     * @param locator The locator for the WebElement to be used
     * @param text    The text to be used in comparison
     * @return True if the elements with given text is invisible, false if the wait expires
     */
    @Override
    public Boolean waitForElementInvisibilityWithText(By locator, String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.invisibilityOfElementWithText(locator, text));
            if (bool) {
                throw new Exception("Could not confirm invisibility of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Checks if any elements on the page match the locator
     *
     * @param locator The locator for the WebElement to be used
     * @return True if the elements found by a locator are present, false if the wait expires or no elements are found.
     */
    @Override
    public Boolean waitForPresenceOfAllElementsLocatedBy(By locator) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            List<WebElement> elements = wait.until(ExpectedConditions.presenceOfAllElementsLocatedBy(locator));
            if (elements.size() == 0) {
                throw new Exception("Could not confirm presence of the elements required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for a given element to go stale
     *
     * @param element The WebElement to check
     * @return True if the element becomes stale, false if the wait expires.
     */
    @Override
    public Boolean waitForStalenessOf(WebElement element) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.stalenessOf(element));
            if (bool) {
                throw new Exception("Could not confirm staleness of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits until specified text is found within a given element
     *
     * @param element The WebElement to check
     * @param text    The text to be used in comparison
     * @return True if the element contains the given text, false if the wait expires.
     */
    @Override
    public Boolean waitForTextToBePresentInElement(WebElement element, String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.textToBePresentInElement(element, text));
            if (bool) {
                throw new Exception("Text does not appear within the timeout period.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits until specified text is found within a given element
     *
     * @param locator The locator for the WebElement to check
     * @param text    The text to be used in comparison
     * @return True if the element contains the given text, false if the wait expires.
     */
    @Override
    public Boolean waitForTextToBePresentInElement(By locator, String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.textToBePresentInElementLocated(locator, text));
            if (bool) {
                throw new Exception("Text does not appear within the timeout period.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits until specified text is found within a given element's value attribute
     *
     * @param locator The locator for the WebElement to check
     * @param text    The text to be used in comparison
     * @return True if the element contains the given text, false if the wait expires.
     */
    @Override
    public Boolean waitForTextToBePresentInElementValue(By locator, String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.textToBePresentInElementValue(locator, text));
            if (bool) {
                throw new Exception("Text does not appear within the timeout period.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits until a given String is found in the value attribute of the given element
     *
     * @param element The element to observe
     * @param text    The text to be used in comparison
     * @return True if the element contains the given text, false if the wait expires.
     */
    @Override
    public Boolean waitForTextToBePresentInElementValue(WebElement element, String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.textToBePresentInElementValue(element, text));
            if (bool) {
                throw new Exception("Text does not appear within the timeout period.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Pauses execution until the title of the page title contains a given String
     *
     * @param text The text to be used in comparison
     * @return True if the title contains the given text, false if the wait expires.
     */
    @Override
    public Boolean waitForTitleToContain(String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.titleContains(text));
            if (bool) {
                throw new Exception("Title does not appear within the timeout period.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Pauses execution until the title of the page title matches a given String
     *
     * @param text The text to be used in comparison
     * @return True if the title matches the given text, false if the wait expires.
     */
    @Override
    public Boolean waitForTitleToBe(String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.titleIs(text));
            if (bool) {
                throw new Exception("Title does not appear within the timeout period.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Pauses execution until the url contains a given String
     *
     * @param text The text to use in comparison.
     * @return True if the URL contains the given text, false if the wait expires.
     */
    @Override
    public Boolean waitForUrlToContain(String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.urlContains(text));
            if (bool) {
                throw new Exception("URL does not appear within the timeout period.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Pauses execution until the url matches a given String
     *
     * @param text The text to use in comparison.
     * @return True if the URL matches the given text, false if the wait expires.
     */
    @Override
    public Boolean waitForUrlToMatch(String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.urlMatches(text));
            if (bool) {
                throw new Exception("URL does not appear within the timeout period.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Pauses execution until the url matches a given String
     *
     * @param text The text to use in comparison.
     * @return True if the URL matches the given text, false if the wait expires.
     */
    @Override
    public Boolean waitForUrlToBe(String text) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            Boolean bool = wait.until(ExpectedConditions.urlToBe(text));
            if (bool) {
                throw new Exception("URL does not appear within the timeout period.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    /**
     * Waits for all of the element identified by the locator to be visible
     *
     * @param locator The locator to use to find elements
     * @return True if all elements located are visible, false if the wait expires.
     */
    @Override
    public Boolean waitForVisibilityOfAllElementsLocatedBy(By locator) {
        try {
            WebDriverWait wait = new WebDriverWait(EncapsulatedDriver, BaseSettings.Timeout);
            List<WebElement> elements = wait.until(ExpectedConditions.visibilityOfAllElementsLocatedBy(locator));
            if (elements.size() == 0) {
                throw new Exception("Could not confirm the visibility of the element required.");
            }
            return true;
        } catch (Exception ex) {
            return false;
        }
    }

    //endregion

    private void getCollectionOfElements(LocatorType type, String locator) {
        List<WebElement> collection = new LinkedList<WebElement>() {
        };
        switch (type) {
            case ClassName:
                collection = Element.findElements(By.className(locator));
                break;
            case CssSelector:
                collection = Element.findElements(By.cssSelector(locator));
                break;
            case Id:
                collection = Element.findElements(By.id(locator));
                break;
            case LinkText:
                collection = Element.findElements(By.linkText(locator));
                break;
            case Name:
                collection = Element.findElements(By.name(locator));
                break;
            case NotSpecified:
            case PartialLinkText:
                collection = Element.findElements(By.partialLinkText(locator));
                break;
            case TagName:
                collection = Element.findElements(By.tagName(locator));
                break;
            case XPath:
                collection = Element.findElements(By.xpath(locator));
                break;
        }
        Elements = collection;
    }

    private void initialiseRatWatch(Boolean performanceTimings) {
        System.out.println("Creating RatWatch to monitor event timings.");
        RatTimerCollection = new RatWatch();
        RatTimerCollection.StartTimer();
        RecordPerformance = performanceTimings;
        System.out.println("-- Initialised.");
    }

    private void establishDriverType(DriverType driverType, BasePreferences preferences) {
        try {
            DriverName = driverType.name();
            BrowserControl controller = null;

            switch (DriverName.toLowerCase()) {
                case "chromedriver":
                    controller = new ChromeControl(null);
                    break;
                case "firefoxdriver":
                    controller = new FirefoxControl(null);
                    break;
                case "operadriver":
                    controller = new OperaControl(null);
                    break;
                case "safaridriver":
                    if (PlatformUtil.isMac()) {
                        controller = new SafariControl(null);
                    } else {
                        System.out.println("Safari is a Mac only browser.");
                    }
                    break;
                case "edgedriver":
                    if (PlatformUtil.isWindows()) {
                        controller = new EdgeControl(null);
                    } else {
                        System.out.println("Edge is a Windows only browser.");
                    }
                    break;
                case "internetexplorerdriver":
                    if (PlatformUtil.isWindows()) {
                        controller = new IEControl(null);
                    } else {
                        System.out.println("Internet Exploiter is a Windows only browser.");
                    }
                    break;
                case "remotedriver":
                    controller = new RemoteControl();
                    break;
                default:
                    throw new Exception();
            }

            if (controller != null) {
                if (preferences != null) {
                    EncapsulatedDriver = controller.startDriver(preferences);
                } else {
                    EncapsulatedDriver = controller.startDriver();
                }
            } else {
                System.out.println("Unable to load the driver control class requested.");
                System.out.println("Please check that your driver is supported.");
                throw new Exception();
            }
        } catch (Exception ex) {
            ErrorHandler.HandleErrors(EncapsulatedDriver, ex, "RatDriver", "establishDriverType", "Could not establish the driver type");
        }
    }
}

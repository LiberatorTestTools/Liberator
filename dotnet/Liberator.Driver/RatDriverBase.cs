﻿using Liberator.Driver.BrowserControl;
using Liberator.Driver.Enums;
using Liberator.Driver.Performance;
using Liberator.Driver.Preferences;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Liberator.Driver
{
    /// <summary>
    /// Base class for the RatDriver emulation layer
    /// </summary>
    /// <typeparam name="TWebDriver">The type of web driver to create</typeparam>
    public partial class RatDriver<TWebDriver> : IRatDriver<TWebDriver>
        where TWebDriver : IWebDriver, new()
    {
        #region Internal Variables

        Actions _action;

        List<RatProcess> startingProcesses;
        List<RatProcess> testProcesses;

        int _browserPid = 0;
        int _driverPid = 0;

        #endregion

        #region Constructor & Helpers

        /// <summary>
        /// Base constructor for RatDriver
        /// </summary>
        /// <param name="driverSettings">Settings file for the Driver being instantiated.</param>
        /// <param name="performanceTimings">Whether to obtain performance timings for the browser.</param>
        public RatDriver([Optional, DefaultParameterValue(null)]IDriverSettings driverSettings,
            [Optional, DefaultParameterValue(false)]bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }

                EstablishDriverSettings();
                string driverType = typeof(TWebDriver).Name;

                GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);

                string type = "Liberator.Driver.BrowserControl." + driverType + "Control";
                IBrowserControl controller = (IBrowserControl)Activator.CreateInstance(Type.GetType(type));
                Driver = (TWebDriver)controller.StartDriver();

                if (performanceTimings) { RatTimerCollection.StopTimer(EnumTiming.Instantiation); }

                WaitForPageToLoad(null);
                WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);

                GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("An unexpected error has been detected.");
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Creates an anstance of Firefox with a specified profile
        /// </summary>
        /// <param name="profileName">The name of the profile to load</param>
        /// <param name="driverSettings">Settings file for the Driver being instantiated.</param>
        /// <param name="performanceTimings">Whether to obtain performance timings for the browser.</param>
        public RatDriver(string profileName, [Optional, DefaultParameterValue(null)]FirefoxSettings driverSettings,
            [Optional, DefaultParameterValue(false)]bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }

                string driverType = typeof(TWebDriver).Name;

                GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);

                if (typeof(TWebDriver) == typeof(FirefoxDriver))
                {
                    EstablishDriverSettings();
                    FirefoxDriverControl controller = new FirefoxDriverControl();
                    Driver = (TWebDriver)controller.StartDriverSavedProfile(profileName);
                    WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);

                    GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
                }
                else
                {
                    Console.Out.WriteLine("{0} does not currently allow the loading of profiles.", driverType);
                    Console.Out.WriteLine("Please switch to Firefox if named profile loading is required");
                }

                if (performanceTimings) { RatTimerCollection.StopTimer(EnumTiming.Instantiation); }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("An unexpected error has been detected.");
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Creates a firefox instance using a profile directory path
        /// </summary>
        /// <param name="profileDirectory">The path of the profile directory</param>
        /// <param name="cleanDirectory">Whether to clean the directory</param>
        /// <param name="driverSettings">Settings file for the Driver being instantiated.</param>
        /// <param name="performanceTimings">Whether to obtain performance timings for the browser.</param>
        public RatDriver(string profileDirectory, bool cleanDirectory,
            [Optional, DefaultParameterValue(null)]FirefoxSettings driverSettings,
             [Optional, DefaultParameterValue(false)]bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }

                string driverType = typeof(TWebDriver).Name;

                GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);

                if (typeof(TWebDriver) == typeof(FirefoxDriver))
                {
                    EstablishDriverSettings();
                    FirefoxDriverControl controller = new FirefoxDriverControl();
                    Driver = (TWebDriver)controller.StartDriverLoadProfileFromDisk(profileDirectory, cleanDirectory);
                    WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);

                    GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
                }
                else
                {
                    Console.Out.WriteLine("{0} does not currently allow the loading of profiles.", driverType);
                    Console.Out.WriteLine("Please switch to Firefox if named profile loading is required");
                }

                if (performanceTimings) { RatTimerCollection.StopTimer(EnumTiming.Instantiation); }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("An unexpected error has been detected.");
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Creates a chrome driver instance in modile emulation mode
        /// </summary>
        /// <param name="type">Which type of phone to emulate</param>
        /// <param name="touch">(Optional Parameter) Whether touch actions are enabled</param>
        /// <param name="driverSettings">Settings file for the Driver being instantiated.</param>
        /// <param name="performanceTimings">Whether to obtain performance timings for the browser.</param>
        public RatDriver(EnumPhoneType type, [Optional, DefaultParameterValue(false)] bool touch,
            [Optional, DefaultParameterValue(null)]ChromeSettings driverSettings,
            [Optional, DefaultParameterValue(false)]bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }

                string driverType = typeof(TWebDriver).Name;

                GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);

                if (typeof(TWebDriver) == typeof(ChromeDriver))
                {
                    EstablishDriverSettings();
                    ChromeDriverControl controller = new ChromeDriverControl();
                    Driver = (TWebDriver)controller.StartMobileDriver(type, touch);
                    WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);

                    GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
                }
                else
                {
                    Console.Out.WriteLine("{0} does not currently allow the loading of profiles.", driverType);
                    Console.Out.WriteLine("Please switch to Chrome if mobile emulation is required");
                }

                if (performanceTimings) { RatTimerCollection.StopTimer(EnumTiming.Instantiation); }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("An unexpected error has been detected.");
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Creates a chrome driver instance in modile emulation mode
        /// </summary>
        /// <param name="height">The height of the screen</param>
        /// <param name="width">The width of the screen</param>
        /// <param name="userAgent">The user agent returned by the device</param>
        /// <param name="pixelRatio">The pixel ratio of the screen</param>
        /// <param name="touch">(Optional Parameter) Whether touch actions are enabled</param>
        /// <param name="driverSettings">Settings file for the Driver being instantiated.</param>
        /// <param name="performanceTimings">Whether to obtain performance timings for the browser.</param>
        public RatDriver(long height, long width, string userAgent, double pixelRatio,
            [Optional, DefaultParameterValue(false)] bool touch,
            [Optional, DefaultParameterValue(null)]ChromeSettings driverSettings,
            [Optional, DefaultParameterValue(false)]bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }

                string driverType = typeof(TWebDriver).Name;

                GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);

                if (typeof(TWebDriver) == typeof(ChromeDriver))
                {
                    EstablishDriverSettings();
                    ChromeDriverControl controller = new ChromeDriverControl();
                    Driver = (TWebDriver)controller.StartMobileDriver(height, width, userAgent, pixelRatio, touch);
                    WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);

                    GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
                }
                else
                {
                    Console.Out.WriteLine("{0} does not currently allow the loading of profiles.", driverType);
                    Console.Out.WriteLine("Please switch to Chrome if mobile emulation is required");
                }

                if (performanceTimings) { RatTimerCollection.StopTimer(EnumTiming.Instantiation); }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("An unexpected error has been detected.");
                HandleErrors(ex);
            }
        }

        #endregion

        #region Private Methods

        private void GetWebDriverSettings()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Waits for a page to load
        /// </summary>
        /// <param name="element">An element from the previous page. If omitted, the code will wait for the body of the page to be viosible</param>
        public void WaitForPageToLoad(IWebElement element)
        {
            if (RecordPerformance) { RatTimerCollection.StartTimer(); }

            if (element == null)
            {
                var load = new WebDriverWait(Driver, Preferences.BaseSettings.Timeout)
                    .Until(Liberator.ExpectedConditions.ElementIsVisible(By.TagName("body")));
            }
            else if (typeof(TWebDriver) != typeof(OperaDriver))
            {
                var wait = new WebDriverWait(Driver, Preferences.BaseSettings.Timeout)
                    .Until(
                    Liberator.ExpectedConditions.StalenessOf(element));
            }
            else
            {
                //TODO: Investigate the Opera Driver with respect to DOM staleness
                Console.Out.WriteLine("Opera does not currently seem to report staleness of the DOM. Under investigation");
            }

            if (RecordPerformance) { RatTimerCollection.StopTimer(EnumTiming.PageLoad); }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises driver settings.
        /// </summary>
        private void EstablishDriverSettings()
        {
            Id = Guid.NewGuid();
            WindowHandles = new Dictionary<string, string>();
            GetWebDriverSettings();
        }

        /// <summary>
        /// Return the name of the process used by the browser
        /// </summary>
        /// <returns>The name of the browser process</returns>
        private string BrowserProcessName(string driverType)
        {
            switch (driverType.ToLower())
            {
                case "chromedriver":
                    return "chrome";
                case "edgedriver":
                    return "MicrosoftEdge";
                case "firefoxdriver":
                    return "firefox";
                case "internetexplorerdriver":
                    return "iexplore";
                case "operadriver":
                    return "opera";

            }
            return null;
        }

        /// <summary>
        /// Returne the name of the process used by the prowser
        /// </summary>
        /// <returns>The name of the browser process</returns>
        private string DriverProcessName(string driverType)
        {
            switch (driverType.ToLower())
            {
                case "chromedriver":
                    return "chromedriver";
                case "edgedriver":
                    return "msdriver";
                case "firefoxdriver":
                    return "geckodriver";
                case "internetexplorerdriver":
                    return "IEDriverServer";
                case "operadriver":
                    return "operadriver";
            }
            return null;
        }

        /// <summary>
        /// Initialises the RatWatch for performance gathering
        /// </summary>
        /// <param name="performanceTimings">Whether to collect timings</param>
        private void InitialiseRatWatch(bool performanceTimings)
        {
            Console.Out.WriteLine("Creating RatWatch to monitor event timings.");
            RatTimerCollection = new RatWatch();
            RatTimerCollection.StartTimer();
            RecordPerformance = performanceTimings;
            Console.Out.WriteLine("-- Initialised.");
        }

        /// <summary>
        /// Isolates the processes used in the test
        /// </summary>
        /// <param name="driverType">The name ofn the driver type</param>
        /// <param name="processCollectionTime">The time in the initialisation at which the collection occurs.</param>
        private void GetProcesses(string driverType, ProcessCollectionTime processCollectionTime)
        {
            List<RatProcess> processList = new List<RatProcess>();
            Process[] _processes = Process.GetProcesses();

            Console.Out.WriteLine("Gathering the current browser and driver processes.");
            foreach (Process process in _processes)
            {
                if (process.ProcessName.Equals(BrowserProcessName(driverType))
                    || process.ProcessName.Equals(DriverProcessName(driverType)))
                {
                    processList.Add(new RatProcess() { Id = process.Id, Name = process.ProcessName });
                    Console.Out.WriteLine("-- Found: Process {0}, with ID: {1}", process.ProcessName, process.Id);
                }
            }

            switch (processCollectionTime)
            {
                case ProcessCollectionTime.InitialisationStart:
                    startingProcesses = processList;
                    break;
                case ProcessCollectionTime.InitialisationEnd:
                    testProcesses = processList.Except(startingProcesses).ToList();
                    break;
                default:
                    break;
            }
        }

        private void KillTestProcesses()
        {
            foreach (RatProcess process in testProcesses)
            {
                try
                {
                    var processObject = Process.GetProcessById(process.Id);
                    Console.Out.WriteLine("Killing driver and browser processes opened by the test.");
                    if (!processObject.HasExited)
                    {
                        processObject.Kill();
                        processObject.WaitForExit();
                    }
                }
                catch { } //No need to act, the process is already closed
                Console.Out.WriteLine("-- Killed: Processs {0}, with ID: {1}", process.Name, process.Id);
            }
        }

        #endregion
    }

    /// <summary>
    /// Process object for PID handling
    /// </summary>
    internal class RatProcess : IEquatable<RatProcess>
    {
        internal int Id { get; set; }

        internal string Name { get; set; }

        public override bool Equals(object o)
        {
            RatProcess other = o as RatProcess;
            return Equals(other);
        }

        public bool Equals(RatProcess other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            int hashName = Name == null ? 0 : Name.GetHashCode();
            int hashId = Id.GetHashCode();
            return hashId ^ hashName;
        }
    }

    internal enum ProcessCollectionTime
    {
        InitialisationStart,
        InitialisationEnd
    }
}
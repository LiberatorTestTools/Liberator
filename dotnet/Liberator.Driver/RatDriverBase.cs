using Liberator.Driver.BrowserControl;
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

        bool _runTests = false;
        string _browserError = null;

        #endregion

        #region Constructor & Helpers

        /// <summary>
        /// Base constructor for RatDriver
        /// </summary>
        /// <param name="driverSettings">Settings file for the Driver being instantiated.</param>
        /// <param name="performanceTimings">Whether to obtain performance timings for the browser.</param>
        public RatDriver([Optional, DefaultParameterValue(null)] IDriverSettings driverSettings,
            [Optional, DefaultParameterValue(false)] bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }


                string driverType = typeof(TWebDriver).Name;
                EstablishDriverSettings(driverType);

                if (!_runTests) { throw new LiberatorOSException(_browserError); }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);
                }


                string type = "Liberator.Driver.BrowserControl." + driverType + "Control";
                IBrowserControl controller = (IBrowserControl)Activator.CreateInstance(Type.GetType(type), driverSettings);
                Driver = (TWebDriver)controller.StartDriver();

                if (performanceTimings) { RatTimerCollection.StopTimer(EnumTiming.Instantiation); }

                WaitForPageToLoad(null);
                WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);


                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(LiberatorOSException))
                { Console.Out.WriteLine("An unexpected error has been detected."); }
                HandleErrors(ex);
            }
        }

        /// <summary>
        /// Creates an anstance of Firefox with a specified profile
        /// </summary>
        /// <param name="profileName">The name of the profile to load</param>
        /// <param name="driverSettings">Settings file for the Driver being instantiated.</param>
        /// <param name="performanceTimings">Whether to obtain performance timings for the browser.</param>
        public RatDriver(string profileName, [Optional, DefaultParameterValue(null)] FirefoxSettings driverSettings,
            [Optional, DefaultParameterValue(false)] bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }

                string driverType = typeof(TWebDriver).Name;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);
                }

                if (typeof(TWebDriver) == typeof(FirefoxDriver))
                {
                    EstablishDriverSettings(driverType);
                    if (!_runTests) { throw new LiberatorOSException(_browserError); }
                    FirefoxDriverControl controller = new FirefoxDriverControl(driverSettings);
                    Driver = (TWebDriver)controller.StartDriverSavedProfile(profileName);
                    WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
                    }
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
                if (ex.GetType() != typeof(LiberatorOSException))
                {
                    Console.Out.WriteLine("An unexpected error has been detected.");
                }
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
            [Optional, DefaultParameterValue(null)] FirefoxSettings driverSettings,
             [Optional, DefaultParameterValue(false)] bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }

                string driverType = typeof(TWebDriver).Name;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);
                }

                if (typeof(TWebDriver) == typeof(FirefoxDriver))
                {
                    EstablishDriverSettings(driverType);
                    if (!_runTests) { throw new LiberatorOSException(_browserError); }
                    FirefoxDriverControl controller = new FirefoxDriverControl(driverSettings);
                    Driver = (TWebDriver)controller.StartDriverLoadProfileFromDisk(profileDirectory, cleanDirectory);
                    WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
                    }
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

                if (ex.GetType() != typeof(LiberatorOSException))
                { Console.Out.WriteLine("An unexpected error has been detected."); }
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
            [Optional, DefaultParameterValue(null)] ChromeSettings driverSettings,
            [Optional, DefaultParameterValue(false)] bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }

                string driverType = typeof(TWebDriver).Name;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);
                }

                if (typeof(TWebDriver) == typeof(ChromeDriver))
                {
                    EstablishDriverSettings(driverType);
                    if (!_runTests) { throw new LiberatorOSException(_browserError); }
                    ChromeDriverControl controller = new ChromeDriverControl(driverSettings);
                    Driver = (TWebDriver)controller.StartMobileDriver(type, touch);
                    WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
                    }
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
                if (ex.GetType() != typeof(LiberatorOSException))
                { Console.Out.WriteLine("An unexpected error has been detected."); }
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
            [Optional, DefaultParameterValue(null)] ChromeSettings driverSettings,
            [Optional, DefaultParameterValue(false)] bool performanceTimings)
        {
            try
            {
                RecordPerformance = performanceTimings;
                if (performanceTimings) { InitialiseRatWatch(performanceTimings); }

                string driverType = typeof(TWebDriver).Name;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    GetProcesses(driverType, ProcessCollectionTime.InitialisationStart);
                }

                if (typeof(TWebDriver) == typeof(ChromeDriver))
                {
                    EstablishDriverSettings(driverType);
                    if (!_runTests) { throw new LiberatorOSException(_browserError); }

                    ChromeDriverControl controller = new ChromeDriverControl(driverSettings);
                    Driver = (TWebDriver)controller.StartMobileDriver(height, width, userAgent, pixelRatio, touch);
                    WindowHandles.Add(Driver.CurrentWindowHandle, Driver.Title);

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        GetProcesses(driverType, ProcessCollectionTime.InitialisationEnd);
                    }
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
                if (ex.GetType() != typeof(LiberatorOSException))
                { Console.Out.WriteLine("An unexpected error has been detected."); }
                HandleErrors(ex);
            }
        }

        #endregion

        #region Private Methods

        private void GetWebDriverSettings(string driverType)
        {
            switch (driverType.ToLower())
            {
                case "chromedriver":
                case "firefoxdriver":
                    _runTests = true;
                    break;
                case "edgedriver":
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        _runTests = true;
                        _browserError = null;
                    }
                    else
                    {
                        _runTests = false;
                        _browserError = "Edge is only available for Windows.";
                    }
                    break;
                case "internetexplorerdriver":
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        _runTests = true;
                        _browserError = null;
                    }
                    else
                    {
                        _runTests = false;
                        _browserError = "Internet Explorer is only available for Windows.";
                    }
                    break;
                case "operadriver":
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        _runTests = true;
                        _browserError = null;
                    }
                    else
                    {
                        _runTests = false;
                        _browserError = "Opera Driver currently displays errors on Mac OS X & Linux.";
                    }
                    break;
                case "safaridriver":

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        if (Environment.OSVersion.Version.Major != 19)
                        {
                            _runTests = true;
                            _browserError = null;
                        }
                        else
                        {
                            _runTests = false;
                            _browserError = "Safari automation is currently broken on Darwin v19 (Catalina).";
                        }
                    }
                    else
                    {
                        _runTests = false;
                        _browserError = "Safari is only available for Mac OS X.";
                    }
                    break;
                default:
                    _runTests = false;
                    break;
            }
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
                var load = new WebDriverWait(Driver, BaseSettings.Timeout)
                    .Until(ExpectedConditions.ElementIsVisible(By.TagName("body")));
            }
            else if (typeof(TWebDriver) != typeof(OperaDriver))
            {
                var wait = new WebDriverWait(Driver, Preferences.BaseSettings.Timeout)
                    .Until(
                    ExpectedConditions.StalenessOf(element));
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
        private void EstablishDriverSettings(string driverType)
        {
            Id = Guid.NewGuid();
            WindowHandles = new Dictionary<string, string>();
            GetWebDriverSettings(driverType);
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
                    return "msedge";
                case "firefoxdriver":
                    return "firefox";
                case "internetexplorerdriver":
                    return "iexplore";
                case "operadriver":
                    return "opera";
                case "safaridriver":
                    return "Safari";

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
                    return "msedgedriver";
                case "firefoxdriver":
                    return "geckodriver";
                case "internetexplorerdriver":
                    return "IEDriverServer";
                case "operadriver":
                    return "operadriver";
                case "safaridriver":
                    return "safaridriver";
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

            switch (processCollectionTime)
            {
                case ProcessCollectionTime.InitialisationEnd:
                    Console.Out.WriteLine("Gathering the current browser and driver processes.");
                    break;
                default:
                    break;
            }
            foreach (Process process in _processes)
            {
                if (process.ProcessName.Equals(BrowserProcessName(driverType))
                    || process.ProcessName.Equals(DriverProcessName(driverType)))
                {
                    processList.Add(new RatProcess() { Id = process.Id, Name = process.ProcessName });
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
            Console.Out.WriteLine("Killing driver and browser processes opened by the test.");
            if (testProcesses != null)
            {
                foreach (RatProcess process in testProcesses)
                {
                    try
                    {
                        Process processObject = Process.GetProcessById(process.Id);
                        if (!processObject.HasExited)
                        {
                            processObject.Kill();
                            processObject.WaitForExit();
                        }
                    }
                    catch { } //No need to act, the process is already closed
                }

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
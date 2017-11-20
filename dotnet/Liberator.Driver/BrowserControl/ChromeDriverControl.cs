﻿using Liberator.Driver.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Liberator.Driver.BrowserControl
{
    internal class ChromeDriverControl : IBrowserControl
    {
        #region Public Properties

        /// <summary>
        /// Holds the preset values for Chrome Options
        /// </summary>
        public ChromeOptions Options { get; set; }

        /// <summary>
        /// The Chrome driver service
        /// </summary>
        public ChromeDriverService Service { get; set; }

        /// <summary>
        /// The performance logging preferences for Chrome
        /// </summary>
        public ChromePerformanceLoggingPreferences LoggingPreferences { get; set; }

        /// <summary>
        /// Holds the instantiated Chrome Driver
        /// </summary>
        public IWebDriver Driver { get; set; }

        /// <summary>
        /// The maximum amount of time to wait between commands
        /// </summary>
        public TimeSpan CommandTimeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TouchActions Touch { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Sets all defaults from specified entries in the app.config file
        /// </summary>
        public ChromeDriverControl()
        {
            string timeout = Preferences.Preferences.GetPreferenceSetting("Timeout");
            if (!timeout.Contains(",")) { timeout = "0,0,0,10,0"; }
            var to = timeout.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            CommandTimeout = new TimeSpan(Convert.ToInt32(to[0]), Convert.ToInt32(to[1]), Convert.ToInt32(to[2]), Convert.ToInt32(to[3]), Convert.ToInt32(to[4]));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the Chrome Driver with the preset values from app.config
        /// </summary>
        /// <returns>An instance of the Chrome Driver</returns>
        public IWebDriver StartDriver()
        {
            try
            {
                string binaryLocation = Preferences.Preferences.GetPreferenceSetting("ChromeDriver_BinaryLocation");
                string path = null;

                if (binaryLocation == "" || binaryLocation == null) { path = Preferences.Preferences.DriverPath; }
                else { path = binaryLocation; }

                Process[] chromedrivers = Process.GetProcessesByName("chromedriver");
                foreach (Process driver in chromedrivers) { driver.Kill(); }
                SetOptions();
                SetDriverService();
                AddAdditionalCapabilities();
                //SetChromePerformanceLoggingPreferences();
                //Options.SetLoggingPreference("performance", LogLevel.All);
                Driver = new ChromeDriver(path, Options, CommandTimeout);
                return Driver;
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Could not start chrome driver.");
                        Console.WriteLine("Please investigate the changes you have made to your config file.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
                return null;
            }
        }

        /// <summary>
        /// Starts a Chrome Mobile Emuation driver
        /// </summary>
        /// <param name="phoneType">The type of device to emulate</param>
        /// <param name="touch">Whether to respond to toucn events</param>
        /// <returns>An instance of the Chrome Driver set to emulate a particular device</returns>
        public IWebDriver StartMobileDriver(EnumPhoneType phoneType, bool touch)
        {
            try
            {
                SetOptions();
                PhoneTypes.TryGetValue(phoneType, out string phoneName);
                Options.EnableMobileEmulation(phoneName);
                SetMobileDriverService();
                AddAdditionalCapabilities();
                Driver = new ChromeDriver(Service, Options, CommandTimeout);
                return Driver;
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Could not start the chrome driver's mobile emulation functionality.");
                        Console.WriteLine("Please investigate the changes you have made to your config file.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
                return null;
            }
        }

        /// <summary>
        /// Starts a specific mobile emulation using  hights and widths
        /// </summary>
        /// <param name="height">The height of the screen</param>
        /// <param name="width">The width of the screen</param>
        /// <param name="userAgent">The user agent returned by the device</param>
        /// <param name="pixelRatio">The pixel ratio of the screen</param>
        /// <param name="touch">Whether to emulate a touch screen</param>
        /// <returns>A web driver</returns>
        public IWebDriver StartMobileDriver(Int64 height, Int64 width, string userAgent, double pixelRatio, [Optional, DefaultParameterValue(false)]bool touch)
        {
            try
            {
                SetMobileDriverService();
                SetOptions();
                AddAdditionalCapabilities();
                AmendMobileEmulationForChrome(touch, height, width, userAgent, pixelRatio);
                Driver = new ChromeDriver(Service, Options, CommandTimeout);
                return Driver;
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Could not start the chrome driver's mobile emulation functionality.");
                        Console.WriteLine("Please investigate the vales you have passed to the method.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
                return null;
            }
        }

        /// <summary>
        /// Sets the mobile emulation settings for the Chrome Driver
        /// </summary>
        /// <param name="phoneType">The type of device to emulate</param>
        /// <param name="touchEvents">Whether to respond to toucn events</param>
        public void SetMobileEmulationOptions(EnumPhoneType phoneType, bool touchEvents)
        {
            SetOptions();
            Options.EnableMobileEmulation(SetChromeMobileEmulationDeviceSettings(phoneType, touchEvents));
            AddAdditionalCapabilities();
        }

        /// <summary>
        /// Adds an additional capability to the Chrome Driver
        /// </summary>
        /// <param name="name">the name of the capabilitty to add</param>
        /// <param name="value">The value to set for the capability</param>
        public void AddAdditionalCapability(string name, object value)
        {
            try
            {
                Options.AddAdditionalCapability(name, value);
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Unable to add the capability {0} | Value: {1}", name, value);
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
            }
        }

        /// <summary>
        /// Adds an extension to the Chrome Driver
        /// </summary>
        /// <param name="options">The Chrome Options object</param>
        /// <param name="extensionPath">The path to the extension</param>
        /// <returns>The CHrome Options object</returns>
        public ChromeOptions AddExtension(ChromeOptions options, string extensionPath)
        {
            try
            {
                options.AddExtension(extensionPath);
                return options;
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Unable to add the extension {0}", extensionPath);
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
                return null;
            }

        }

        ///// <summary>
        ///// Adds a tracing category to Chrome's logging preferences
        ///// </summary>
        ///// <param name="category">The category of logging to add</param>
        //public void AddTracingCategoryToLoggingPreferences(string category)
        //{
        //    try
        //    {
        //        LoggingPreferences.AddTracingCategory(category);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        #endregion

        #region Private Constructor Methods

        /// <summary>
        /// Sets the Chrome Options from the app.config file
        /// </summary>
        private void SetOptions()
        {
            try
            {
                Boolean.TryParse(Preferences.Preferences.GetPreferenceSetting("Chrome_LeaveBrowserRunning"), out bool leaveRunning);
                string debug = Preferences.Preferences.GetPreferenceSetting("Chrome_DebuggerAddress");
                string minidump = Preferences.Preferences.GetPreferenceSetting("Chrome_MinidumpPath");

                // SetChromePerformanceLoggingPreferences();

                ChromeOptions options = new ChromeOptions()
                {
                    BinaryLocation = Preferences.Preferences.GetPreferenceSetting("Chrome_BinaryLocation")
                };
                if (debug.Contains(@"\")) { options.DebuggerAddress = Preferences.Preferences.GetPreferenceSetting("Chrome_DebuggerAddress"); }
                options.LeaveBrowserRunning = leaveRunning;
                if (minidump.Contains(@"\")) { options.MinidumpPath = minidump; }
                //options.PerformanceLoggingPreferences = LoggingPreferences

                AddAdditionalCapabilities();
                AddExtensions();
                AddLocalStatePreferences();
                AddUserProfilePreferences();
                Options = options;
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Unable to load driver options settings from the config file.");
                        Console.WriteLine("Please reset Liberator.Driver.Dll.Config to its default settings.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the Chrome Driver Service setting from the app.config file
        /// </summary>
        private void SetDriverService()
        {
            try
            {

                Int32.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_AndroidDebugBridgePort"), out int android);
                Boolean.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_EnableVerboseLogging"), out bool verbose);
                Boolean.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_HideCommandPromptWindow"), out bool command);
                Int32.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_Port"), out int port);
                Boolean.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_SuppressInitialDiagnosticInformation"), out bool sidi);


                string binaryLocation = Preferences.Preferences.GetPreferenceSetting("ChromeDriver_BinaryLocation");
                string path = null;

                if (binaryLocation != "" || binaryLocation != null) { path = Preferences.Preferences.DriverPath; }
                else { path = binaryLocation; }

                string logPath = Preferences.Preferences.GetPreferenceSetting("ChromeDriver_LogPath");
                string portServer = Preferences.Preferences.GetPreferenceSetting("ChromeDriver_PortServerAddress");
                string whitelist = Preferences.Preferences.GetPreferenceSetting("ChromeDriver_WhitelistedIPAddresses");

                ChromeDriverService service = ChromeDriverService.CreateDefaultService(path);
                service.AndroidDebugBridgePort = android;
                service.EnableVerboseLogging = verbose;
                service.HideCommandPromptWindow = command;
                if (logPath.Contains(@"\")) { service.LogPath = logPath; }
                service.Port = port;
                if (portServer.Contains(@"\")) { service.PortServerAddress = portServer; }
                service.SuppressInitialDiagnosticInformation = sidi;
                if (whitelist.Contains(".")) { service.WhitelistedIPAddresses = whitelist; }
                Service = service;
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Driver was unable to load the settings for the mobile driver service.");
                        Console.WriteLine("Please reset Liberator.Driver.Dll.Config to its default settings.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
            }
        }


        /// <summary>
        /// Sets the Chrome Mobile Driver Service setting from the app.config file
        /// </summary>
        private void SetMobileDriverService()
        {
            try
            {
                string path = "";
                string portServer, whitelist;

                Int32.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_AndroidDebugBridgePort"), out int android);
                Boolean.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_EnableVerboseLogging"), out bool verbose);
                Boolean.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_HideCommandPromptWindow"), out bool prompt);
                Int32.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_Port"), out int port);
                Boolean.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeDriver_SuppressInitialDiagnosticInformation"), out bool sidi);

                if (Preferences.Preferences.GetPreferenceSetting("ChromeDriver_BinaryLocation") == "") { path = Preferences.Preferences.DriverPath; }
                else { path = Preferences.Preferences.GetPreferenceSetting("ChromeDriver_BinaryLocation"); }

                portServer = Preferences.Preferences.KVList["ChromeDriver_PortServerAddress"].Value;
                whitelist = Preferences.Preferences.KVList["ChromeDriver_WhitelistedIPAddresses"].Value;

                ChromeDriverService service = ChromeDriverService.CreateDefaultService(path);
                service.AndroidDebugBridgePort = android;
                service.EnableVerboseLogging = verbose;
                service.HideCommandPromptWindow = prompt;
                service.LogPath = "";
                service.Port = port;
                if (portServer.Contains(@"\")) { service.PortServerAddress = portServer; }
                service.SuppressInitialDiagnosticInformation = sidi;
                if (whitelist.Contains(@"\")) { service.WhitelistedIPAddresses = whitelist; }
                Service = service;
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Driver was unable to load the settings for the mobile driver service.");
                        Console.WriteLine("Please reset Liberator.Driver.Dll.Config to its default settings.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
            }
        }
        /// <summary>
        /// Adds a series of additional capabilities to the Chrome Options object from the app.config file
        /// </summary>
        private void AddAdditionalCapabilities()
        {
            try
            {

                var capabilityList = Preferences.Preferences.GetPreferenceSetting("Chrome_CapabilityList");

                if (capabilityList.Contains(","))
                {
                    var list = capabilityList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string item in list)
                    {
                        string[] setting = item.Split(new string[] { "=", "|" }, StringSplitOptions.RemoveEmptyEntries);
                        string name = setting[0];
                        Type valueType = Type.GetType(setting[2]);
                        var objValue = Convert.ChangeType(setting[1], valueType);
                        Options.AddAdditionalCapability(name, objValue);
                    }
                }
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Driver was unable to load the capabilities from the config file.");
                        Console.WriteLine("Please reset Liberator.Driver.Dll.Config to its default settings.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
            }
        }

        ///// <summary>
        ///// Loads the Performance Logging Preferences for Chrome from the app.config file
        ///// </summary>
        //private void SetChromePerformanceLoggingPreferences()
        //{
        //    ChromePerformanceLoggingPreferences prefs = new ChromePerformanceLoggingPreferences();
        //    var buri = Preferences.Preferences.KVList["ChromePerformance_BufferUsageReportingInterval"].Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        //    prefs.BufferUsageReportingInterval = new TimeSpan(Convert.ToInt32(buri[0]), Convert.ToInt32(buri[1]), Convert.ToInt32(buri[2]), Convert.ToInt32(buri[3]), Convert.ToInt32(buri[4]));
        //    prefs.IsCollectingNetworkEvents = Convert.ToBoolean(Preferences.Preferences.KVList["ChromePerformance_IsCollectingNetworkEvents"].Value);
        //    prefs.IsCollectingPageEvents = Convert.ToBoolean(Preferences.Preferences.KVList["ChromePerformance_IsCollectingPageEvents"].Value);
        //    prefs.IsCollectingTimelineEvents = Convert.ToBoolean(Preferences.Preferences.KVList["ChromePerformance_IsCollectingTimelineEvents"].Value);
        //    LoggingPreferences = prefs;
        //}

        ///// <summary>
        ///// Adds a tracing category to the logging preferences for Chrome from the app.config file
        ///// </summary>
        //public void AddTracingCategoriesToLoggingPreferences()
        //{
        //    try
        //    {
        //        var categories = Preferences.Preferences.KVList["Chrome_TracingCategories"].Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        //        LoggingPreferences.AddTracingCategories(categories);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        public void AmendMobileEmulationForChrome(bool touchEvent, Int64 height, Int64 width, string userAgent, double pixelRatio)
        {
            try
            {
                bool touch = false;
                Int64 h = 0, w = 0;
                Double pixel;

                Boolean.TryParse(Preferences.Preferences.KVList["ChromeMobileEmulation_EnableTouchEvents"].Value, out touch);
                Int64.TryParse(Preferences.Preferences.KVList["ChromeMobileEmulation_Height"].Value, out h);
                Int64.TryParse(Preferences.Preferences.KVList["ChromeMobileEmulation_Width"].Value, out w);
                Double.TryParse(Preferences.Preferences.KVList["ChromeMobileEmulation_PixelRatio"].Value, out pixel);

                ChromeMobileEmulationDeviceSettings settings = new ChromeMobileEmulationDeviceSettings()
                {
                    EnableTouchEvents = touch,
                    Height = h,
                    PixelRatio = pixel,
                    UserAgent = userAgent,
                    Width = w
                };
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Driver was unable to load the mobile emulation settings.");
                        Console.WriteLine("Please reset Liberator.Driver.Dll.Config to its default settings.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
            }
        }

        /// <summary>
        /// Adds an extension to the Chrome Options object from the app.config
        /// </summary>
        private void AddExtensions()
        {
            try
            {
                var extensionList = Preferences.Preferences.GetPreferenceSetting("Chrome_ExtensionsList");

                if (extensionList.Contains(","))
                {
                    var list = extensionList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    Options.AddExtensions(list);
                }
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Unable to add the extensions specified in the config file");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
            }

        }

        /// <summary>
        /// Add the Local State Preferences from the app.config file
        /// </summary>
        private void AddLocalStatePreferences()
        {
            string name = "";
            object objValue = new object();
            try
            {
                var preferenceList = Preferences.Preferences.GetPreferenceSetting("Chrome_LocalStatePreferences");


                if (preferenceList.Contains(","))
                {
                    var list = preferenceList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string preference in list)
                    {
                        string[] setting = preference.Split(new string[] { "=", "|" }, StringSplitOptions.RemoveEmptyEntries);
                        name = setting[0];
                        Type valueType = Type.GetType(setting[2]);
                        objValue = Convert.ChangeType(setting[1], valueType);
                        Options.AddLocalStatePreference(name, objValue);
                    } 
                }
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Unable to add the local state settings specified in the config file");
                        Console.WriteLine("Setting: {0} | Value: {1}", name, objValue.ToString());
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
            }
        }

        /// <summary>
        /// Adds the User Profile Preferences from the app.config file
        /// </summary>
        private void AddUserProfilePreferences()
        {
            object objValue = new object();
            string name = "";
            try
            {
                var preferenceList = Preferences.Preferences.GetPreferenceSetting("Chrome_UserProfilePreferences");

                if (preferenceList.Contains(","))
                {
                    var list = preferenceList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string preference in list)
                    {
                        string[] setting = preference.Split(new string[] { "=", "|" }, StringSplitOptions.RemoveEmptyEntries);
                        name = setting[0];
                        Type valueType = Type.GetType(setting[2]);
                        objValue = Convert.ChangeType(setting[1], valueType);
                        Options.AddUserProfilePreference(name, objValue);
                    }

                }            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Unable to add the user profile settings specified in the config file");
                        Console.WriteLine("Setting: {0} | Value: {1}", name, objValue.ToString());
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the mobile emulation settings for Chrome from the app.config file
        /// </summary>
        /// <returns>The Chrome Mobile Emulation Device Settings</returns>
        private ChromeMobileEmulationDeviceSettings SetDefaultChromeMobileEmulationDeviceSettings()
        {
            try
            {
                string userAgent;

                Boolean.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeMobileEmulation_EnableTouchEvents"), out bool touch);
                Int64.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeMobileEmulation_Height"), out long height);
                Double.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeMobileEmulation_PixelRatio"), out double ratio);
                Int64.TryParse(Preferences.Preferences.GetPreferenceSetting("ChromeMobileEmulation_Width"), out long width);

                ChromeMobileEmulationDeviceSettings settings = new ChromeMobileEmulationDeviceSettings();

                userAgent = Preferences.Preferences.GetPreferenceSetting("ChromeMobileEmulation_UserAgent");

                settings.EnableTouchEvents = touch;
                settings.Height = height;
                settings.PixelRatio = ratio;
                if (userAgent.Length > 1) { settings.UserAgent = userAgent; }
                settings.Width = width;


                return settings;
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Driver was unable to load the mobile emulation settings.");
                        Console.WriteLine("Please reset Liberator.Driver.Dll.Config to its default settings.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                };
                return null;
            }
        }

        /// <summary>
        /// Sets the mobile emulation settings for Chrome
        /// </summary>
        /// <param name="phoneType">The type of device to emulate</param>
        /// <param name="touchEvents">Whether to respond to touch events</param>
        /// <returns>The Chrome Mobile Emulation Device Settings</returns>
        private ChromeMobileEmulationDeviceSettings SetChromeMobileEmulationDeviceSettings(EnumPhoneType phoneType, bool touchEvents)
        {
            try
            {
                ChromeMobileEmulationDeviceSettings settings = new ChromeMobileEmulationDeviceSettings()
                {
                    EnableTouchEvents = touchEvents,
                    UserAgent = phoneType.ToString(),
                };
                return settings;
            }
            catch (Exception ex)
            {
                switch (Preferences.Preferences.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.WriteLine("Unable to set Chrome Mobile Emulation settings.");
                        Console.WriteLine("Device Type: " + phoneType.ToString() + "| Touch Events: " + touchEvents.ToString());
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        break;
                }
                return null;
            }
        }

        #endregion

        private Dictionary<EnumPhoneType, string> PhoneTypes = new Dictionary<EnumPhoneType, string>()
        {
            { EnumPhoneType.AmazonKindleFireHDX, "Kindle Fire HDX" },
            {EnumPhoneType.AppleiPad, "iPad" },
            {EnumPhoneType.AppleiPadMini, "iPad Mini" },
            {EnumPhoneType.AppleiPadPro, "iPad Pro" },
            {EnumPhoneType.AppleiPhone4, "iPhone 4" },
            {EnumPhoneType.AppleiPhone5, "iPhone 5" },
            {EnumPhoneType.AppleiPhone6, "iPhone 6" },
            {EnumPhoneType.AppleiPhone6Plus, "iPhone 6 Plus" },
            {EnumPhoneType.BlackBerryPlayBook, "Blackberry PlayBook" },
            {EnumPhoneType.BlackBeryZ30, "BlackBerry Z30" },
            {EnumPhoneType.GoogleNexus10, "Nexus 10" },
            {EnumPhoneType.GoogleNexus4, "Nexus 4" },
            {EnumPhoneType.GoogleNexus5, "Nexus 5" },
            {EnumPhoneType.GoogleNexus6, "Nexus 6" },
            {EnumPhoneType.GoogleNexus7, "Nexus 7" },
            {EnumPhoneType.LGOptimusL70, "LG Optimus L70" },
            {EnumPhoneType.MicrosoftLumia550, "Microsoft Lumia 550" },
            {EnumPhoneType.MicrosoftLumia950, "Microsoft Lumia 950" },
            {EnumPhoneType.Nexus5X, "Nexus 5X" },
            {EnumPhoneType.Nexus6P, "Nexus 6P" },
            {EnumPhoneType.NokiaLumia520, "Nokia Lumia 520" },
            {EnumPhoneType.NokiaN9, "Nokia N9" },
            {EnumPhoneType.SamsungGalaxyNote2, "Galaxy Note II" },
            {EnumPhoneType.SamsungGalaxyNote3, "Galaxy Note 3" },
            {EnumPhoneType.SamsungGalaxyS3, "Galaxy S III" },
            {EnumPhoneType.SamsungGalaxyS5, "Galaxy S5" }
        };
    }
}

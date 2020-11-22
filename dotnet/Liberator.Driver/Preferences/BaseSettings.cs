using Liberator.Driver.Enums;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Liberator.Driver.Preferences
{
    /// <summary>
    /// Base settings for RAT Driver
    /// </summary>
    public static class BaseSettings
    {
        /// <summary>
        /// Standard timeout
        /// </summary>
        public static TimeSpan Timeout { get; set; }

        /// <summary>
        /// The length of time to sleep
        /// </summary>
        public static TimeSpan Sleep { get; set; }

        /// <summary>
        /// Implicit wait value for WebDriver
        /// </summary>
        public static TimeSpan ImplicitWait { get; set; }

        /// <summary>
        /// The amount of time to keep a hover active
        /// </summary>
        public static TimeSpan MenuHoverTime { get; set; }

        /// <summary>
        /// Page Load wait time
        /// </summary>
        public static TimeSpan PageLoad { get; set; }

        /// <summary>
        /// Asynchronous JavaScript wait time
        /// </summary>
        public static TimeSpan AsyncJavaScript { get; set; }

        /// <summary>
        /// Whether to allow RatDriver to handle alerts
        /// </summary>
        public static bool AlertHandling { get; set; }

        /// <summary>
        /// Whether to use internal timers
        /// </summary>
        public static bool InternalTimers { get; set; }

        /// <summary>
        /// Location of Chrome Driver. Defaults to the supplied version.
        /// </summary>
        public static string ChromeDriverLocation { get; set; }

        /// <summary>
        /// Location of the Microsoft Web Driver. Defaults to the supplied version.
        /// Windows only
        /// </summary>
        public static string EdgeDriverLocation { get; set; }

        /// <summary>
        /// Location of the Firefox Driver. Defaults to the supplied version.
        /// </summary>
        public static string FirefoxDriverLocation { get; set; }

        /// <summary>
        /// Location of the IEDriverServer executable. Defaults to the supplied version.
        /// Windows only.
        /// </summary>
        public static string InternetExplorerDriverLocation { get; set; }

        /// <summary>
        /// Location of the Opera Driver. Defaults to the supplied version.
        /// </summary>
        public static string OperaDriverLocation { get; set; }

        /// <summary>
        /// Location of the Safari Driver. Defaults to the supplied version.
        /// Mac OS X only
        /// </summary>
        public static string SafariDriverLocation { get; set; }

        /// <summary>
        /// Location of the Chrome application
        /// </summary>
        public static string ChromeLocation { get; set; }

        /// <summary>
        /// Locations of the Firefox application
        /// </summary>
        public static string FirefoxLocation { get; set; }

        /// <summary>
        /// Location of the Opera application
        /// </summary>
        public static string OperaLocation { get; set; }

        /// <summary>
        /// The debug level to use for the test run
        /// </summary>
        public static EnumConsoleDebugLevel DebugLevel { get; set; }



        static internal Assembly _assembly;
        static internal string _libraryLoc;

        static BaseSettings()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _libraryLoc = Assembly.GetExecutingAssembly().FullName.Split(',')[0];

            FindDrivers();
            FindApplications();

            Timeout = new TimeSpan(0, 0, 0, 30, 0);
            Sleep = new TimeSpan(0, 0, 0, 20, 0);
            ImplicitWait = new TimeSpan(0, 0, 0, 10, 0);
            MenuHoverTime = new TimeSpan(0, 0, 0, 10, 0);
            PageLoad = new TimeSpan(0, 0, 0, 30, 0);
            AsyncJavaScript = new TimeSpan(0, 0, 0, 10, 0);

            AlertHandling = true;
            InternalTimers = true;
        }


        static private void FindDrivers()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ChromeDriverLocation = FindExecutables(@".\BrowserDrivers\Win\", "chromedriver.exe");
                EdgeDriverLocation = @".\BrowserDrivers\Win\";
                FirefoxDriverLocation = FindExecutables(@".\BrowserDrivers\Win\", "geckodriver.exe");
                InternetExplorerDriverLocation = FindExecutables(@".\BrowserDrivers\Win\", "IEDriverServer.exe");
                OperaDriverLocation = FindExecutables(@".\BrowserDrivers\Win\", "operadriver.exe");
                SafariDriverLocation = null;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                ChromeDriverLocation = FindExecutables(@"./BrowserDrivers/Mac/", "chromedriver");
                EdgeDriverLocation = null;
                FirefoxDriverLocation = FindExecutables(@"./BrowserDrivers/Mac/", "geckodriver");
                InternetExplorerDriverLocation = null;
                OperaDriverLocation = FindExecutables(@"./BrowserDrivers/Mac/", "operadriver");
                SafariDriverLocation = "/usr/bin/safaridriver";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ChromeDriverLocation = "/usr/local/bin/chromedriver";
                EdgeDriverLocation = null;
                FirefoxDriverLocation = "/usr/local/bin/geckodriver";
                InternetExplorerDriverLocation = null;
                OperaDriverLocation = "/usr/local/bin/operadriver/operadriver";
                SafariDriverLocation = null;
            }
        }


        static private void FindApplications()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    ChromeLocation = FindExecutables(@"C:\Program Files (x86)\Google\Chrome\Application", "chrome.exe");
                    FirefoxLocation = FindExecutables(@"C:\Program Files (x86)\Mozilla Firefox", "firefox.exe");
                    OperaLocation = FindExecutables(@"C:\Program Files\Opera", "opera.exe");
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    ChromeLocation = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome";
                    FirefoxLocation = FindExecutables(@"/Applications/", "Firefox.app/Contents/MacOS/firefox");
                    OperaLocation = FindExecutables(@"/Applications/", "Opera.app/Contents/MacOS/Opera"); ;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    ChromeLocation = null;
                    FirefoxLocation = "usr/bin/firefox";
                    OperaLocation = "/snap/bin/opera";
                }
            }
            catch (Exception)
            {
                Console.Out.WriteLine("Failed to locate an executable.");
            }
        }

        /// <summary>
        /// Gets the grandfather directory for the current test assembly
        /// </summary>
        /// <returns>The grandfather directory</returns>
        static private string GetGrandfatherDirectory()
        {
            try
            {
                return Directory.GetParent(Path.GetFullPath(Path.Combine(Path.GetFullPath(_assembly.Location), @"..\..\"))).FullName;
            }
            catch (Exception)
            {
                Console.Out.WriteLine("Unable to move to grandfather directory.");
                return null;
            }
        }

        /// <summary>
        /// Finds all files with a specified name in a specified directory.
        /// </summary>
        /// <param name="pathToSearch">Path to search for files.</param>
        /// <param name="fileToFind">File name to find.</param>
        /// <returns>An array of file paths that fit the search parameters.</returns>
        static private string FindExecutables(string pathToSearch, string fileToFind)
        {
            try
            {
                return Directory.GetFiles(pathToSearch, fileToFind, SearchOption.AllDirectories)[0];
            }
            catch (Exception)
            {
                Console.Out.WriteLine("No executable found for: {0}", fileToFind);
                return null;
            }
        }
    }

    /// <summary>
    /// Injector for a base settings class
    /// </summary>
    public abstract class BasePreferences : IDriverSettings
    {
        /// <summary>
        /// Creates preset values for the preference injection
        /// </summary>
        public BasePreferences()
        {
            Timeout = new TimeSpan(0, 0, 0, 30, 0);
            Sleep = new TimeSpan(0, 0, 0, 20, 0);
            ImplicitWait = new TimeSpan(0, 0, 0, 10, 0);
            MenuHoverTime = new TimeSpan(0, 0, 0, 10, 0);
            PageLoad = new TimeSpan(0, 0, 0, 30, 0);
            AsyncJavaScript = new TimeSpan(0, 0, 0, 10, 0);

            AlertHandling = true;
            InternalTimers = true;
        }

        /// <summary>
        /// Standard timeout
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// The length of time to sleep
        /// </summary>
        public TimeSpan Sleep { get; set; }

        /// <summary>
        /// Implicit wait value for WebDriver
        /// </summary>
        public TimeSpan ImplicitWait { get; set; }

        /// <summary>
        /// The amount of time to keep a hover active
        /// </summary>
        public TimeSpan MenuHoverTime { get; set; }

        /// <summary>
        /// Page Load wait time
        /// </summary>
        public TimeSpan PageLoad { get; set; }

        /// <summary>
        /// Asynchronous JavaScript wait time
        /// </summary>
        public TimeSpan AsyncJavaScript { get; set; }

        /// <summary>
        /// Whether to allow RatDriver to handle alerts
        /// </summary>
        public bool AlertHandling { get; set; }

        /// <summary>
        /// Whether to use internal timers
        /// </summary>
        public bool InternalTimers { get; set; }

        /// <summary>
        /// Location of Chrome Driver. Defaults to the supplied version.
        /// </summary>
        public string ChromeDriverLocation { get; set; }

        /// <summary>
        /// Location of the Microsoft Web Driver. Defaults to the supplied version.
        /// </summary>
        public string EdgeDriverLocation { get; set; }

        /// <summary>
        /// Location of the Firefox Driver. Defaults to the supplied version.
        /// </summary>
        public string FirefoxDriverLocation { get; set; }

        /// <summary>
        /// Location of the IEDriverServer executable. Defaults to the supplied version.
        /// </summary>
        public string InternetExplorerDriverLocation { get; set; }

        /// <summary>
        /// Location of the Opera Driver. Defaults to the supplied version.
        /// </summary>
        public string OperaDriverLocation { get; set; }

        /// <summary>
        /// Location of the Chrome application
        /// </summary>
        public string ChromeLocation { get; set; }

        /// <summary>
        /// Locations of the Firefox application
        /// </summary>
        public string FirefoxLocation { get; set; }

        /// <summary>
        /// Location of the Opera application
        /// </summary>
        public string OperaLocation { get; set; }

        /// <summary>
        /// The debug level to use for the test run
        /// </summary>
        public EnumConsoleDebugLevel DebugLevel { get; set; }
    }
}

using Liberator.Driver.Enums;
using Liberator.Driver.Preferences;
using OpenQA.Selenium;
using Microsoft.Edge.SeleniumTools;
using System;
using System.IO;
using System.Reflection;

namespace Liberator.Driver.BrowserControl
{
    internal class EdgeDriverControl : IBrowserControl
    {
        #region Public Properties

        /// <summary>
        /// Holds the instantiated Edge Driver
        /// </summary>
        public IWebDriver Driver { get; set; }

        /// <summary>
        /// Holds the preset values for Edge Options
        /// </summary>
        public EdgeOptions Options { get; set; }

        /// <summary>
        /// The Edge driver service
        /// </summary>
        public EdgeDriverService Service { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Loads preset values for the driver and service from the app.config file
        /// </summary>
        public EdgeDriverControl()
        {
            Options = new EdgeOptions
            {
                UseChromium = true,
                BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"
            };
        }

        /// <summary>
        /// Allows the specification of Edge Driver settings
        /// </summary>
        /// <param name="edgeSettings">The settings file to be used.</param>
        public EdgeDriverControl(EdgeSettings edgeSettings)
        {
            Options = new EdgeOptions()
            {
                UseChromium = true,
                BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"
            };

            if (edgeSettings != null)
            {
                Edge.HideCommandPromptWindow = edgeSettings.HideCommandPromptWindow;
                Edge.Host = edgeSettings.Host;
                Edge.Package = edgeSettings.Package;
                Edge.Port = edgeSettings.Port;
                Edge.SuppressInitialDiagnosticInformation = edgeSettings.SuppressInitialDiagnosticInformation;
                Edge.UseVerboseLogging = edgeSettings.UseVerboseLogging;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the Edge Driver with the preset values from app.config
        /// </summary>
        /// <returns>An instance of the Edge Driver</returns>
        public IWebDriver StartDriver()
        {
            try
            {
                SetEdgeDriverService();
                //Assembly assembly = Assembly.GetExecutingAssembly();
                Driver = new EdgeDriver(BaseSettings.EdgeDriverLocation, Options);
                return Driver;
            }
            catch (Exception ex)
            {
                switch (BaseSettings.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.Out.WriteLine("Could not start Edge driver.");
                        Console.Out.WriteLine("Please investigate the changes you have made to your config file.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.Out.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.Out.WriteLine(ex.Message);
                        Console.Out.WriteLine(ex.StackTrace);
                        break;
                }
                return null;
            }
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Sets the Chrome Driver Service setting from the app.config file
        /// </summary>
        private void SetEdgeDriverService()
        {
            try
            {

                bool.TryParse(Edge.HideCommandPromptWindow, out bool command);
                int.TryParse(Edge.Port, out int port);
                bool.TryParse(Edge.SuppressInitialDiagnosticInformation, out bool sidi);
                bool.TryParse(Edge.UseVerboseLogging, out bool verbose);

                string driverLocation = Directory.GetParent(BaseSettings.EdgeDriverLocation).FullName;

                string host = Edge.Host;
                string package = Edge.Package;

                EdgeDriverService service = EdgeDriverService.CreateChromiumService(driverLocation);
                service.HideCommandPromptWindow = command;
                service.Host = host ?? null;
                service.Package = package ?? null;
                service.Port = Convert.ToInt32(port);
                service.SuppressInitialDiagnosticInformation = sidi;
                service.UseVerboseLogging = verbose;
                Service = service;
            }
            catch (Exception ex)
            {
                switch (BaseSettings.DebugLevel)
                {
                    case EnumConsoleDebugLevel.Human:
                        Console.Out.WriteLine("Could not configure the Edge Driver Service settings.");
                        break;
                    case EnumConsoleDebugLevel.NotSpecified:
                    case EnumConsoleDebugLevel.Message:
                        Console.Out.WriteLine(ex.Message);
                        break;
                    case EnumConsoleDebugLevel.StackTrace:
                        Console.Out.WriteLine(ex.Message);
                        Console.Out.WriteLine(ex.StackTrace);
                        break;
                }
            }
        }

        #endregion
    }
}

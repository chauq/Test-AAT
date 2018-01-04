using System;
using System.Configuration;
using System.Drawing;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace Test_AAT.Config
{
    public class CustomRemoteWebDriver : RemoteWebDriver
    {
        public CustomRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities)
        {
        }

        public string getSessionID()
        {
            return base.SessionId.ToString();
        }
    }

    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public const int ExplicitWaitSeconds = 10;
        public const int PollingIntervalMilliseconds = 100;
        public IWebDriver _idriver;

        [BeforeScenario]
        public void Initialise()
        {
            //InitialiseIEDriver();
            InitialiseChromeDriver();
            //InitialiseBrowserStack();
        }

        //[BeforeScenario("Mobile")]
        //public void BeforeMoblieScenario()
        //{
        //    _idriver.Manage().Window.Size = new Size(450, 800);
        //}

        // Run Locally - IE Driver
        private void InitialiseIEDriver()
        {
            _idriver = new InternetExplorerDriver();
            _objectContainer.RegisterInstanceAs<IWebDriver>(_idriver);
            _idriver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("BaseUrl"));
            _idriver.Manage().Window.Maximize();
        }

        // Run Locally - Chrome Driver
        public void InitialiseChromeDriver()
        {
            _idriver = new ChromeDriver();
            _objectContainer.RegisterInstanceAs<IWebDriver>(_idriver);
            _idriver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("BaseUrl"));
            _idriver.Manage().Window.Maximize();
        }

        // Run on BrowserStack
        private void InitialiseBrowserStack()
        {
            BrowserStackCapabilities();

            _driver = new CustomRemoteWebDriver(new Uri(ConfigurationManager.AppSettings.Get("BrowserStackUrl")), capability);
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("BaseUrl"));

            if (isDesktopBrowser == "true")
                _driver.Manage().Window.Maximize();

            GetSessionId();
        }

        CustomRemoteWebDriver _driver;
        DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();
        readonly string isDesktopBrowser = ConfigurationManager.AppSettings.Get("IsDesktopBrowser");


        private void BrowserStackCapabilities()
        {
            capability.SetCapability("browserstack.user", (ConfigurationManager.AppSettings.Get("BrowserStackUser")));
            capability.SetCapability("browserstack.key", (ConfigurationManager.AppSettings.Get("BrowserStackKey")));
            capability.SetCapability("browserstack.debug", (ConfigurationManager.AppSettings.Get("BrowserStackDebug")));


            if (isDesktopBrowser == "true")
            {
                // Desktop Browser
                capability.SetCapability("browser", (ConfigurationManager.AppSettings.Get("BrowserType")));
                capability.SetCapability("browser_version", (ConfigurationManager.AppSettings.Get("BrowserVersion")));
                capability.SetCapability("os", (ConfigurationManager.AppSettings.Get("OsType")));
                capability.SetCapability("os_version", (ConfigurationManager.AppSettings.Get("OsVersion")));
                capability.SetCapability("resolution", (ConfigurationManager.AppSettings.Get("OsResolution")));
            }
            else if (isDesktopBrowser == "false")
            {
                // Mobile Browser
                capability.SetCapability("browserName", (ConfigurationManager.AppSettings.Get("BrowserName")));
                capability.SetCapability("platform", (ConfigurationManager.AppSettings.Get("Platform")));
                capability.SetCapability("device", (ConfigurationManager.AppSettings.Get("Device")));
            }
            else
            {
                Console.WriteLine("*** Browser type not set in App Config");
            }
        }

        private void GetSessionId()
        {
            string sessionId = _driver.getSessionID();
            Console.WriteLine("BROWSERSTACK SESSION ID: " + sessionId);
        }

        [AfterScenario]
        public void CleanUp()
        {
            if (_driver != null)
                _driver.Quit();
            if (_idriver != null)
                _idriver.Quit();
        }
    }
}
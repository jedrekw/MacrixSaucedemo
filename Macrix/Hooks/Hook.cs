using BoDi;
using Macrix.Configuration;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Macrix.Hooks
{
    
    [Binding]
    public sealed class Hooks
    {
        public static ConfigSettings config;
        private string ConfigSettingsPath = System.IO.Directory.GetParent(@"../../../../").FullName +
                                            Path.DirectorySeparatorChar + "Configuration" +Path.DirectorySeparatorChar+ "ConfigVariables.json";
        private static IWebDriver _webDriver;
        public static IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            config = new ConfigSettings();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(ConfigSettingsPath);
            IConfigurationRoot configuration = builder.Build();
            configuration.Bind(config);
            
            switch (config.BrowserName.ToLower())
            {
                case "chrome":
                    _webDriver = new ChromeDriver();
                    break;
                case "firefox":
                    _webDriver = new FirefoxDriver();
                    break;
            }
            _webDriver.Manage().Window.Maximize();
            _container.RegisterInstanceAs<IWebDriver>(_webDriver);
        }
        
        [AfterScenario]
        public void AfterScenario()
        {
            _webDriver.Quit();
        }
    
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;



namespace WebAddressbookTests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigationHelper;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigation.OpenHomePage();
                app.Value = newInstance;
            }
            return app.Value;
        }

        ~ApplicationManager()
        {
            try
            {
                //app.Value.Auth.Logout();
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        private ApplicationManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            baseURL = "http://localhost/addressbook/";

            loginHelper = new LoginHelper(this);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
            navigationHelper = new NavigationHelper(this, baseURL);
        }

        public LoginHelper Auth { get => loginHelper; }
        public NavigationHelper Navigation { get => navigationHelper; }
        public GroupHelper Group { get => groupHelper; }
        public ContactHelper Contact { get => contactHelper; }
        public IWebDriver Driver { get => driver; }

    }
}

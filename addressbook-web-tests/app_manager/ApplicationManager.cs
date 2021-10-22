using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



        public ApplicationManager()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook";

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

        public void Stop()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}

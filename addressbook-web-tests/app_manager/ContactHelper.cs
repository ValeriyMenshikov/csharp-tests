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
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigation.InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigation.GoToHomePage();
            return this;
        }

        public ContactHelper UpdateByIndex(ContactData contact, int index)
        {
            index += 2;
            manager.Navigation.OpenHomePage();
            driver.FindElement(By.CssSelector(String.Format("tr:nth-child({0}) > td:nth-child(8)", index))).Click();
            FillContactForm(contact);
            SubmitGroupUpdate();
            manager.Navigation.GoToHomePage();
            return this;
        }

        private void SubmitGroupUpdate()
        {
            driver.FindElement(By.Name("update")).Click();
            contactsCache = null;
        }

        public bool CheckContacts()
        {
            manager.Navigation.OpenHomePage();
            if (driver.FindElements(By.Name("selected[]")).Count() != 0)
            {
                return true;
            }
            return false;
        }


        public int GetContactsCount()
        {
            manager.Navigation.OpenHomePage();
            return driver.FindElements(By.Name("selected[]")).Count();
        }

        private List<ContactData> contactsCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactsCache == null)
            {
                contactsCache = new List<ContactData>();
                manager.Navigation.OpenHomePage();

                List<IWebElement> elements = driver.FindElements(By.CssSelector("tr")).ToList();
                List<List<IWebElement>> cells = new List<List<IWebElement>>();

                for (int index = 1; index <= elements.Count - 1; index++)
                {
                    cells.Add(elements[index].FindElements(By.TagName("td")).ToList());
                }

                foreach (var cell in cells)
                {
                    // string address = cell[3].Text.Trim();
                    // string all_emails = cell[4].Text.Trim();
                    // string all_phones = cell[5].Text.Trim();
                    ;
                    contactsCache.Add(new ContactData()
                    {
                        Id = cell[0].FindElement(By.TagName("input")).GetAttribute("value"),
                        Lastname = cell[1].Text.Trim(),
                        Firstname = cell[2].Text.Trim()
                    });
                }

            }
            return new List<ContactData>(contactsCache);
        }

        public ContactHelper DeleteByIndex(int index)
        {
            index += 2;
            driver.FindElement(By.CssSelector(String.Format("tr:nth-child({0}) > td:nth-child(1)", index))).Click();
            driver.FindElement(By.CssSelector("div:nth-child(8)")).Click();
            driver.SwitchTo().Alert().Accept();
            manager.Navigation.OpenHomePage();
            contactsCache = null;
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);
            SetDate(By.Name("bday"), contact.Bday);
            SetDate(By.Name("bmonth"), contact.Bmonth);
            Type(By.Name("byear"), contact.Byear);
            SetDate(By.Name("aday"), contact.Aday);
            SetDate(By.Name("amonth"), contact.Amonth);
            Type(By.Name("ayear"), contact.Ayear);
            // driver.FindElement(By.Name("new_group")).Click();
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }

        public void SetDate(By locator, string value)
        {
            if (value != null)
            {
                driver.FindElement(locator).Click();
                new SelectElement(driver.FindElement(locator)).SelectByText(value);
            }
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactsCache = null;
            return this;
        }

    }
}

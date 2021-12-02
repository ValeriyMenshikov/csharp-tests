using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
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

        public ContactData GetContactInformationFromTable(int index)
        {
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            return ExtractFields(cells);
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigation.OpenHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            return new ContactData()
            {
                Firstname = firstName,
                Lastname = lastName,
                Middlename = middleName,
                Address = address,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        public ContactData GetContactInformationFromViewPage(int index)
        {
            manager.Navigation.OpenHomePage();
            ViewContactByIndex(index);
            string text = driver.FindElement(By.Id("content")).Text;
            string fmlAddress = new Regex(@"^.*H:", RegexOptions.Singleline).Match(text).Value.Replace("H:", "");
            string homePhone = new Regex(@"H: (.*)").Match(text).Groups[1].Value.Trim();
            string mobilePhone = new Regex(@"M: (.*)").Match(text).Groups[1].Value.Trim();
            string workPhone = new Regex(@"W: (.*)").Match(text).Groups[1].Value.Trim();
            string secondaryPhone = new Regex(@"P: (.*)").Match(text).Groups[1].Value.Trim();
            IList<IWebElement> emails = driver.FindElements(By.CssSelector("#content > a"));
            string allEmails = ""; 
            foreach (IWebElement item in emails)
            {
                allEmails += item.Text + "\r\n";
            }
            return new ContactData()
            {
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone,
                Phone2 = secondaryPhone,
                AllEmails = allEmails.Trim(),
                FMLAddress = fmlAddress
            };
        }

        private void ViewContactByIndex(int index)
        {
            index += 2;
            manager.Navigation.OpenHomePage();
            driver.FindElement(By.CssSelector(String.Format("tr:nth-child({0}) > td:nth-child(7)", index))).Click();
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
            manager.Navigation.OpenHomePage();
            InitContactModification(index);
            FillContactForm(contact);
            SubmitGroupUpdate();
            manager.Navigation.GoToHomePage();
            return this;
        }

        private void InitContactModification(int index)
        {
            index += 2;
            driver.FindElement(By.CssSelector(String.Format("tr:nth-child({0}) > td:nth-child(8)", index))).Click();
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
                    contactsCache.Add(ExtractFields(cell));
                }

            }
            return new List<ContactData>(contactsCache);
        }

        public ContactData ExtractFields(IList<IWebElement> cell)
        {
            return new ContactData()
            {
                Id = cell[0].FindElement(By.TagName("input")).GetAttribute("value"),
                Lastname = cell[1].Text.Trim(),
                Firstname = cell[2].Text.Trim(),
                Address = cell[3].Text.Trim(),
                AllEmails = cell[4].Text.Trim(),
                AllPhones = cell[5].Text.Trim().Replace("+", "")
            };
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

        public int GetNumberOfSearchResults()
        {
            manager.Navigation.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

    }
}

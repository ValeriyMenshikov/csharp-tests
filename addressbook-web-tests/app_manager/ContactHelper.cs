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
            InitContactModificationByIndex(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            string bDay = driver.FindElement(By.Name("bday")).GetAttribute("value");
            string bMonth = driver.FindElement(By.Name("bmonth")).GetAttribute("value");
            string bYear = driver.FindElement(By.Name("byear")).GetAttribute("value");
            string aDay = driver.FindElement(By.Name("aday")).GetAttribute("value");
            string aMonth = driver.FindElement(By.Name("amonth")).GetAttribute("value");
            string aYear = driver.FindElement(By.Name("ayear")).GetAttribute("value");
            string address2 = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string phone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");
            string contactDetails = CleanAndConcat(firstName,
                                                    middleName,
                                                    lastName,
                                                    nickName,
                                                    title,
                                                    company,
                                                    address,
                                                    homePhone,
                                                    mobilePhone,
                                                    workPhone,
                                                    fax,
                                                    email,
                                                    email2,
                                                    email3,
                                                    homePage,
                                                    bDay,
                                                    bMonth,
                                                    bYear,
                                                    aDay,
                                                    aMonth,
                                                    aYear,
                                                    address2,
                                                    phone2,
                                                    notes);

            return new ContactData()
            {
                Firstname = firstName,
                Lastname = lastName,
                Middlename = middleName,
                Nickname = nickName,
                Title = title,
                Company = company,
                Address = address,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone,
                Fax = fax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Homepage = homePage,
                Bday = bDay,
                Bmonth = bMonth,
                Byear = bYear,
                Aday = aDay,
                Amonth = aMonth,
                Ayear = aYear,
                Address2 = address2,
                Phone2 = phone2,
                Notes = notes,
                ContactDetails = contactDetails,
            };
        }


        private static string CleanAndConcat(string firstName,
                                            string middleName,
                                            string lastName,
                                            string nickName,
                                            string title,
                                            string company,
                                            string address,
                                            string homePhone,
                                            string mobilePhone,
                                            string workPhone,
                                            string fax,
                                            string email,
                                            string email2,
                                            string email3,
                                            string homePage,
                                            string bDay,
                                            string bMonth,
                                            string bYear,
                                            string aDay,
                                            string aMonth,
                                            string aYear,
                                            string address2,
                                            string phone2,
                                            string notes)
        {
            string ContactDetails = "";
            if (!string.IsNullOrEmpty(firstName))
            {
                ContactDetails += firstName;
            }
            if (!string.IsNullOrEmpty(middleName))
            {
                ContactDetails += ' ' + middleName;
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                ContactDetails += ' ' + lastName;
            }
            if (!string.IsNullOrEmpty(nickName))
            {
                ContactDetails += "\r\n" + nickName;
            }
            if (!string.IsNullOrEmpty(title))
            {
                ContactDetails += "\r\n" + title;
            }
            if (!string.IsNullOrEmpty(company))
            {
                ContactDetails += "\r\n" + company;
            }
            if (!string.IsNullOrEmpty(address))
            {
                ContactDetails += "\r\n" + address;
            }
            if (!string.IsNullOrEmpty(homePhone))
            {
                ContactDetails += "\r\n\r\nH: " + homePhone;
            }
            if (!string.IsNullOrEmpty(mobilePhone))
            {
                ContactDetails += "\r\nM: " + mobilePhone;
            }
            if (!string.IsNullOrEmpty(workPhone))
            {
                ContactDetails += "\r\nW: " + workPhone;
            }
            if (!string.IsNullOrEmpty(fax))
            {
                ContactDetails += "\r\nF: " + fax;
            }
            if (!string.IsNullOrEmpty(email))
            {
                ContactDetails += "\r\n\r\n" + email;
            }
            if (!string.IsNullOrEmpty(email2))
            {
                ContactDetails += "\r\n" + email2;
            }
            if (!string.IsNullOrEmpty(email3))
            {
                ContactDetails += "\r\n" + email3;
            }
            if (!string.IsNullOrEmpty(homePage))
            {
                ContactDetails += "\r\nHomepage:\r\n" + homePage;
            }
            if (bDay != "0" || bMonth != "-" || !string.IsNullOrEmpty(bYear))
            {
                string age = CalculateAge(bYear);
                ContactDetails += "\r\n\r\n" + "Birthday " + (bDay == "0" ? "" : bDay + ". ") + (bMonth == "-" ? "" : bMonth + " ") + bYear + string.Format(" ({0})", age);
            }
            if (aDay != "0" || aMonth != "-" || !string.IsNullOrEmpty(aYear))
            {
                string age = CalculateAge(aYear);
                ContactDetails += "\r\n" + "Anniversary " + (aDay == "0" ? "" : aDay + ". ") + (aMonth == "-" ? "" : aMonth + " ") + aYear + string.Format(" ({0})", age);
            }
            if (address2 != "")
            {
                ContactDetails += "\r\n\r\n" + address2;
            }
            if (phone2 != "")
            {
                ContactDetails += "\r\n\r\nP: " + phone2;
            }
            if (notes != "")
            {
                ContactDetails += "\r\n\r\n" + notes;
            }

            return ContactDetails.Trim();
        }

        private static string CalculateAge(string year)
        {
            int age = DateTime.Now.Year - Convert.ToInt32(year);
            
            return age.ToString();
        }

        /*        public ContactData GetContactInformationFromViewPage(int index)
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
                }*/

        public ContactData GetContactInformationFromViewPage(int index)
        {
            manager.Navigation.OpenHomePage();
            ViewContactByIndex(index);
            string text = driver.FindElement(By.Id("content")).Text.Trim();
            return new ContactData()
            {
                ContactDetails = text
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
            InitContactModificationByIndex(index);
            FillContactForm(contact);
            SubmitGroupUpdate();
            manager.Navigation.GoToHomePage();
            return this;
        }
        public ContactHelper Update(ContactData contact, ContactData data)
        {
            manager.Navigation.OpenHomePage();
            driver.FindElement(By.XPath(String.Format("(//input[@name='selected[]' and @value='{0}']/../..//img[@alt='Edit'])", contact.Id))).Click();
            FillContactForm(data);
            SubmitGroupUpdate();
            manager.Navigation.GoToHomePage();
            return this;
        }

        private void InitContactModificationByIndex(int index)
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
            manager.Navigation.OpenHomePage();
            SelectContactByIndex(index);
            SubmitContactDelete();
            return this;
        }

        private void SubmitContactDelete()
        {
            driver.FindElement(By.CssSelector("div:nth-child(8)")).Click();
            driver.SwitchTo().Alert().Accept();
            contactsCache = null;
        }

        private ContactHelper SelectContactByIndex(int index)
        {
            index += 2;
            driver.FindElement(By.CssSelector(String.Format("tr:nth-child({0}) > td:nth-child(1)", index))).Click();
            return this;
        }

        public ContactHelper Delete(ContactData contact)
        {
            manager.Navigation.OpenHomePage();
            SelectContact(contact.Id);
            SubmitContactDelete();
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
            Type(By.Name("fax"), contact.Fax);
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

        public ContactHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigation.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public ContactHelper RemoveFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigation.OpenHomePage();
            SelectGroupFilter(group);
            SelectContact(contact.Id);
            SubmitContactRemoveFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            manager.Navigation.OpenHomePage();
            return this;
        }

        private void SubmitContactRemoveFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        private void SelectGroupFilter(GroupData group)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(group.Id);
        }
    }
}

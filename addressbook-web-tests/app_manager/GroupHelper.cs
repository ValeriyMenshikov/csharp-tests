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
    public class GroupHelper : HelperBase
    {

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigation.OpenGroupPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }


        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper UpdateByIndex(GroupData group, int index)
        {
            manager.Navigation.OpenGroupPage();
            index += 5;
            driver.FindElement(By.CssSelector(String.Format("span:nth-child({0}) > input[type=checkbox]", index))).Click();
            driver.FindElement(By.CssSelector("input[type=submit]:nth-child(3)")).Click();
            FillGroupForm(group);
            SubmitGroupUpdate();
            ReturnToGroupsPage();
            return this;

        }

        public void SubmitGroupUpdate()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
        }

        public int GetGroupsCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        public GroupHelper DeleteByIndex(int index)
        {
            manager.Navigation.OpenGroupPage();
            index += 5;
            driver.FindElement(By.CssSelector(String.Format("span:nth-child({0}) > input[type=checkbox]", index))).Click();
            SubmitGroupDelete();
            ReturnToGroupsPage();
            return this;
        }

        private void SubmitGroupDelete()
        {
            driver.FindElement(By.CssSelector("input[type=submit]:nth-child(2)")).Click();
            groupCache = null;
        }

        public bool CheckGroups()
        {
            manager.Navigation.OpenGroupPage();
            if (driver.FindElements(By.CssSelector("input[type=checkbox]")).Count() != 0)
            {
                return true;
            }
            return false;
        }

        private List<GroupData> groupCache = null;

        public List<GroupData> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                manager.Navigation.OpenGroupPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    GroupData group = new GroupData(element.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    };
                    groupCache.Add(group);
                }
            }
            return new List<GroupData>(groupCache);
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }


        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }
    }
}

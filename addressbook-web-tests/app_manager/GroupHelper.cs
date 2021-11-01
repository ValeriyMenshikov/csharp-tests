﻿using System;
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
            return this;
        }

        public GroupHelper UpdateByIndex(GroupData group, int index)
        {
            manager.Navigation.OpenGroupPage();
            index += 4;
            driver.FindElement(By.CssSelector(String.Format("span:nth-child({0})", index))).Click();
            driver.FindElement(By.CssSelector("input[type=submit]:nth-child(3)")).Click();
            FillGroupForm(group);
            driver.FindElement(By.Name("update")).Click();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper DeleteByIndex(int index)
        {
            manager.Navigation.OpenGroupPage();
            index += 4;
            driver.FindElement(By.CssSelector(String.Format("span:nth-child({0})", index))).Click();
            driver.FindElement(By.CssSelector("input[type=submit]:nth-child(2)")).Click();
            ReturnToGroupsPage();
            return this;
        }


        public GroupHelper FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
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

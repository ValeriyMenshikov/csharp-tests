using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroudUpdateTests : AuthTestBase
    {
        [Test]
        public void GroupUpdateTest()
        {
            GroupData group = new GroupData("group");
            group.Header = "group1";
            group.Footer = "group1";

            if (! app.Group.CheckGroups())
            {
                app.Group.Create(group);
            }

            int index = 0;
            List<GroupData> oldGroups = app.Group.GetGroupList();
            app.Group.UpdateByIndex(group, index);
            List<GroupData> newGroups = app.Group.GetGroupList();
            oldGroups[index].Name = group.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

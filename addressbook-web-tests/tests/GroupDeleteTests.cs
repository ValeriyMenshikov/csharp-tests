using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupDeleteTests : AuthTestBase
    {
        [Test]
        public void GroupDeleteTest()
        {
            int index = 0;
            if (! app.Group.CheckGroups())
            {
                GroupData group = new GroupData("group");
                group.Header = "group";
                group.Footer = "group";
                app.Group.Create(group);
            }
            List<GroupData> oldGroups = app.Group.GetGroupList();
            app.Group.DeleteByIndex(index);
            List<GroupData> newGroups = app.Group.GetGroupList();
            
            oldGroups.RemoveAt(index);
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

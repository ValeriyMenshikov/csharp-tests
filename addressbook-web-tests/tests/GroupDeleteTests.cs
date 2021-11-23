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
            GroupData toBeRemoved = oldGroups[0];
            app.Group.DeleteByIndex(index);
            Assert.AreEqual(oldGroups.Count - 1, app.Group.GetGroupsCount());
            List<GroupData> newGroups = app.Group.GetGroupList();
            
            oldGroups.RemoveAt(index);
            Assert.AreEqual(oldGroups, newGroups);
            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}

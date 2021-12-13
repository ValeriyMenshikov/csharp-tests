using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupDeleteTests : GroupTestBase
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
            List<GroupData> oldGroups = GroupData.GetAll();
            
            GroupData toBeRemoved = oldGroups[index];
            app.Group.Remove(toBeRemoved);
            Assert.AreEqual(oldGroups.Count - 1, app.Group.GetGroupsCount());
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.RemoveAt(index);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}

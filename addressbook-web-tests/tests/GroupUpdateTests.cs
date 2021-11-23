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
            GroupData oldData = oldGroups[index];
            app.Group.UpdateByIndex(group, index);
            Assert.AreEqual(oldGroups.Count, app.Group.GetGroupsCount());
            List<GroupData> newGroups = app.Group.GetGroupList();
            oldGroups[index].Name = group.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            foreach (GroupData g in newGroups)
            {
                if (g.Id == oldData.Id)
                {
                    Assert.AreEqual(group.Name, g.Name);
                }
            }
        }
    }
}

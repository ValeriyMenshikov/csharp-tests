using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroudUpdateTests : GroupTestBase
    {
        [Test]
        public void GroupUpdateTest()
        {
            GroupData group = new GroupData("group3");
            group.Header = "group2";
            group.Footer = "group1";

            if (! app.Group.CheckGroups())
            {
                app.Group.Create(group);
            }

            int index = 0;
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData groupToBeUpdate = oldGroups[index];
            app.Group.Update(groupToBeUpdate, group);
            Assert.AreEqual(oldGroups.Count, app.Group.GetGroupsCount());
            List<GroupData> newGroups = GroupData.GetAll();
            groupToBeUpdate.Name = group.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            foreach (GroupData g in newGroups)
            {
                if (g.Id == groupToBeUpdate.Id)
                {
                    Assert.AreEqual(group.Name, g.Name);
                }
            }
        }
    }
}

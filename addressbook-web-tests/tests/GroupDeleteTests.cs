using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupDeleteTests : AuthTestBase
    {
        [Test]
        public void GroupDeleteTest()
        {
            int index = 1;
            if (app.Group.CheckGroups() == false)
            {
                GroupData group = new GroupData("group");
                group.Header = "group";
                group.Footer = "group";
                app.Group.Create(group);
            }
            app.Group.DeleteByIndex(index);
        }
    }
}

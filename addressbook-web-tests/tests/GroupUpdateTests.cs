using System;
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
            group.Header = "group";
            group.Footer = "group";

            if (app.Group.CheckGroups() == false)
            {
                app.Group.Create(group);
            }

            int index = 1;
            app.Group.UpdateByIndex(group, index);
        }
    }
}

using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupDeleteTests : TestBase
    {
        [Test]
        public void GroupDeleteTest()
        {
            int index = 1;
            app.Group.DeleteByIndex(index);
        }
    }
}

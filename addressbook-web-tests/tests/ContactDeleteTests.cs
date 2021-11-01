using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDeleteTests : TestBase
    {
        [Test]
        public void ContactDeleteTest()
        {
            int index = 1;
            app.Contact.DeleteByIndex(index);
        }
    }
}

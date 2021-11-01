using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactUpdateTests : TestBase
    {
        [Test]
        public void ContactUpdateByIndexTest()
        {
            ContactData contact = new ContactData("2", "3");
            int index = 1;
            app.Contact.UpdateByIndex(contact, index);
        }
    }
}

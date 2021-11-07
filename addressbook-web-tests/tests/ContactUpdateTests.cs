using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactUpdateTests : AuthTestBase
    {
        [Test]
        public void ContactUpdateByIndexTest()
        {
            int index = 1;
            if (app.Contact.CheckContacts() == false)
            {
                app.Contact.Create(new ContactData("firstname", "middlename"));
            }
            ContactData contact = new ContactData("2", "3");
            app.Contact.UpdateByIndex(contact, index);
        }
    }
}

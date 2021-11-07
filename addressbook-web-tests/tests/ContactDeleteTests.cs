using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDeleteTests : AuthTestBase
    {
        [Test]
        public void ContactDeleteTest()
        {
            int index = 1;
            if (app.Contact.CheckContacts() == false)
            {
                ContactData contact = new ContactData("firstname", "middlename");
                app.Contact.Create(contact);
            }
            app.Contact.DeleteByIndex(index);
        }
    }
}

using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDeleteTests : ContactTestBase
    {
        [Test]
        public void ContactDeleteTest()
        {
            int index = 0;
            if (! app.Contact.CheckContacts())
            {
                ContactData contact = new ContactData("firstname", "middlename");
                app.Contact.Create(contact);
            }
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[index];
            app.Contact.Delete(toBeRemoved);
            Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactsCount());
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.RemoveAt(index);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            foreach (ContactData g in newContacts)
            {
                Assert.AreNotEqual(g.Id, toBeRemoved.Id);
            }

        }
    }
}

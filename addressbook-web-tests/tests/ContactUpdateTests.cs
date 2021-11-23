using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactUpdateTests : AuthTestBase
    {
        [Test]
        public void ContactUpdateByIndexTest()
        {
            int index = 0;
            if (! app.Contact.CheckContacts())
            {
                app.Contact.Create(new ContactData("Петров", "Иван ", "Сергеевич"));
            }
            ContactData contact = new ContactData("Петя", "Сергеевич", "Петров");
            
            List<ContactData> oldContacts = app.Contact.GetContactList();
            
            ContactData oldData = oldContacts[index];

            app.Contact.UpdateByIndex(contact, index);
            
            Assert.AreEqual(oldContacts.Count, app.Contact.GetContactsCount());

            List<ContactData> newContacts = app.Contact.GetContactList();

            oldContacts[index].Firstname = contact.Firstname;
            oldContacts[index].Middlename = contact.Middlename;
            oldContacts[index].Lastname = contact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
            foreach (ContactData g in newContacts)
            {
                if (g.Id == oldData.Id)
                {
                    Assert.AreEqual(g.Firstname, contact.Firstname);
                    Assert.AreEqual(g.Lastname, contact.Lastname);
                }
            }
        }
    }
}

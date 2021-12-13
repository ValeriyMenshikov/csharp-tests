using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactUpdateTests : ContactTestBase
    {
        [Test]
        public void ContactUpdateTest()
        {
            int index = 0;
            if (! app.Contact.CheckContacts())
            {
                app.Contact.Create(new ContactData("Петров", "Иван ", "Сергеевич"));
            }
            ContactData contact = new ContactData("Петя", "Сергеевич", "Петров");
            
            List<ContactData> oldContacts = ContactData.GetAll();

            ContactData contactToBeEdit = oldContacts[index];

            app.Contact.Update(contactToBeEdit, contact);
            
            Assert.AreEqual(oldContacts.Count, app.Contact.GetContactsCount());

            List<ContactData> newContacts = ContactData.GetAll();

            contactToBeEdit.Firstname = contact.Firstname;
            contactToBeEdit.Middlename = contact.Middlename;
            contactToBeEdit.Lastname = contact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
            foreach (ContactData g in newContacts)
            {
                if (g.Id == contactToBeEdit.Id)
                {
                    Assert.AreEqual(g.Firstname, contact.Firstname);
                    Assert.AreEqual(g.Lastname, contact.Lastname);
                }
            }
        }
    }
}

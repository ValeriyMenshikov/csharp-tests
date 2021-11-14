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
            app.Contact.UpdateByIndex(contact, index);
            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[0].Firstname = contact.Firstname;
            oldContacts[0].Middlename = contact.Middlename;
            oldContacts[0].Lastname = contact.Lastname;
            oldContacts.Sort();
            foreach (var item in oldContacts)
            {
                System.Console.WriteLine(item);
            }
            newContacts.Sort();
            
            System.Console.WriteLine("----------");

            foreach (var item in newContacts)
            {
                System.Console.WriteLine(item);
            }


            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}

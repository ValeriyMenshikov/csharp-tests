using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("firstname2", "lastname2");
            List<ContactData> oldContacts = app.Contact.GetContactList();
            app.Contact.Create(contact);
            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactsCount());
            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            foreach (var item in oldContacts)
            {
                System.Console.WriteLine(item);
            }

            System.Console.WriteLine("==============");

            foreach (var item in newContacts)
            {
                System.Console.WriteLine(item);
            }
            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}
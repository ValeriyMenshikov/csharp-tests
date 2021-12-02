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
            ContactData contact = new ContactData() {
                Firstname = "FirstName",
                Lastname = "LastName",
                Address = "address",
                Mobile = "+795000012300",
                Work = "89503334442211",
                Home = "312321123",
                Email = "email@mail.ru",
                Email2 = "email123@mail.ru",
                Email3 = "email_32131@mail.ru"
            };
            List<ContactData> oldContacts = app.Contact.GetContactList();
            app.Contact.Create(contact);
            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactsCount());
            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}
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

        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();

            contacts.Add(new ContactData()
            {
                Firstname = "FirstName",
                Lastname = "LastName",
                Address = "address",
                Mobile = "+795000012300",
                Work = "89503334442211",
                Home = "312321123",
                Email = "email@mail.ru",
                Email2 = "email123@mail.ru",
                Email3 = "email_32131@mail.ru"
            });

            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData()
                {
                    Firstname = GenerateRandomString(30),
                    Lastname = GenerateRandomString(30),
                });
            }

            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
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
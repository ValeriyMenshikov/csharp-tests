using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {

        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();

            contacts.Add(new ContactData()
            {
                Firstname = "FirstName",
                Middlename = "MiddleName",
                Lastname = "LastName",
                Nickname = "NickName",
                Title = "Title",
                Company = "Company",
                Address = "address",
                Home = "312321123",
                Mobile = "+795000012300",
                Work = "89503334442211",
                Email = "email@mail.ru",
                Email2 = "email123@mail.ru",
                Email3 = "email_32131@mail.ru",
                Homepage = "homepage.ru",
                Bday = "11",
                Bmonth = "February",
                Byear = "1999",
                Aday = "12",
                Amonth = "July",
                Ayear = "1998",
                Address2 = "Address 2",
                Phone2 = "998798127319823",
                Notes = "Notes"
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

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = ContactData.GetAll();
            app.Contact.Create(contact);
            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactsCount());
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
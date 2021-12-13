using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> contacts = ContactData.GetAll();

            if (groups.Count() == 0)
            {
                app.Group.Create(new GroupData("GroupName")
                {
                    Header = "Header",
                    Footer = "Footer"
                });
                groups = GroupData.GetAll();
            }

            if (contacts.Count() == 0)
            {
                app.Contact.Create(new ContactData("ContactName")
                {
                    Lastname = "LastName",
                    Middlename = "Middlename"
                });
                contacts = ContactData.GetAll();
            }

            GroupData group = groups[0];

            List<ContactData> oldList = group.GetContacts();

            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contact.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests 
{
    [TestFixture]
    public class DeletingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void DeleteContactFromGroupTest()
        {
            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> contacts = ContactData.GetAll();

            if (groups.Count() == 0)
            {
                app.Group.Create(new GroupData("GroupName") {
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

            if (oldList.Count == 0)
            {
                ContactData freeContact = contacts.Except(oldList).First();
                app.Contact.AddContactToGroup(freeContact, group);
                oldList = group.GetContacts();
            }

            ContactData contact = oldList[0];

            app.Contact.RemoveFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}

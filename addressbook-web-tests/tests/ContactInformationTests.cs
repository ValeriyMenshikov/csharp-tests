using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            int index = 0;
            ContactData fromTable = app.Contact.GetContactInformationFromTable(index);
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(index);
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromForm.Address, fromTable.Address);
            Assert.AreEqual(fromForm.AllPhones, fromTable.AllPhones);
            Assert.AreEqual(fromForm.AllEmails, fromTable.AllEmails);
        }

        [Test]
        public void TestContactInformationFromViewPage()
        {
            int index = 0;
            ContactData fromViewPage = app.Contact.GetContactInformationFromViewPage(index);
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(index);
            Assert.AreEqual(fromViewPage.ContactDetails, fromForm.ContactDetails);
        }
    }
}

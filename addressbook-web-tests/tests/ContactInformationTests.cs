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
            int index = 1;
            ContactData fromTable = app.Contact.GetContactInformationFromTable(index);
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(index);
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromForm.Address, fromForm.Address);
            Assert.AreEqual(fromForm.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromForm.AllEmails, fromForm.AllEmails);
        }
    }
}

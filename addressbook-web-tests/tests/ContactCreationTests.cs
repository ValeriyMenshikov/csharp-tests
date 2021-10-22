using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            app.Navigation.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contact.InitContactCreation();
            app.Contact.FillContactForm(new ContactData("firstname", "middlename"));
            app.Contact.SubmitContactCreation();
            app.Navigation.GoToHomePage();
            app.Auth.Logout();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;

        public ContactData()
        {
        }

        public ContactData(string firstname = null,
                           string lastname = null,
                           string middlename = null,
                           string nickname = null,
                           string title = null,
                           string company = null,
                           string address = null,
                           string home = null,
                           string mobile = null,
                           string work = null,
                           string fax = null,
                           string email = null,
                           string email2 = null,
                           string email3 = null,
                           string homepage = null,
                           string bday = null,
                           string bmonth = null,
                           string byear = null,
                           string aday = null,
                           string amonth = null,
                           string ayear = null,
                           string address2 = null,
                           string phone2 = null,
                           string id = null,
                           string notes = null)
        {
            Id = id;
            Firstname = firstname;
            Middlename = middlename;
            Lastname = lastname;
            Nickname = nickname;
            Title = title;
            Company = company;
            Address = address;
            Home = home;
            Mobile = mobile;
            Work = work;
            Fax = fax;
            Email = email;
            Email2 = email2;
            Email3 = email3;
            Homepage = homepage;
            Bday = bday;
            Bmonth = bmonth;
            Byear = byear;
            Aday = aday;
            Amonth = amonth;
            Ayear = ayear;
            Address2 = address2;
            Phone2 = phone2;
            Notes = notes;
        }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }
        
        public string Middlename { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }
        
        public string Nickname { get; set; }
        
        public string Title { get; set; }
        
        public string Company { get; set; }
        
        public string Address { get; set; }
        
        public string Home { get; set; }
        
        public string Mobile { get; set; }
        
        public string Work { get; set; }
        
        public string Fax { get; set; }
        
        public string Email { get; set; }
        
        public string Email2 { get; set; }
        
        public string Email3 { get; set; }
        
        public string Homepage { get; set; }
        
        public string Bday { get; set; }
        
        public string Bmonth { get; set; }
        
        public string Byear { get; set; }
        
        public string Aday { get; set; }
        
        public string Amonth { get; set; }
        
        public string Ayear { get; set; }
        
        public string Address2 { get; set; }
        
        public string Phone2 { get; set; }
        
        public string Notes { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }
        
        public string ContactDetails { get; set; }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(Home) + CleanUp(Mobile) + CleanUp(Work) + CleanUp(Phone2)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }


        private string CleanUpEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return "";
            }
            return email + "\r\n";
        }

        private string CleanUp(string value)
        {
            if (value == null || value == "")
            {
                return "";
            }
            return Regex.Replace(value, @"[ +()-]", "") + "\r\n";
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (Firstname == other.Firstname && Lastname == other.Lastname)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() ^ Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("Id={0}, Firstname={1}, Lastname={2}", Id, Firstname, Lastname);
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null) || this.Id == null)
            {
                return 1;
            }

            if (other.Id == null)
            {
                return -1;
            }

            if (this.Id.Length > other.Id.Length)
            {
                return 1;
            }
            else if (this.Id.Length < other.Id.Length)
            {
                return -1;
            }

            return Id.CompareTo(other.Id);
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

    }
}
using Contacts.Management.Api.DataAccess.Context;
using Contacts.Management.Api.DataAccess.Interfaces;
using Contacts.Management.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Management.Api.DataAccess.Repositories
{
    public class ContactsRepository : IContactsRepository
    {

        public List<Contact> GetContacts()
        {

            using (var dbContext = new ContactsDBContext())
            {
                List<Contact> contacts = dbContext.Contacts.ToList();
                return contacts;
            }
        }

        public bool AddContact(Contact contact)
        {
            using (var dbContext = new ContactsDBContext())
            {
                dbContext.Contacts.Add(contact);
                dbContext.SaveChanges();
                return true;
            }
        }

        public bool UpdateContact(Contact contact)
        {
            using (var dbContext = new ContactsDBContext())
            {
                var con = dbContext.Contacts.FirstOrDefault(c => c.Id == contact.Id);
                if (con != null)
                {
                    con.FirstName = contact.FirstName;
                    con.LastName = contact.LastName;
                    con.Email = contact.Email;
                    con.PhoneNumber = contact.PhoneNumber;
                    con.IsActive = contact.IsActive;

                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ChangeContactStatus(int contactId)
        {
            using (var dbContext = new ContactsDBContext())
            {
                var con = dbContext.Contacts.FirstOrDefault(c => c.Id == contactId);
                if (con != null)
                {
                    con.IsActive = !con.IsActive;
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

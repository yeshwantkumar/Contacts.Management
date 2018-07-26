using Contacts.Management.Api.Models;
using System.Data.Entity;

namespace Contacts.Management.Api.DataAccess.Context
{
    public class ContactsDBContext : DbContext
    {
        public ContactsDBContext(): base("ContactsDB")
        {

        }

        public virtual DbSet<Contact> Contacts { get; set; }
    }
}

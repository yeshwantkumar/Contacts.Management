using Contacts.Management.Api.Models;
using System.Collections.Generic;

namespace Contacts.Management.Api.DataAccess.Interfaces
{
    public interface IContactsRepository
    {
        List<Contact> GetContacts();
        bool AddContact(Contact contact);
        bool UpdateContact(Contact contact);
        bool ChangeContactStatus(int contactId);
    }
}

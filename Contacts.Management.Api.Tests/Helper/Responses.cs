using Contacts.Management.Api.Models;
using System.Collections.Generic;

namespace Contacts.Management.Api.Tests.Helper
{
    public class Responses
    {
        public static List<Contact> ContactsResponse()
        {
            return new List<Contact>()
            {
                new Contact()
                {
                    Id = 1,
                    FirstName = "Yeshwant",
                    LastName = "Kumar",
                    Email = "yeshwant_kumar@outlook.com",
                    PhoneNumber="9767023679",
                    IsActive=true
                },
                new Contact()
                {
                    Id = 2,
                    FirstName = "Yeshwant2",
                    LastName = "Kumar",
                    Email = "yeshwant_kumar@gmail.com",
                    PhoneNumber="123456",
                    IsActive=true
                }
            };
        }
    }
}

using Contacts.Management.Api.Models;

namespace Contacts.Management.Api.Tests.Helper
{
    public class Requests
    {
        public static Contact ContactRequest()
        {
            return new Contact()
                {
                    Id = 1,
                    FirstName = "Yeshwant",
                    LastName = "Kumar",
                    Email = "yeshwant_kumar@outlook.com",
                    PhoneNumber = "9767023679",
                    IsActive = true
                };
        }
    }
}

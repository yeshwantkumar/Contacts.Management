using System.ComponentModel.DataAnnotations;

namespace Contacts.Management.Api.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage="FirstName is required.")]
        [MaxLength(30, ErrorMessage="FirstName must be less than 30 characters.")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage="Invalid Email.")]  
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid PhoneNumber.")]  
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

    }
}


namespace Contacts.Management.Api.Models.ErrorResponses
{
    public class ValidationError : BasicError
    {
        public string Field { get; set; }

        public ValidationError(string field, string message) : base(ErrorCodes.ValidationError.ToString(),message)
        {
            Field = field;
        }
    }
}

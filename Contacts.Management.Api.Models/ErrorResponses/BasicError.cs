
namespace Contacts.Management.Api.Models.ErrorResponses
{
    public class BasicError
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public BasicError(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}

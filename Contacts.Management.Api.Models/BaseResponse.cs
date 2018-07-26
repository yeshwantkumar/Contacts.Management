namespace Contacts.Management.Api.Models
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }

        public bool IsError
        {
            get;
            private set;
        }

        private T _errors;
        public T Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                IsError = true;
            }
        }
    }
}

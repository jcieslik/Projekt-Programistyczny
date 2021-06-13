using System;

namespace Application.Common.Exceptions
{
    public class EmailAlreadyInUseException : Exception
    {
        public EmailAlreadyInUseException()
            : base()
        {
        }

        public EmailAlreadyInUseException(string email)
            : base($"Email; \"{email}\" is already in use.")
        {
        }

    }
}

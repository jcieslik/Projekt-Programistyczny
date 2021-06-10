using System;

namespace Application.Common.Exceptions
{
    public class UsernameAlreadyInUseException : Exception
    {
        public UsernameAlreadyInUseException()
            : base()
        {
        }

        public UsernameAlreadyInUseException(string username)
            : base($"Username; \"{username}\" is already in use.")
        {
        }
    }
}

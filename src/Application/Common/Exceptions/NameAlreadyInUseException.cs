using System;

namespace Application.Common.Exceptions
{
    public class NameAlreadyInUseException : Exception
    {
        public NameAlreadyInUseException()
            : base()
        {
        }

        public NameAlreadyInUseException(string name)
            : base($"Name; \"{name}\" is already in use.")
        {
        }
    }
}

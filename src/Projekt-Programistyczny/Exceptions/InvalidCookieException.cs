using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Exceptions
{
    public class InvalidCookieException : Exception
    {
        public InvalidCookieException() : base("Cookie is missing information about current user") { }
    }
}

using System;

namespace CrossTech.Application.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException() : base()
        {
        }

        public AccessDeniedException(string message) : base(message)
        {
        }
    }
}

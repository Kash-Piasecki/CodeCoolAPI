using System;

namespace CodeCoolAPI.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
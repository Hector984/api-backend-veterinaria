using System.Runtime.Serialization;

namespace API_Veterinaria.Exceptions
{
    public class ForbidenException : Exception
    {
        public ForbidenException()
        {
        }

        public ForbidenException(string? message) : base(message)
        {
        }

        public ForbidenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
